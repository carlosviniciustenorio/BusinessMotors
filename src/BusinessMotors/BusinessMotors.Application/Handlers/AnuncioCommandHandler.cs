using BusinessMotors.Domain.Helpers;
using BusinessMotors.Integration.AWS.S3;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BusinessMotors.Application.Handlers
{
    public class AnuncioCommandHandler : IRequestHandler<AddAnuncioCommand.Command, Unit>, 
                                         IRequestHandler<GetAnunciosQuery.Anuncios, List<AnunciosResponse>>,
                                         IRequestHandler<GetAnuncioQuery.Anuncio, AnuncioResponse>
    {
        private readonly ICaracteristicaRepository _caracteristicaRepository;
        private readonly IOpcionalRepository _opcionalRepository;
        private readonly ITipoCombustivelRepository _tipoCombustivelRepository;
        private readonly IAnuncioRepository _anuncioRepository;
        private readonly IModeloRepository _modeloRepository;
        private readonly IVersaoRepository _versaoRepository;
        private readonly UserManager<Usuario> _userManager;
        private readonly ILogger<AnuncioCommandHandler> _logger;

        public AnuncioCommandHandler(ICaracteristicaRepository caracteristicaRepository, IOpcionalRepository opcionalRepository, ITipoCombustivelRepository tipoCombustivelRepository, IAnuncioRepository anuncioRepository, UserManager<Usuario> userManager, IModeloRepository modeloRepository, IVersaoRepository versaoRepository, ILogger<AnuncioCommandHandler> logger)
        {
            _caracteristicaRepository = caracteristicaRepository;
            _opcionalRepository = opcionalRepository;
            _tipoCombustivelRepository = tipoCombustivelRepository;
            _anuncioRepository = anuncioRepository;
            _userManager = userManager;
            _modeloRepository = modeloRepository;
            _versaoRepository = versaoRepository;
            _logger = logger;
        }

        #region Post
        public async Task<Unit> Handle(AddAnuncioCommand.Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.usuarioId);
            if(user is null)
                throw new InvalidOperationException("Usuário não localizado");

            var tiposCombustieis = await ValidarRetornarTiposCombustiveisAsync(request);
            var opcionais = await ValidarRetornarOpcionaisAsync(request);
            var caracteristicas = await  ValidarRetornarCaracteristicasAsync(request);
            var versao = await ValidarRetornarVersaoASync(request);
            var modelo = await ValidarRetornarModeloASync(request);
            
            List<Imagem> imagens = await EfetuarUploadDeImagensAsync(request.files);

            Anuncio anuncio = new(request.placa,
                                  modelo,
                                  versao,
                                  tiposCombustieis, 
                                  request.portas, 
                                  request.cambio, 
                                  request.cor, 
                                  opcionais, 
                                  caracteristicas, 
                                  request.km, 
                                  request.estado, 
                                  request.cidade,
                                  request.preco, 
                                  user.Id, 
                                  request.exibirTelefone, 
                                  request.exibirEmail,
                                  imagens,
                                  request.anoFabricacao,
                                  request.anoVeiculo,
                                  null);

            await _anuncioRepository.AddAsync(anuncio);
            await _anuncioRepository.SaveChangesAsync();

            var counter = Prometheus.Metrics.CreateCounter("AnunciosCriados","Counter de anúncios criados");
            counter.Inc();

            return Unit.Value;
        }
        
        #endregion

        #region GET
        public async Task<List<AnunciosResponse>> Handle(GetAnunciosQuery.Anuncios request, CancellationToken cancellationToken)
        {
            var anuncios = await _anuncioRepository.GetAllAsync(request);

            List<AnunciosResponse> response = new();

            if(anuncios.Any())
                anuncios.ForEach(anuncio => response.Add(new AnunciosResponse{
                    Id = anuncio.Id,
                    Modelo = new(anuncio.Modelo),
                    Cambio = EnumHelper.GetDisplayName(anuncio.Cambio),
                    Cor = EnumHelper.GetDisplayName(anuncio.Cor),
                    Km = anuncio.Km,
                    Estado = anuncio.Estado,
                    Cidade = anuncio.Cidade,
                    Preco = anuncio.Preco,
                    ExibirEmail = anuncio.ExibirEmail,
                    ExibirTelefone = anuncio.ExibirTelefone,
                    AnoVeiculo = anuncio.AnoVeiculo,
                    AnoFabricacao = anuncio.AnoFabricacao,
                    Imagem = new ImagemResponse(anuncio.ImagensS3.First())
                }));

            var counter = Prometheus.Metrics.CreateCounter("AnunciosConsultados","Counter de anúncios consultados");
            counter.Inc();

            return response;
        }

        public async Task<AnuncioResponse> Handle(GetAnuncioQuery.Anuncio request, CancellationToken cancellationToken)
        {
            var anuncio = await _anuncioRepository.GetByIdAsync(request.Id);
            if(anuncio is null)
                throw new InvalidDataException("Anúncio informado não localizado");

            var response = new AnuncioResponse{
                                                Id = anuncio.Id,
                                                Placa = anuncio.Placa,
                                                Modelo = new(anuncio.Modelo),
                                                TiposCombustiveis = anuncio.TiposCombustiveis?.Select(d => new TipoCombustivelResponse(d)).ToList() ?? new List<TipoCombustivelResponse>(),
                                                Opcionais = anuncio.Opcionais?.Select(d => new OpcionalResponse(d)).ToList() ?? new List<OpcionalResponse>(),
                                                Portas = anuncio.Portas,
                                                Cambio = EnumHelper.GetDisplayName(anuncio.Cambio),
                                                Cor = EnumHelper.GetDisplayName(anuncio.Cor),
                                                Caracteristicas = anuncio.Caracteristicas?.Select(d => new CaracteristicaResponse(d)).ToList() ?? new List<CaracteristicaResponse>(),
                                                Km = anuncio.Km,
                                                Estado = anuncio.Estado,
                                                Cidade = anuncio.Cidade,
                                                Preco = anuncio.Preco,
                                                ExibirEmail = anuncio.ExibirEmail,
                                                ExibirTelefone = anuncio.ExibirTelefone,
                                                AnoVeiculo = anuncio.AnoVeiculo,
                                                AnoFabricacao = anuncio.AnoFabricacao,
                                                Imagens = anuncio.ImagensS3?.Select(d => new ImagemResponse(d)).ToList() ?? new List<ImagemResponse>(),
                                                UsuarioId = anuncio.UserId
                                            };

            var counter = Prometheus.Metrics.CreateCounter("AnuncioConsultado","Counter de anúncio consultado");
            counter.Inc();

            return response;
        }

        #endregion

        #region Métodos
        private async Task<List<Opcional>> ValidarRetornarOpcionaisAsync(AddAnuncioCommand.Command request)
        {
            List<Opcional> opcionais = new();
            if(request.idOpcionais != null && request.idOpcionais.Any())
            {
                foreach (var item in request.idOpcionais)
                {
                    opcionais.Add(await _opcionalRepository.GetByIdAsync(item) ?? throw new InvalidOperationException($"Opcional informado não localizado, id: {item}"));
                }
            }

            return opcionais;
        }

        private async Task<List<Caracteristica>> ValidarRetornarCaracteristicasAsync(AddAnuncioCommand.Command request)
        {
            List<Caracteristica> caracteristicas = new();
            if(request.idCaracteristicas != null && request.idCaracteristicas.Any())
            {
                foreach (var item in request.idCaracteristicas)
                {
                    caracteristicas.Add(await _caracteristicaRepository.GetByIdAsync(item) ?? throw new InvalidOperationException($"Característica informada não localizada, id: {item}"));
                }
            }

            return caracteristicas;
        }

        private async Task<List<TipoCombustivel>> ValidarRetornarTiposCombustiveisAsync(AddAnuncioCommand.Command request)
        {
            List<TipoCombustivel> tiposCombustieis = new();
            if(request.idTiposCombustiveis != null && request.idTiposCombustiveis.Any())
            {
                foreach (var item in request.idTiposCombustiveis)
                {
                    tiposCombustieis.Add(await _tipoCombustivelRepository.GetByIdAsync(item) ?? throw new InvalidOperationException($"Tipo combustível informado não localizado, id: {item}"));
                }
            }

            return tiposCombustieis;
        }

        private async Task<Modelo> ValidarRetornarModeloASync(AddAnuncioCommand.Command request) => await _modeloRepository.GetByIdAsync(request.idModelo) ?? throw new InvalidOperationException("Modelo informado não localizado");
        
        private async Task<Versao> ValidarRetornarVersaoASync(AddAnuncioCommand.Command request) => await _versaoRepository.GetByIdAsync(request.idVersao) ?? throw new InvalidOperationException("Versão informada não localizada");

        private async Task<List<Imagem>> EfetuarUploadDeImagensAsync(List<IFormFile> files)
        {
            List<Imagem> imagens = new();
            try
            {
                foreach (var item in files)
                {
                    var imagem = await S3Service.UploadImage(item, "salescar", "us-east-1");
                    imagens.Add(new Imagem(imagem));
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Erro ao fazer upload de imagens no S3: {e}");
                var counter = Prometheus.Metrics.CreateCounter("AnunciosComFalha","Counter de anúncios com falha");
                counter.Inc();
                throw;
            }
            return imagens;
        }
        #endregion
    }
}