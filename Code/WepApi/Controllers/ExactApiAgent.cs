using Azure;
using Azure.AI.Agents.Persistent;
using Azure.AI.Projects;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExcelToJsonAgentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExactApiAgentController : ControllerBase
    {
        [HttpPost("pullData")]
        public async Task<IActionResult> PullData([FromForm]string description, [FromForm] string threadId)
        {
            var response = await CallAgent(description, threadId);

            return Ok(response);
        }

        private async Task<string> CallAgent(string description, string threadId) {
            var messages = await RunAgentConversation(description, threadId);
            return string.Join("\n", messages);
        }

        static async Task<IEnumerable<string>> RunAgentConversation(string userInput, string tId)
        {
            List<string> messagesOut = new List<string>();
            try
            {
                var endpoint = new Uri("https://enpowering-brockers-ai-agent.services.ai.azure.com/api/projects/enpowering-brockers-agent-project");
                //access token obtained from azure cloud console due to some reason.
                string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6InlFVXdtWFdMMTA3Q2MtN1FaMldTYmVPYjNzUSIsImtpZCI6InlFVXdtWFdMMTA3Q2MtN1FaMldTYmVPYjNzUSJ9.eyJhdWQiOiJodHRwczovL2FpLmF6dXJlLmNvbSIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0L2I3MjdhNTMwLWEwZDUtNGZiOC1iZDQwLWQ4Zjk3NjNlOTdkYi8iLCJpYXQiOjE3NjI0MjM4NDQsIm5iZiI6MTc2MjQyMzg0NCwiZXhwIjoxNzYyNDI4NjYwLCJhY3IiOiIxIiwiYWlvIjoiQVhRQWkvOGFBQUFBV090bElmUkdBS2thNFd4dG4zUGdDdnY4dFpxeUpDTmtNeHpvQnI1ekN3WU93RE9wNXorVTJ1b1JTTWJtaEJFRVgyRTRBWGxsWVUxVXlnQlJ6SHh4cG8wUUhiVGFHdGFYZ09kSHpFUUFBeXlRVDdZbDZZTC90NUZwcTNzUUMzS3lqeFhJbnRkNzdTdVpwdGVYMWxXVDBBPT0iLCJhbXIiOlsicHdkIiwicnNhIiwibWZhIl0sImFwcGlkIjoiYjY3N2MyOTAtY2Y0Yi00YThlLWE2MGUtOTFiYTY1MGE0YWJlIiwiYXBwaWRhY3IiOiIwIiwiZGV2aWNlaWQiOiI4MmEwNzUwNS1hNmEwLTQ2OWEtOGQxZi03OGMzMmUyZjAzNjAiLCJmYW1pbHlfbmFtZSI6IlNpbmdoIiwiZ2l2ZW5fbmFtZSI6IlNpbXJhbmppdCIsImdyb3VwcyI6WyJhMjA2MWIwMC1hMDYyLTQ5MDUtYjU5Mi0wMWViMWY3Mzc0YWQiLCJhNzdiZmYwMy03NmQ5LTRhNDQtYjJjOC1kNTM4MmI2ZDk2MzYiLCI3MWYxODcxMS1kOTcyLTQ2OTItOTIxMy1jNjQzNDQzNzE3MWMiLCI2ZTg5YTgxMS1kZGNhLTRkNjQtODc2Mi1kZjA4ODNmMzIwMjIiLCJkMWVlMTMxNS0yNTY4LTQzMzYtYTFiNS1iOWFiNzQ4YTAxNDEiLCI1ZTE2NjQxNy02NzRjLTQxNzItOGY0Ny0wZWZkZjgxNjA1YzUiLCJiYzAwZWExOS0wNjAxLTRlZjctOWYyOS04NzNjNzU1NDEzZjEiLCIwNDU5YmIyNS1lNzJlLTQxMTItOGMwZS1kOGQ0NDg2ZTNjNTYiLCJkNGQ2YmUyNS1iZWMxLTRlZWUtYjQ5Mi0zN2E2MTRiNDAzNWIiLCI0NGQ3NzczNC0wOTljLTQ3NjYtOWE4YS05YzE3YTc2ODY3MzUiLCJiMGI0MzE0My0zMDYxLTQ4YWUtYjAyNi05ZTc1ZDAzOGVkYzYiLCI5MzYwOGE0NS1mMWI1LTRjM2UtOTY2NC0yNWE2ZmVhZjhiODgiLCIzZDI5NTE0OC1jNTllLTQyZGQtOGUwMy1mZGU3ZjY4NWM0MGIiLCJmMmJkM2I1Ni1kNzYwLTRmMWUtOGUwNC02ZWQ0YmFlY2I0MTEiLCI3MWFjMGU1Ny1mN2MxLTQ3ODItOTcxNS0wYzdiMzYyZjc5Y2EiLCJhZWMyZTQ1Yy1mNzZlLTRiN2MtYWUyMC04NDQ1ZGUyZjdkODIiLCJjZGM5YTE2MC1lNWU5LTRjNjYtYmRjZC0zMDkwNjlhNDMzMGQiLCIyNGIyY2M2NC03OTZlLTQ0OWQtOTY0Yi0wNTAxNmFjYTg2MGIiLCI1NmEzM2M3Yy1jZmIzLTQ5NjgtYTMwYS02NTMwYmFlZGZiOTAiLCIzZGVhYzU4YS1lNTA1LTQwZjItYWE0NS01NzUzZWEyMTRiN2YiLCJkNDNlNjU5Yi1kMmQ0LTRiYjEtOWNkOC04ZWY0ZTc1YTFiZjIiLCJmM2RjZDU5ZS1iNWNmLTRiNmMtOTMzMi0yOWJmYTdjMzcxY2UiLCI2Y2U2NTNhMS1kNTlmLTRiNzMtODc4Ny05ZGJmNmY0MDY2NTgiLCI3NjUxMTlhZC1jNmMyLTQwNjMtYTA0YS1iODJiYzc2NGM1ZGQiLCI3YmI4YjBiNS1lMGRmLTQ3MWUtOTAxYS0xZTY1ZmQzY2VlZjUiLCJhYjNjZjNiYi1mOGFkLTRlZDUtYTNlMS0xNjhlNWY1OGViNjYiLCJjY2YwYzJjOC1hN2NjLTQ0N2UtODhhMi0zNDM2OWVjNTg1NzgiLCJmNTUzZDNlNy0zNTRmLTQyNmItYjBlZC00NDczMjdmZWRmZDEiLCI4ZWNhOTllZC1mMzZlLTRhYmQtODUxZC0zMTBjNDkyMWNjYWYiLCIwZTI1ODJmOS05MGNmLTRmZWMtOWU3Mi1lZmJjYTI2NmJkMzYiLCJjYzQ3NGVmYS0xYjE5LTRhYjMtOTA0MC1kMmU0MzU0ZDE1MWQiXSwiaWR0eXAiOiJ1c2VyIiwiaXBhZGRyIjoiMTE2LjUwLjU5LjIwMSIsIm5hbWUiOiJTaW1yYW5qaXQgU2luZ2giLCJvaWQiOiJkY2U3OTI3Mi0yZmMxLTQzOTAtYmE5OS1jZGY4Mzc1ZmVkMzUiLCJvbnByZW1fc2lkIjoiUy0xLTUtMjEtMjcyNDAzMjMxNC0zODQwMDY5Njk1LTI0Mjk3NjEwMTItMjc5MjkiLCJwdWlkIjoiMTAwMzNGRkZBQkNBMDY3RiIsInJoIjoiMS5BWElBTUtVbnQ5V2d1RS05UU5qNWRqNlgyMTl2cGhqZjJ4ZE1uZGNXTkhFcW5MNHdBWGR5QUEuIiwic2NwIjoidXNlcl9pbXBlcnNvbmF0aW9uIiwic2lkIjoiMDBhODJlYzktYWFhOS03MGJiLTg3NGItZDAzM2UyM2Y3YTQ2Iiwic3ViIjoicTQtSkVYcXFTeWVlQ3dWU1lUT0pjc0RCUVlNek1ua1dZOTQ3dTMwR1I3MCIsInRpZCI6ImI3MjdhNTMwLWEwZDUtNGZiOC1iZDQwLWQ4Zjk3NjNlOTdkYiIsInVuaXF1ZV9uYW1lIjoiU2ltcmFuaml0LlNpbmdoQGNvZm9yZ2UuY29tIiwidXBuIjoiU2ltcmFuaml0LlNpbmdoQGNvZm9yZ2UuY29tIiwidXRpIjoiZ3Y4Z1k2VDhPVTZscXlGUzR4MURBQSIsInZlciI6IjEuMCIsInhtc19hY3RfZmN0IjoiNSAzIiwieG1zX2Z0ZCI6ImNPOUVlQ2V1SXd5QmVxMjd5Y3BaQXdSMmFJU3F5SXo0YzVnNkVMQkFubndCYTI5eVpXRnpiM1YwYUMxa2MyMXoiLCJ4bXNfaWRyZWwiOiIxMiAxIiwieG1zX3N1Yl9mY3QiOiIzIDgifQ.D_lW4a0Rm8e8m56v1SIhWFcdk6Uml7-VbLbTkU2paZ6K17XfXPTHxmmnhoSh4RPDr1VCL2SbpBfgn-ySfi_83nYMSi_Rq41roX_Cx2X3rojDJ19bvksSciIPYKkUyNuqsmHU9gSxw5nun8u29EfdB1JhryFhjQkNQUYs2MUrwHZD0jI3CBNZ81mr5eUZr1cpUlawo0H0mjQ3CCKTUt2vG6a7JNClGHHm0Yx-WVlssm-e2DvOFBZDDOCutIKJqI0-Dc7VT-iGWM5vfAzxY-VoIe7dsYMlqUe1I1h96KBB2Fho0_iiACbXwxUzg_SNbwz6BwEP-Z09HyNJiJw4RHdvwQ";
                var credential = new AccessTokenCredential(accessToken);

                var projectClient = new AIProjectClient(endpoint, credential);

                PersistentAgentsClient agentsClient = projectClient.GetPersistentAgentsClient();
                PersistentAgent agent = agentsClient.Administration.GetAgent("asst_usnOZBO5DnST40JubrRtdJ3r");
                PersistentAgentThread thread;
                if (string.IsNullOrEmpty(tId) || tId == "Empty")
                {
                   thread = agentsClient.Threads.CreateThread();
                }
                else
                {
                    thread = agentsClient.Threads.GetThread(tId);
                }
                
                PersistentThreadMessage messageResponse = agentsClient.Messages.CreateMessage(
                    thread.Id,
                    MessageRole.User,
                    userInput);

                ThreadRun run = agentsClient.Runs.CreateRun(thread.Id, agent.Id);

                do
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(500));
                    run = agentsClient.Runs.GetRun(thread.Id, run.Id);
                    List<ToolOutput> toolOutputs = [];
                    if (run.Status == RunStatus.RequiresAction && run.RequiredAction is SubmitToolOutputsAction submitToolOutputsAction)
                    {
                        var toolApprovals = new List<ToolOutput>();
                        foreach (RequiredToolCall toolCall in submitToolOutputsAction.ToolCalls)
                        {
                            toolOutputs.Add(await GetResolvedToolOutputAsync(toolCall, "https://mymcptest-apavevbrc5gudscm.canadacentral-01.azurewebsites.net/api/mcp"));
                        }

                        run = agentsClient.Runs.SubmitToolOutputsToRun(thread.Id, run.Id, toolOutputs);
                    }
                }
                while (run.Status == RunStatus.Queued
                    || run.Status == RunStatus.InProgress
                    || run.Status == RunStatus.RequiresAction);

                Pageable<PersistentThreadMessage> messages = agentsClient.Messages.GetMessages(thread.Id, order: ListSortOrder.Ascending);
                foreach (PersistentThreadMessage threadMessage in messages)
                {
                    //messagesOut.Add($"{threadMessage.CreatedAt:yyyy-MM-dd HH:mm:ss} - {threadMessage.Role,10}: ");
                    if (threadMessage.Role.ToString() == "assistant")
                    {
                        foreach (MessageContent contentItem in threadMessage.ContentItems)
                        {
                            if (contentItem is MessageTextContent textItem)
                            {
                                messagesOut.Add(textItem.Text);
                            }
                            else if (contentItem is MessageImageFileContent imageFileItem)
                            {
                                messagesOut.Add($"<image from ID: {imageFileItem.FileId}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                messagesOut.Add(ex.Message);
            }

            return messagesOut;
        }

        private static async Task<ToolOutput> GetResolvedToolOutputAsync(RequiredToolCall toolCall, string mcpServerUrl)
        {
            // Handle different tool types
            if (toolCall is RequiredFunctionToolCall functionToolCall)
            {
                var functionName = functionToolCall.Name;
                var arguments = functionToolCall.Arguments;

                try
                {
                    using var httpClient = new HttpClient();

                    var arguments1 = @"{
                ""jsonrpc"": ""2.0"",
  ""id"": ""1"",
  ""method"": ""tools/call"",
  ""params"": {
    ""name"": ""GetPolicy"",
    ""arguments"": +arguments+}}";

                    arguments = arguments1.Replace("+arguments+", arguments);
                    var requestBody = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(arguments);
                    var jsonContent = System.Text.Json.JsonSerializer.Serialize(requestBody);
                    var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                    // Call the specific function endpoint
                    var functionUrl = mcpServerUrl.Replace("/runtime/webhooks/mcp/sse", $"/api/{functionName}");
                    //logger.LogInformation("Calling MCP function: {Url}", functionUrl.Replace(mcpServerUrl.Split('?')[1], "[REDACTED]"));
                    HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, functionUrl);
                    httpRequestMessage.Content = content;
                    httpRequestMessage.Headers.Accept.Clear();
                    httpRequestMessage.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    httpRequestMessage.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/event-stream"));
                    var response = await httpClient.SendAsync(httpRequestMessage);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();

                        // Parse the response to extract the content
                        responseContent = responseContent.Replace("\r", "").Replace("\n", "").Replace("event: messagedata:", "");
                        var responseObj = JsonConvert.DeserializeObject<JObject>(responseContent);
                        var responseObj1 = JsonConvert.DeserializeObject<JObject>(responseObj["result"]["content"][0]["text"].ToString());
                        responseObj1["success"] = true;
                        responseObj1["message"] = "Policy Returned by Exact in Test Agent";
                        responseObj["result"]["content"][0]["text"] = JsonConvert.SerializeObject(responseObj1);
                        var result = JsonConvert.SerializeObject(responseObj);

                        return new ToolOutput(toolCall.Id, result);
                    }
                    else
                    {
                        return new ToolOutput(toolCall.Id, $"Error calling {functionName}: HTTP {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    return new ToolOutput(toolCall.Id, $"Error calling {functionName}: {ex.Message}");
                }
            }

            // For non-function tool calls
            return new ToolOutput(toolCall.Id, "Tool call processed successfully");
        }
    }

    public class AccessTokenCredential : TokenCredential
    {
        private readonly string _accessToken;

        public AccessTokenCredential(string accessToken)
        {
            _accessToken = accessToken;
        }

        public override AccessToken GetToken(TokenRequestContext requestContext, System.Threading.CancellationToken cancellationToken)
        {
            return new AccessToken(_accessToken, DateTimeOffset.UtcNow.AddHours(1)); // Set token expiration as needed
        }

        public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, System.Threading.CancellationToken cancellationToken)
        {
            return await Task.FromResult(new AccessToken(_accessToken, DateTimeOffset.UtcNow.AddHours(1))); // Set token expiration as needed
        }
    }
}
