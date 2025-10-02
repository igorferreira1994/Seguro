using Application.Commands;
using Application.Handlers;
using Application.Ports;
using Domain.Entities;
using FluentAssertions;
using Moq;

namespace Tests;

public class CreatePropostaCommandHandlerTests
{
    private readonly Mock<IPropostaRepository> _mockRepo = new();
    private readonly CreatePropostaCommandHandler _handler;

    public CreatePropostaCommandHandlerTests()
    {
        _handler = new CreatePropostaCommandHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldSavePropostaAndReturnId()
    {
        var command = new CriarPropostaCommand("PROP-001", "Igor", 1000m);

        _mockRepo.Setup(r => r.SaveAsync(It.IsAny<Proposta>()))
                 .Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be("PROP-001");
        _mockRepo.Verify(r => r.SaveAsync(It.Is<Proposta>(p =>
            p.Id == "PROP-001" &&
            p.NomeCliente == "Igor" &&
            p.Valor == 1000m &&
            p.Status == StatusProposta.EmAnalise)), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidCommand_ShouldThrowInvalidOperationException()
    {
        var command = new CriarPropostaCommand("", "", -10m);

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
                 .WithMessage("*Erro ao criar proposta*");

        _mockRepo.Verify(r => r.SaveAsync(It.IsAny<Proposta>()), Times.Never);
    }
}
