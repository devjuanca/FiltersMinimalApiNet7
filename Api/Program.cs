using Application;
using Application.Services.Interfaces;
using Application.Services.Services;
using Domain;
using FiltersMinimalApiNet7.Filters;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICreateWorkerService, CreateWorkerService>();
builder.Services.AddScoped<IIsBlackListed, BlackListReportService>();


builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("createNewWorker", CreateWorker)
.AddFilter<ValidatorFilter<Worker>>()
.AddFilter<WorkerBussinessRuleFilter>();

static async Task<IResult> CreateWorker(Worker newWorker, ICreateWorkerService createWorkerService)
{
    var createdWorker = await createWorkerService.CreateNewWorker(newWorker);

    return Results.Ok(createdWorker);
}

app.Run();
