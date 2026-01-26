using MediatR;
using UStack.Users.Domain.Outcome;

namespace UStack.Users.Application.Abstraction.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }