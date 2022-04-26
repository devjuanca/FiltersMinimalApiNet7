using Application.Services.Interfaces;
using Domain;

namespace Application.Services.Services;

public class CreateWorkerService : ICreateWorkerService
{
    public async Task<Worker> CreateNewWorker(Worker newWorker)
    {
        //Create Worker
        await Task.CompletedTask;
        return newWorker;
    }
}
