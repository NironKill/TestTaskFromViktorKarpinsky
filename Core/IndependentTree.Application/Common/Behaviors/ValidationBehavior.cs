﻿using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace IndependentTree.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(v => v.ValidateAsync(context).Result)
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .ToList();

            foreach (ValidationFailure failure in failures)
                failure.CustomState = request;

            if (failures.Count != 0)           
                throw new ValidationException(failures);
            
            return next();
        }
    }
}
