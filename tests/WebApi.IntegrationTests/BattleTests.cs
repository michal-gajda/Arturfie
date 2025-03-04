namespace Arturfie.WebApi.IntegrationTests;

using System.Net;

[TestClass]
public sealed class BattleTests
{
    private static HttpClient? client;

    [ClassInitialize]
    public static void Initialize(TestContext context)
    {
        var factory = new WebApplicationFactory<Program>();
        client = factory.CreateClient();
    }

    [TestMethod]
    public async Task Duel_ThorVsThanos_Should_Return_Thanos()
    {
        // Arrange

        // Act
        var response = await client!.GetAsync("/battle?character=Thor&rival=Thanos");

        // Assert
        response.StatusCode.Should()
            .Be(HttpStatusCode.OK)
            ;
        response.Content.Headers.ContentType?.ToString().Should()
            .Be("text/plain; charset=utf-8")
            ;

        var content = await response.Content.ReadAsStringAsync();
        content.Should()
            .Be("Thanos")
            ;
    }

    [TestMethod]
    public async Task Duel_BatmanVsJoker_Should_Return_Joker()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/battle?hero=Batman&villain=Joker");
        request.Headers.Add("X-Api-Version", "2.0");

        // Act
        var response = await client!.SendAsync(request);

        // Assert
        response.StatusCode.Should()
            .Be(HttpStatusCode.OK)
            ;
        response.Content.Headers.ContentType?.ToString().Should()
            .Be("text/plain; charset=utf-8")
            ;

        var content = await response.Content.ReadAsStringAsync();
        content.Should()
            .Be("Joker")
            ;
    }

    [TestMethod]
    public async Task Duel_SupermanVsLexLuthor_Should_Return_Superman()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/battle?hero=Superman&villain=Lex%20Luthor");
        request.Headers.Add("X-Api-Version", "2.0");

        // Act
        var response = await client!.SendAsync(request);

        // Assert
        response.StatusCode.Should()
            .Be(HttpStatusCode.OK)
            ;
        response.Content.Headers.ContentType?.ToString().Should()
            .Be("text/plain; charset=utf-8")
            ;

        var content = await response.Content.ReadAsStringAsync();
        content.Should()
            .Be("Superman")
            ;
    }

    [TestMethod]
    public async Task Duel_Between_The_Same_Type_Should_Returns_BadRequest()
    {
        // Arrange

        // Act
        var response = await client!.GetAsync("/battle?character=Batman&rival=Superman");

        // Assert
        response.StatusCode.Should()
            .Be(HttpStatusCode.BadRequest)
            ;
        response.Content.Headers.ContentType?.ToString().Should()
            .Be("application/problem+json")
            ;

        var content = await response.Content.ReadAsStringAsync();
        content.Should()
            .Contain("characters of the same type should not fight")
            ;
    }
}
