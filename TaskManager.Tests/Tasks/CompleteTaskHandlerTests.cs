using FluentAssertions;
using Moq;
using TaskManager.Application.Tasks.Commands.CompleteTask;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Tests.Tasks;

public class CompleteTaskHandlerTests
{
    private readonly Mock<ITaskRepository> _repositoryMock;
    private readonly CompleteTaskHandler _handler;

    public CompleteTaskHandlerTests()
    {
        _repositoryMock = new Mock<ITaskRepository>();
        _handler = new CompleteTaskHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ExistingTask_ShouldCompleteTask()
    {
        // Arrange
        var task = new TaskItem("Tarefa pra completar", null);
        var command = new CompleteTaskCommand(task.Id);

        _repositoryMock
            .Setup(r => r.GetByIdAsync(task.Id))
            .ReturnsAsync(task);

        _repositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<TaskItem>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        task.IsCompleted.Should().BeTrue();
        task.CompletedAt.Should().NotBeNull();
        _repositoryMock.Verify(r => r.UpdateAsync(task), Times.Once);
    }

    [Fact]
    public async Task Handle_NonExistingTask_ShouldThrowException()
    {
        // Arrange
        var command = new CompleteTaskCommand(Guid.NewGuid());

        _repositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((TaskItem?)null);

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>()
            .WithMessage("Tarefa não encontrada.");
    }
}