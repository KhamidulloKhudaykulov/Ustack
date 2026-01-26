using MediatR;
using UStack.Users.Domain.Outcome;

namespace UStack.Users.Application.Abstraction.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{ }