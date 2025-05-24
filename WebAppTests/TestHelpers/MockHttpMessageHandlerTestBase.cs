using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using NSubstitute;
using Shouldly;

namespace WebApplicationTests.TestHelpers;

/// <summary>
///     Provides a base class for unit tests that require mocking of HTTP message handlers and HTTP client factories.
///     Facilitates the setup and verification of HTTP requests and responses in tests.
/// </summary>
public class MockHttpMessageHandlerTestBase
{
    protected MockHttpMessageHandlerTestBase()
    {
        var httpClient = new HttpClient(HttpMessageHandlerMock)
        {
            BaseAddress = new Uri("http://localhost:5000")
        };
        HttpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);
    }

    private MockHttpMessageHandler HttpMessageHandlerMock { get; } =
        Substitute.ForPartsOf<MockHttpMessageHandler>();

    protected IHttpClientFactory HttpClientFactory { get; } = Substitute.For<IHttpClientFactory>();

    /// <summary>
    ///     Verifies that the mocked HTTP message handler processes a request with the specified HTTP method
    ///     and a request URI containing the given string.
    /// </summary>
    /// <param name="httpMethod">The HTTP method (e.g., GET, POST) to validate against the request.</param>
    /// <param name="requestUri">The string that should be contained in the request URI.</param>
    protected void HttpMockHandlerShouldHandleFor(HttpMethod httpMethod, string requestUri)
    {
        HttpMockHandlerDoes(message =>
        {
            message.Method.ShouldBe(httpMethod);
            message.RequestUri!.AbsoluteUri.ShouldContain(requestUri);
        });
    }

    /// <summary>
    ///     Configures the mocked HTTP message handler to return a response with the specified content and status code
    ///     when any HTTP request is sent.
    /// </summary>
    /// <typeparam name="TResponse">The type of the response content to be serialized as JSON.</typeparam>
    /// <param name="response">The response object to be serialized and returned in the HTTP response body.</param>
    /// <param name="statusCode">The HTTP status code to be set in the response.</param>
    protected void HttpMockHandlerReturns<TResponse>(TResponse response, HttpStatusCode statusCode)
    {
        HttpMessageHandlerMock
            .MockSend(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>())
            .Returns(x => new HttpResponseMessage(statusCode)
            {
                Content = JsonContent.Create(response)
            });
    }

    /// <summary>
    ///     Verifies that the mocked HTTP message handler receives a request with a JSON body
    ///     matching the specified request object.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request object expected in the HTTP request body.</typeparam>
    /// <param name="requestObject">The expected request object to compare against the deserialized request body.</param>
    protected void HttpMockHandlerShouldReceiveObject<TRequest>(TRequest requestObject)
    {
        HttpMockHandlerDoes(message =>
        {
            Debug.Assert(message.Content != null);
            var content = message.Content.ReadAsStringAsync().Result;
            var receivedObject = JsonSerializer.Deserialize<TRequest>(content);
            receivedObject.ShouldBe(requestObject);
        });
    }

    private void HttpMockHandlerDoes(Action<HttpRequestMessage> callbackAction)
    {
        HttpMessageHandlerMock
            .When(x => x.MockSend(Arg.Any<HttpRequestMessage>(), Arg.Any<CancellationToken>()))
            .Do(x => callbackAction(x.Arg<HttpRequestMessage>()));
    }
}
