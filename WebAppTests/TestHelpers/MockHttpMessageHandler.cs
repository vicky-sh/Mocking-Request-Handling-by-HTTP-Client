namespace WebApplicationTests.TestHelpers;

public class MockHttpMessageHandler : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        return Task.FromResult(MockSend(request, cancellationToken));
    }

    public virtual HttpResponseMessage MockSend(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }
}
