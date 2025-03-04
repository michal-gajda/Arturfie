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
    public async Task Get_Endpoint_ReturnsSuccessAndCorrectContentType()
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
    public async Task Get_Endpoint_ReturnsBadRequestCorrectContentType()
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
