namespace Arturfie.Application.FunctionalTests;

using Arturfie.Application.Battle.Exceptions;
using Arturfie.Application.Battle.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

[TestClass]
public sealed class DuelFunctionalTests : ApplicationBootstrapperBase
{
    private readonly IMediator mediator;
    public DuelFunctionalTests() : base()
    {
        this.mediator = this.serviceProvider.GetRequiredService<IMediator>();
    }

    [TestMethod]
    public async Task Duel_Should_Throw_CharacterNotFoundException_For_Empty_Character_Or_Rival()
    {
        // Arrange
        var query = new Duel
        {
            Character = "Character",
            Rival = "Rival"
        };

        // Act
        var action = async () => await this.mediator.Send(query, CancellationToken.None);

        // Assert
        await action.Should()
            .ThrowAsync<CharacterNotFoundException>()
            ;
    }

    [TestMethod]
    public async Task Duel_Should_Throw_OpponentHimselfException_For_Himself_As_Rival()
    {
        // Arrange
        var query = new Duel
        {
            Character = "Batman",
            Rival = "Batman"
        };

        // Act
        var action = async () => await this.mediator.Send(query, CancellationToken.None);

        // Assert
        await action.Should()
            .ThrowAsync<OpponentHimselfException>()
            ;
    }

    [TestMethod]
    public async Task fkdjskjfhskjhfkjsd()
    {
        // Arrange
        var query = new Duel
        {
            Character = "Batman",
            Rival = "Joker"
        };

        // Act
        string sut = await this.mediator.Send(query, CancellationToken.None);

        // Assert
        sut.Should()
            .NotBeEmpty()
            .And
            .Be("Joker")
            ;
    }
}
