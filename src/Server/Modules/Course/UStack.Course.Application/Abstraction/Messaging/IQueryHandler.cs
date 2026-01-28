using MediatR;
using UStack.Course.Domain.Outcome;

namespace UStack.Course.Application.Abstraction.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{ }