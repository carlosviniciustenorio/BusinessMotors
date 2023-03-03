using CManager.Application.Queries;
using Microsoft.AspNetCore.Identity;

namespace CManager.Application.Handlers
{
    public class AnuncioCommandHandler : IRequestHandler<AddAnuncioCommand.Command, Unit>, IRequestHandler<GetAnunciosQuery.Anuncios, List<AnunciosResponse>>
    {
        private readonly IMarcaRepository _marcaRepository;
        private readonly ICaracteristicaRepository _caracteristicaRepository;
        private readonly IOpcionalRepository _opcionalRepository;
        private readonly ITipoCombustivelRepository _tipoCombustivelRepository;
        private readonly IAnuncioRepository _anuncioRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public AnuncioCommandHandler(IMarcaRepository marcaRepository, ICaracteristicaRepository caracteristicaRepository, IOpcionalRepository opcionalRepository, ITipoCombustivelRepository tipoCombustivelRepository, IAnuncioRepository anuncioRepository, UserManager<IdentityUser> userManager)
        {
            _marcaRepository = marcaRepository;
            _caracteristicaRepository = caracteristicaRepository;
            _opcionalRepository = opcionalRepository;
            _tipoCombustivelRepository = tipoCombustivelRepository;
            _anuncioRepository = anuncioRepository;
            _userManager = userManager;
        }

        #region Post
        public async Task<Unit> Handle(AddAnuncioCommand.Command request, CancellationToken cancellationToken)
        {
            List<TipoCombustivel> tiposCombustieis = await ValidarRetornarTiposCombustiveis(request);
            List<Opcional> opcionais = await ValidarRetornarOpcionais(request);
            List<Caracteristica> caracteristicas = await ValidarRetornarCaracteristicas(request);
            Marca marca = await _marcaRepository.GetByIdAsync(request.idMarca);
            
            var user = await _userManager.FindByIdAsync(request.usuarioId);
            if(user is null)
                throw new InvalidOperationException("Usuário informado não localizado");

            Anuncio anuncio = new(request.placa, marca, request.anoModelo, request.anoFabricacao, request.versao, tiposCombustieis, request.portas, request.cambio, request.cor, opcionais, caracteristicas, request.km, request.estado, request.preco, user.Id, request.exibirTelefone, request.exibirEmail);
            await _anuncioRepository.AddAsync(anuncio);
            await _anuncioRepository.SaveChangesAsync();
            
            return Unit.Value;
        }
        
        #endregion

        #region GET
        public async Task<List<AnunciosResponse>> Handle(GetAnunciosQuery.Anuncios request, CancellationToken cancellationToken)
        {
            var anuncios = await _anuncioRepository.GetAllAsync();
            List<AnunciosResponse> response = new();
            anuncios.ForEach(a => response.Add(new AnunciosResponse{
                Id = a.Id,
                Placa = a.Placa,
                Marca = a.Marca,
                AnoModelo = a.AnoModelo,
                AnoFabricacao = a.AnoFabricacao,
                Versao = a.Versao,
                TiposCombustiveis = a.TiposCombustiveis,
                Portas = a.Portas,
                Cambio = a.Cambio,
                Cor = a.Cor,
                Caracteristicas = a.Caracteristicas,
                Km = a.Km,
                Estado = a.Estado,
                Preco = a.Preco,
                UsuarioId = a.UsuarioId,
                ExibirEmail = a.ExibirEmail,
                ExibirTelefone = a.ExibirTelefone
            }));

            return response;
        }
        
        #endregion

        #region Métodos
        public async Task<List<Opcional>> ValidarRetornarOpcionais(AddAnuncioCommand.Command request)
        {
            List<Opcional> opcionais = new();
            if(request.idCaracteristicas != null && request.idCaracteristicas.Any())
            {
                foreach (var item in request.idCaracteristicas)
                {
                    var opcional = await _opcionalRepository.GetByIdAsync(item);
                    opcionais.Add(opcional);
                }
            }

            return opcionais;
        }

        public async Task<List<Caracteristica>> ValidarRetornarCaracteristicas(AddAnuncioCommand.Command request)
        {
            List<Caracteristica> caracteristicas = new();
            if(request.idCaracteristicas != null && request.idCaracteristicas.Any())
            {
                foreach (var item in request.idCaracteristicas)
                {
                    var caracteristica = await _caracteristicaRepository.GetByIdAsync(item);
                    caracteristicas.Add(caracteristica);
                }
            }

            return caracteristicas;
        }

        public async Task<List<TipoCombustivel>> ValidarRetornarTiposCombustiveis(AddAnuncioCommand.Command request)
        {
            List<TipoCombustivel> tiposCombustieis = new();
            if(request.idTiposCombustiveis != null && request.idTiposCombustiveis.Any())
            {
                foreach (var item in request.idTiposCombustiveis)
                {
                    var tipoCombustivel = await _tipoCombustivelRepository.GetByIdAsync(item);
                    tiposCombustieis.Add(tipoCombustivel);
                }
            }

            return tiposCombustieis;
        }

        #endregion
    }
}