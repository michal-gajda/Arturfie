namespace Arturfie.Application.UnitTests.Battle;

using Arturfie.Application.Battle.Exceptions;
using Arturfie.Application.Battle.Interfaces;
using Arturfie.Application.Battle.Enums;
using Arturfie.Application.Battle.Models;
using Arturfie.Application.Battle.Services;
using Microsoft.Extensions.Logging.Abstractions;

[TestClass]
public sealed class BattleServiceTests
{
    private readonly static Character Batman = new()
    {
        Name = "Batman",
        Score = 8.3m,
        Type = CharacterType.Hero,
        Weakness = "Joker",
    };

    private readonly static Character HarleyQuinn = new()
    {
        Name = "Harley Quinn",
        Score = 7.3m,
        Type = CharacterType.Villain,
    };

    private readonly static Character Joker = new()
    {
        Name = "Joker",
        Score = 8.2m,
        Type = CharacterType.Villain,
    };

    private readonly static Character Superman = new()
    {
        Name = "Superman",
        Score = 9.6m,
        Type = CharacterType.Hero,
        Weakness = "Lex Luthor"
    };

    private readonly NullLogger<BattleService> logger = new();
    private readonly ICharacterProvider provider = Substitute.For<ICharacterProvider>();
    private readonly IBattleService service;

    public BattleServiceTests()
    {
        this.provider.GetCharacterByNameAsync("Batman", Arg.Any<CancellationToken>()).Returns(Batman);
        this.provider.GetCharacterByNameAsync("Harley Quinn", Arg.Any<CancellationToken>()).Returns(HarleyQuinn);
        this.provider.GetCharacterByNameAsync("Joker", Arg.Any<CancellationToken>()).Returns(Joker);
        this.provider.GetCharacterByNameAsync("Superman", Arg.Any<CancellationToken>()).Returns(Superman);

        this.service = new BattleService(this.logger, provider);
    }

    [TestMethod]
    public async Task FightAsync_GivenUnknownRivalName_ThrowsCharacterNotFoundException()
    {
        // Arrange
        var characterName = "Superman";
        var rivalName = "Lex Luthor";

        // Act
        var action = async () => await service.FightAsync(characterName, rivalName, CancellationToken.None);

        // Assert
        await action.Should()
            .ThrowAsync<CharacterNotFoundException>()
            ;
    }

    [TestMethod]
    public async Task FightAsync_GivenUnknownCharacter_ThrowsCharacterNotFoundException()
    {
        // Arrange
        var characterName = "Lex Luthor";
        var rivalName = "Superman";

        // Act
        var action = async () => await service.FightAsync(characterName, rivalName, CancellationToken.None);

        // Assert
        await action.Should()
            .ThrowAsync<CharacterNotFoundException>()
            ;
    }

    [TestMethod]
    public async Task FightAsync_GivenTwoHeros_WrongOpponentException()
    {
        // Arrange
        var characterName = "Batman";
        var rivalName = "Superman";

        // Act
        var action = async () => await service.FightAsync(characterName, rivalName, CancellationToken.None);

        // Assert
        await action.Should()
            .ThrowAsync<WrongOpponentException>()
            ;
    }

    [TestMethod]
    public async Task FightAsync_GivenHeroAndVillain_Should_Return_Hero_Character_With_Higher_Score()
    {
        // Arrange
        var characterName = "Batman";
        var rivalName = "Harley Quinn";

        // Act
        var sut = await service.FightAsync(characterName, rivalName, CancellationToken.None);

        // Assert
        sut.Should()
            .NotBeNull()
            .And
            .Be(characterName)
            ;
    }
}
