using Application.Services.Interfaces;
using Domain;
using FluentValidation;

namespace FiltersMinimalApiNet7.Filters;

public class ValidatorFilter<T> : IRouteHandlerFilter where T : class
{
    private readonly IValidator<T> _validator;

    public ValidatorFilter(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async ValueTask<object?> InvokeAsync(
        RouteHandlerInvocationContext context, RouteHandlerFilterDelegate next)
    {
        T? validatable = context.Parameters.SingleOrDefault(x => x?.GetType() == typeof(T)) as T;

        if (validatable is null)
        {
            return Results.BadRequest();
        }

        var validationResult = await _validator.ValidateAsync(validatable);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(validationResult.Errors);
        }

        var result = await next(context);

        if (result is (Task<IResult>))
            return ((Task<IResult>)result!).Result;

        return result;
    }
}
