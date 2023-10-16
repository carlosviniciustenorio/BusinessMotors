
using Microsoft.AspNetCore.Http;

namespace CManager.Application.Commands
{
    public static class AddAnuncioCommand
    {
        public sealed class Command : IRequest<Unit>
        {
            public string placa {get; set;}
            public int idModelo {get; set;}
            public int idVersao {get; set;}
            public List<int>? idTiposCombustiveis {get; set;}
            public int portas {get; set;}
            public ECambio cambio {get; set;}
            public ECor cor {get; set;}
            public List<int>? idOpcionais {get; set;}
            public List<int>? idCaracteristicas {get; set;}
            public string km {get; set;}
            public string estado {get; set;}
            public string? usuarioId {get; set;}
            public decimal preco {get; set;}
            public bool exibirTelefone {get; set;}
            public bool exibirEmail {get; set;}
            public int anoFabricacao { get; set; }
            public int anoVeiculo { get; set; }
            public List<IFormFile> files {get; set;}
        }

        public sealed class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.placa)
                    .NotEmpty()
                    .WithMessage("Placa não pode ser vazio")
                    .Length(7);

                RuleFor(c => c.idModelo)
                    .NotEmpty()
                    .WithMessage("Id Modelo não pode ser vazio");

                RuleFor(c => c.idVersao)
                    .NotEmpty()
                    .WithMessage("Id Versão não pode ser vazio");

                RuleFor(c => c.anoFabricacao)
                    .NotEmpty()
                    .WithMessage("Ano da fabricação não pode ser vazio");

                RuleFor(c => c.anoVeiculo)
                    .NotEmpty()
                    .WithMessage("Ano do veículo não pode ser vazio");
            }
        }
    }
}