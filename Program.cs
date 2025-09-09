using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Home page with simple HTML so you can see it in a browser.
app.MapGet("/", () => Results.Content($@"<!doctype html>
<html lang='en'>
<head>
  <meta charset='utf-8' />
  <meta name='viewport' content='width=device-width, initial-scale=1' />
  <title>Azure .NET Test Web App</title>
  <style>
    body {{ font-family: system-ui, -apple-system, 'Segoe UI', Roboto, Arial, sans-serif; padding: 2rem; line-height: 1.5; }}
    code, pre {{ background: #f4f4f4; padding: 0.2rem 0.4rem; border-radius: 6px; }}
    .ok {{ color: #0a7d00; font-weight: 700; }}
    ul {{ margin-top: 1rem; }}
    a {{ text-decoration: none; }}
  </style>
</head>
<body>
  <h1>✅ Web App is running!</h1>
  <p>Time (UTC): {DateTime.UtcNow:O}</p>
  <p>Host: {Environment.MachineName}</p>
  <p>.NET: {Environment.Version}</p>
  <p>OS: {RuntimeInformation.OSDescription}</p>
  <ul>
    <li><a href='/health'>/health</a> — JSON health endpoint</li>
    <li><a href='/hello/azure'>/hello/azure</a> — dynamic greeting</li>
    <li><a href='/env'>/env</a> — environment info (safe subset)</li>
  </ul>
</body>
</html>", "text/html"));

// Health check endpoint
app.MapGet("/health", () => Results.Json(new { status = "Healthy", timeUtc = DateTime.UtcNow }));

// Simple route with a parameter
app.MapGet("/hello/{name}", (string name) => $"Hello {name}, your app is working!");

// Environment/diagnostics (safe subset only)
app.MapGet("/env", () =>
{
    string Get(string key) => Environment.GetEnvironmentVariable(key) ?? "(not set)";
    var info = new
    {
        machine = Environment.MachineName,
        framework = RuntimeInformation.FrameworkDescription,
        os = RuntimeInformation.OSDescription,
        processArch = RuntimeInformation.ProcessArchitecture.ToString(),
        website = Get("WEBSITE_SITE_NAME"),
        instance = Get("WEBSITE_INSTANCE_ID"),
        region = Get("REGION_NAME"),
        aspnetEnv = Get("ASPNETCORE_ENVIRONMENT"),
        dotnetInContainer = Get("DOTNET_RUNNING_IN_CONTAINER"),
    };
    return Results.Json(info);
});

app.Run();
