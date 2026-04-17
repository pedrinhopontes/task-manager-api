using FluentAssertions;
using Moq;
using TaskManager.Application.Tasks.Commands.CreateTask;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Tests.Tasks;

public class CreateTaskHandlerTests
{
    private readonly Mock<ITaskRepository> _repositoryMock;
    private readonly CreateTaskHandler _handler;

    public CreateTaskHandlerTests()
    {
        _repositoryMock = new Mock<ITaskRepository>();
        _handler = new CreateTaskHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateTaskAndReturnGuid()
    {
        // Arrange
        var command = new CreateTaskCommand("Minha tarefa", "Descrição da tarefa");

        _repositoryMock
            .Setup(r => r.AddAsync(It.IsAny<TaskItem>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty();
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<TaskItem>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCallRepositoryOnce()
    {
        // Arrange
        var command = new CreateTaskCommand("Outra tarefa", null);

        _repositoryMock
            .Setup(r => r.AddAsync(It.IsAny<TaskItem>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<TaskItem>()), Times.Once);
    }
}