namespace DependabotPoc;

public class TestClient
{
    public HttpClient Client { get; }

    public TestClient(HttpClient client)
    {
        Client = client;
    }
}