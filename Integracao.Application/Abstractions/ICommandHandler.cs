using Integracao.Domain.Shared;

namespace Integracao.Application.Abstractions;

public interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    ValueTask<Result> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}

public interface ICommandHandler<TCommand, TResponse>
    where TCommand : ICommand
    where TResponse : class
{
    ValueTask<Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}
