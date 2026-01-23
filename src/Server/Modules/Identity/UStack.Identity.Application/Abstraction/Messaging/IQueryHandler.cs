using MediatR;
using UStack.Identity.Domain.Outcome;

namespace UStack.Identity.Application.Abstraction.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{ }