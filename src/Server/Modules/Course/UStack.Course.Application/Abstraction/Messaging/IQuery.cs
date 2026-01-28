using MediatR;
using UStack.Course.Domain.Outcome;

namespace UStack.Course.Application.Abstraction.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }