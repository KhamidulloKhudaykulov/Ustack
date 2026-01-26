using MediatR;
using UStack.Users.Domain.Outcome;

namespace UStack.Users.Application.Abstraction.Messaging;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TRequest> : IRequest<Result<TRequest>> { }