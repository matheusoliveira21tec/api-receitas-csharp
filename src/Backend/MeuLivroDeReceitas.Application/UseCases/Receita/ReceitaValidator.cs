using FluentValidation;
using MeuLivroDeReceitas.Comunicacao.Request;
using MeuLivroDeReceitas.Domain.Extension;
using MeuLivroDeReceitas.Exception;

namespace MeuLivroDeReceitas.Application.UseCases.Receita;
public class ReceitaValidator : AbstractValidator<RequestReceitaJson>
{
    public ReceitaValidator()
    {
        RuleFor(x => x.Titulo).NotEmpty().WithMessage(ResourceErrorMessage.TITULO_RECEITA_VAZIO);
        RuleFor(x => x.Categoria).IsInEnum().WithMessage(ResourceErrorMessage.CATEGORIA_RECEITA_INVALIDA);
        RuleFor(x => x.ModoPreparo).NotEmpty().WithMessage(ResourceErrorMessage.MODOPREPARO_RECEITA_VAZIO);
        RuleFor(x => x.Ingredientes).NotEmpty().WithMessage(ResourceErrorMessage.RECEITA_MINIMO_UM_INGREDIENTE);
        RuleFor(x => x.TempoPreparo).InclusiveBetween(1, 1000).WithMessage(ResourceErrorMessage.TEMPO_PREPARO_INVALIDO);
        RuleForEach(x => x.Ingredientes).ChildRules(ingrediente =>
        {
            ingrediente.RuleFor(x => x.Produto).NotEmpty().WithMessage(ResourceErrorMessage.RECEITA_INGREDIENTE_PRODUTO_VAZIO);
            ingrediente.RuleFor(x => x.Quantidade).NotEmpty().WithMessage(ResourceErrorMessage.RECEITA_INGREDIENTE_QUANTIDADE_VAZIO);
        });

        RuleFor(x => x.Ingredientes).Custom((ingredientes, contexto) =>
        {
            var produtosDistintos = ingredientes.Select(c => c.Produto.ToLower()).Distinct();
            if (produtosDistintos.Count() != ingredientes.Count)
            {
                contexto.AddFailure(new FluentValidation.Results.ValidationFailure("Ingredientes", ResourceErrorMessage.RECEITA_INGREDIENTES_REPETIDOS));
            }
        });
    }
}