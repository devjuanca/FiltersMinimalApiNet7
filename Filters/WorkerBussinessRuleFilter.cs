using Application.Services.Interfaces;
using Domain;
using FluentValidation;

namespace FiltersMinimalApiNet7.Filters;

public class WorkerBussinessRuleFilter : IRouteHandlerFilter
{
    private readonly IIsBlackListed _blackListedService;

    public WorkerBussinessRuleFilter(IIsBlackListed blackListedService)
    {
        _blackListedService = blackListedService;
    }

    public async ValueTask<object?> InvokeAsync(
        RouteHandlerInvocationContext context, RouteHandlerFilterDelegate next)
    {
        var worker = context.Parameters.SingleOrDefault(x => x?.GetType() == typeof(Worker)) as Worker;

        if (worker is null)
        {
            return Results.BadRequest();
        }

        if (_blackListedService.IsBlacklisted(worker!.DNI))
        {
            return Results.BadRequest("Worker is in the black list.");
        }
        var result = await next(context);

        if (result is (Task<IResult>))
            return ((Task<IResult>)result!).Result;

        return result;
    }
}
