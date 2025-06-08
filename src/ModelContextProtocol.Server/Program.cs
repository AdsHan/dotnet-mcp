using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server.Integration;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole(options =>
{
    options.LogToStandardErrorThreshold = LogLevel.Debug;
});

var serverInfo = new Implementation { Name = "CatalogMCPServer", Version = "1.0.0" };

builder.Services
    .AddMcpServer(mcp =>
    {
        mcp.ServerInfo = serverInfo;
    })
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

builder.Services.AddHttpClient<ProductService>();

await builder.Build().RunAsync();