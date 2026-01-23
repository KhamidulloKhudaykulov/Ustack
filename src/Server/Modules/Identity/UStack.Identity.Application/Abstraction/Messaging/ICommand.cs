using MediatR;
using UStack.Identity.Domain.Outcome;

namespace UStack.Identity.Application.Abstraction.Messaging;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TRequest> : IRequest<Result<TRequest>> { }