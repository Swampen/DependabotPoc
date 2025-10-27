using DependabotPoc;
using Infra;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();
builder.Services.AddHttpClient<TestClient>().AddDefaultHttpRetryPolicy<TestClient>();

Console.WriteLine("Hello, World!");