using CManager.Integration.AWS.S3;
using Microsoft.AspNetCore.Identity;

namespace CManager.Application.Handlers
{
    public class AnuncioCommandHandler : IRequestHandler<AddAnuncioCommand.Command, Unit>, 
                                         IRequestHandler<GetAnunciosQuery.Anuncios, List<AnunciosResponse>>,
                                         IRequestHandler<GetAnuncioQuery.Anuncio, AnuncioResponse>
    {
        private readonly IMarcaRepository _marcaRepository;
        private readonly ICaracteristicaRepository _caracteristicaRepository;
        private readonly IOpcionalRepository _opcionalRepository;
        private readonly ITipoCombustivelRepository _tipoCombustivelRepository;
        private readonly IAnuncioRepository _anuncioRepository;
        private readonly IModeloRepository _modeloRepository;
        private readonly IVersaoRepository _versaoRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public AnuncioCommandHandler(IMarcaRepository marcaRepository, ICaracteristicaRepository caracteristicaRepository, IOpcionalRepository opcionalRepository, ITipoCombustivelRepository tipoCombustivelRepository, IAnuncioRepository anuncioRepository, UserManager<IdentityUser> userManager, IModeloRepository modeloRepository, IVersaoRepository versaoRepository)
        {
            _marcaRepository = marcaRepository;
            _caracteristicaRepository = caracteristicaRepository;
            _opcionalRepository = opcionalRepository;
            _tipoCombustivelRepository = tipoCombustivelRepository;
            _anuncioRepository = anuncioRepository;
            _userManager = userManager;
            _modeloRepository = modeloRepository;
            _versaoRepository = versaoRepository;
        }

        #region Post
        public async Task<Unit> Handle(AddAnuncioCommand.Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.usuarioId);
            if(user is null)
                throw new InvalidOperationException("Usuário informado não localizado");

            List<TipoCombustivel> tiposCombustieis = await ValidarRetornarTiposCombustiveisAsync(request);
            List<Opcional> opcionais = await ValidarRetornarOpcionaisAsync(request);
            List<Caracteristica> caracteristicas = await ValidarRetornarCaracteristicasAsync(request);
            Versao versao = await ValidarRetornarVersaoASync(request);
            Modelo modelo = await ValidarRetornarModeloASync(request);

            List<Imagem> imagens = new List<Imagem>();
            foreach (var item in request.files)
            {
                try
                {
                    var imagem = await S3Service.UploadImage(item, "salescar", "us-east-1");
                    imagens.Add(new Imagem(imagem));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

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
                    Cambio = anuncio.Cambio,
                    Cor = anuncio.Cor,
                    Km = anuncio.Km,
                    Estado = anuncio.Estado,
                    Preco = anuncio.Preco,
                    // UsuarioId = anuncio.UsuarioId,
                    ExibirEmail = anuncio.ExibirEmail,
                    ExibirTelefone = anuncio.ExibirTelefone,
                    AnoVeiculo = anuncio.AnoVeiculo,
                    AnoFabricacao = anuncio.AnoFabricacao,
                    Imagem = new ImagemResponse(anuncio.ImagensS3.First())
                }));

            return response;
        }

        public async Task<AnuncioResponse> Handle(GetAnuncioQuery.Anuncio request, CancellationToken cancellationToken)
        {
            var anuncio = await _anuncioRepository.GetByIdAsync(request.id);
            if(anuncio is null)
                throw new InvalidOperationException("Anúncio informado não localizado");

            var response = new AnuncioResponse{
                                                Id = anuncio.Id,
                                                Placa = anuncio.Placa,
                                                Modelo = new(anuncio.Modelo),
                                                TiposCombustiveis = anuncio.TiposCombustiveis,
                                                Opcionais = anuncio.Opcionais?.Select(d => new OpcionalResponse(d)).ToList() ?? new List<OpcionalResponse>(),
                                                Portas = anuncio.Portas,
                                                Cambio = anuncio.Cambio,
                                                Cor = anuncio.Cor,
                                                Caracteristicas = anuncio.Caracteristicas,
                                                Km = anuncio.Km,
                                                Estado = anuncio.Estado,
                                                Preco = anuncio.Preco,
                                                // UsuarioId = anuncio.UsuarioId,
                                                ExibirEmail = anuncio.ExibirEmail,
                                                ExibirTelefone = anuncio.ExibirTelefone,
                                                AnoVeiculo = anuncio.AnoVeiculo,
                                                AnoFabricacao = anuncio.AnoFabricacao,
                                                Imagens = new List<ImagemResponse>()
                                            };

            if(anuncio.ImagensS3 != null && anuncio.ImagensS3.Any())
                anuncio.ImagensS3.ForEach(d => response.Imagens.Add(new ImagemResponse(d)));

            return response;
        }

        #endregion

        #region Métodos
        public async Task<List<Opcional>> ValidarRetornarOpcionaisAsync(AddAnuncioCommand.Command request)
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

        public async Task<List<Caracteristica>> ValidarRetornarCaracteristicasAsync(AddAnuncioCommand.Command request)
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

        public async Task<List<TipoCombustivel>> ValidarRetornarTiposCombustiveisAsync(AddAnuncioCommand.Command request)
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

        public async Task<Modelo> ValidarRetornarModeloASync(AddAnuncioCommand.Command request) => await _modeloRepository.GetByIdAsync(request.idModelo) ?? throw new InvalidOperationException("Modelo informado não localizado");
        public async Task<Versao> ValidarRetornarVersaoASync(AddAnuncioCommand.Command request) => await _versaoRepository.GetByIdAsync(request.idVersao) ?? throw new InvalidOperationException("Versão informada não localizada");
        
        #endregion
    }
}