using MediatR;

namespace TaskManager.Application.Tasks.Commands.CreateTask;

public record CreateTaskCommand(string Title, string? Description) : IRequest<Guid>;