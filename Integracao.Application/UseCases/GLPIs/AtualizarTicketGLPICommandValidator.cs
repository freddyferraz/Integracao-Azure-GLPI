using FluentValidation;
using Integracao.Domain.Errors;

namespace Integracao.Application.UseCases.GLPIs;

public class AtualizarTicketGLPICommandValidator : AbstractValidator<AtualizarTicketGLPICommand>
{
    public AtualizarTicketGLPICommandValidator()
    {
        RuleFor(p => p.email)
            .NotEmpty()
            .NotNull()
            .WithErrorCode(UsuarioErrors.EmailUsuarioNaoPodeSerNulo.Code)
            .WithMessage(UsuarioErrors.EmailUsuarioNaoPodeSerNulo.Message);

        RuleFor(p => p.status)
            .NotEmpty()
            .NotNull()
                .WithErrorCode(DeParaStatusErrors.StatusAzureNaoNulo.Code)
                .WithMessage(DeParaStatusErrors.StatusAzureNaoNulo.Message);

        RuleFor(t => t.acodTicketAzure)
            .NotEmpty()
            .NotNull()
                .WithErrorCode(TicketErrors.IdAzureNaoPodeNulo.Code)
                .WithMessage(TicketErrors.IdAzureNaoPodeNulo.Message);

        RuleFor(t => t.token)
            .NotEmpty()
            .NotNull();
    }
}