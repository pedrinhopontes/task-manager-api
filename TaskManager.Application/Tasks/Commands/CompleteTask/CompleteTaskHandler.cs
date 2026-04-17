using MediatR;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Tasks.Commands.CompleteTask;

public class CompleteTaskHandler : IRequestHandler<CompleteTaskCommand>
{
    private readonly ITaskRepository _repository;

    public CompleteTaskHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _repository.GetByIdAsync(request.Id);

        if (task is null)
            throw new Exception("Tarefa não encontrada.");

        task.Complete();
        await _repository.UpdateAsync(task);
    }
}