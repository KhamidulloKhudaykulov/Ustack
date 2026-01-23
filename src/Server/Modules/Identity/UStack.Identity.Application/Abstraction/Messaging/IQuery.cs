using MediatR;
using UStack.Identity.Domain.Outcome;

namespace UStack.Identity.Application.Abstraction.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }