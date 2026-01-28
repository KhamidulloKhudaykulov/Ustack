using MediatR;
using UStack.Course.Domain.Outcome;

namespace UStack.Course.Application.Abstraction.Messaging;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TRequest> : IRequest<Result<TRequest>> { }