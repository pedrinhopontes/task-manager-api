using MediatR;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Tasks.Queries.GetAllTasks;

public record GetAllTasksQuery : IRequest<IEnumerable<TaskItem>>;