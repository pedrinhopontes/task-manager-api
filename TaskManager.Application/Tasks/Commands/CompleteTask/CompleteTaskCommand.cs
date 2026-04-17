using MediatR;

namespace TaskManager.Application.Tasks.Commands.CompleteTask;

public record CompleteTaskCommand(Guid Id) : IRequest;