using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddApplicationInsightsTelemetry();

builder.Services
    .AddMcpServer()
     .WithHttpTransport()
    .WithToolsFromAssembly();


var app = builder.Build();

var logger = app.Logger;
logger.LogInformation("Mcp Server starting...");

// Step 2: Map MCP to a custom route (optional base path)
app.MapMcp("/api/mcp");

app.Run();


[McpServerToolType]
public class ExactMcpTools
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<ExactMcpTools> _logger;

    public ExactMcpTools(IHttpClientFactory httpClientFactory, ILogger<ExactMcpTools> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    [McpServerTool(Name = "GetPolicy"), Description("Fetches policy from Exact by policy reference")]
    public async Task<object> GetPolicyAsync(string policyReference)
    {
        try
        {
            _logger.LogInformation($"Fetching policy data for {policyReference}");

            using var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("SignatureKey", Environment.GetEnvironmentVariable("SignatureKey"));
            client.DefaultRequestHeaders.Add("UnitPsu", Environment.GetEnvironmentVariable("UnitPsu"));
            client.DefaultRequestHeaders.Add("Authorization", Environment.GetEnvironmentVariable("Authorization"));

            var url = $"{Environment.GetEnvironmentVariable("APIWebServiceUrl")}/api/policy/{policyReference}";

            _logger.LogInformation($"Calling url {url}");

            var response = await client.GetStringAsync(url);

            return new { success = true, data = response };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured while fetching policy data for {policyReference}", policyReference);

            return new
            {
                success = false,
                message = "Error occured while fetching policy data",
                error = ex.Message
            };
        }
    }

    [McpServerTool(Name = "GetSchedules"), Description("Fetches policy schedules from Exact by policy reference")]
    public async Task<object> GetSchedulesAsync(string policyReference)
    {
        try
        {
            _logger.LogInformation($"Fetching schedules data for {policyReference}");

            using var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("SignatureKey", Environment.GetEnvironmentVariable("SignatureKey"));
            client.DefaultRequestHeaders.Add("UnitPsu", Environment.GetEnvironmentVariable("UnitPsu"));
            client.DefaultRequestHeaders.Add("Authorization", Environment.GetEnvironmentVariable("Authorization"));

            var url = $"{Environment.GetEnvironmentVariable("APIWebServiceUrl")}/api/v1/policy/{policyReference}/schedules";

            _logger.LogInformation($"Calling url {url}");

            var response = await client.GetStringAsync(url);

            return new { success = true, data = response };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured while fetching schedule data for {policyReference}", policyReference);

            return new
            {
                success = false,
                message = "Error occured while fetching schedule data",
                error = ex.Message
            };
        }
    }

    [McpServerTool(Name = "CreateSchedules"), Description("Create policy schedules for Exact policy reference")]
    public async Task<object> CreateSchedulesAsync(string policyReference, object scheduleData)
    {

        try
        {
            _logger.LogInformation($"Create schedules for policy {policyReference}");

            using var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("SignatureKey", Environment.GetEnvironmentVariable("SignatureKey"));
            client.DefaultRequestHeaders.Add("UnitPsu", Environment.GetEnvironmentVariable("UnitPsu"));
            client.DefaultRequestHeaders.Add("Authorization", Environment.GetEnvironmentVariable("Authorization"));

            var url = $"{Environment.GetEnvironmentVariable("APIWebServiceUrl")}/api/v1/policy/{policyReference}/schedules";

            _logger.LogInformation($"Calling url {url}");

            var json = JsonSerializer.Serialize(scheduleData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            return new { success = true, data = responseBody };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured while creating schedule for {policyReference}", policyReference);

            return new
            {
                success = false,
                message = "Error occured while creating schedule data",
                error = ex.Message
            };
        }
    }
}