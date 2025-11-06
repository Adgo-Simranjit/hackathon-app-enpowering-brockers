//using Azure.AI.Agents.Persistent;
//using Azure.AI.Projects;
//using Azure.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AgentDemo
//{
//    internal class Class1
//    {

//        using Azure.AI.Agents.Persistent;
//using Azure.Identity;
//using Azure.AI.Projects;


//namespace AgentDemo
//    {
//        class Program
//        {
//            static async Task Main(string[] args)
//            {
//                await RunAgentConversation();
//            }
//            static async Task RunAgentConversation()
//            {


//                //string filePath = @"C:\Path\To\File.xlsx";
//                //var fileStream = File.OpenRead(filePath);

//                //var endpoint = new Uri("https://demo-cm-hackathon.services.ai.azure.com/api/projects/demoProject");
//                //var credential = new DefaultAzureCredential();

//                //// Initialize project and agents client
//                //var projectClient = new AIProjectClient(endpoint, credential);
//                //var agentsClient = projectClient.GetPersistentAgentsClient();

//                //// Step 1: Create a thread
//                //var threadResponse = await agentsClient.CreateThreadAsync();

//                //// Step 2: Create a message with the file
//                //var messageOptions = new CreateMessageOptions
//                //{
//                //    Role = "user",
//                //    Content = "Please convert this Excel file to JSON.",
//                //    Files = { BinaryData.FromStream(fileStream) }
//                //};

//                //await agentsClient.CreateMessageAsync(threadResponse.Value.Id, messageOptions);

//                //// Step 3: Create and execute a run
//                //var runOptions = new CreateRunOptions
//                //{
//                //    AgentId = "asst_o3ufnEhQVRvo3PuDGHzUrVwr"
//                //};

//                //var runResponse = await agentsClient.CreateRunAsync(threadResponse.Value.Id, runOptions);

//                //// Step 4: Wait for run to complete (polling or event-based)
//                //var completedRun = await agentsClient.WaitForRunCompletionAsync(threadResponse.Value.Id, runResponse.Value.Id);

//                //// Step 5: Retrieve the output
//                //var messages = await agentsClient.GetMessagesAsync(threadResponse.Value.Id);
//                //foreach (var message in messages.Value)
//                //{
//                //    Console.WriteLine($"Message from {message.Role}: {message.Content}");
//                //}

//                //Path to your Excel file
//                string filePath = @"C:\Path\To\File.xlsx";

//                // Read file into stream
//                byte[] fileBytes = File.ReadAllBytes(filePath);
//                var fileStream = new MemoryStream(fileBytes);

//                // Create input payload using BinaryData
//                var input = new Dictionary<string, BinaryData>
//            {
//                { "file", BinaryData.FromStream(fileStream) }
//            };

//                // Set up Azure AI Foundry project and agent
//                var endpoint = new Uri("https://demo-cm-hackathon.services.ai.azure.com/api/projects/demoProject");
//                var credential = new DefaultAzureCredential();

//                AIProjectClient projectClient = new(endpoint, credential);
//                PersistentAgentsClient agentsClient = projectClient.GetPersistentAgentsClient();

//                // Get the agent by ID
//                PersistentAgent agentClient = agentsClient.Administration.GetAgent("asst_o3ufnEhQVRvo3PuDGHzUrVwr");

//                // Invoke the agent with the input
//                var response = await agentClient.InvokeAsync(input);

//                // Extract and print the JSON output
//                if (response.Output.TryGetValue("json_data", out var jsonData))
//                {
//                    Console.WriteLine("JSON Output:");
//                    Console.WriteLine(jsonData.ToString());
//                }
//                else
//                {
//                    Console.WriteLine("No 'json_data' found in the response.");
//                }



//                //PersistentAgentThread thread = agentsClient.Threads.CreateThread();
//                //Console.WriteLine($"Created thread, ID: {thread.Id}");

//                //PersistentThreadMessage messageResponse = agentsClient.Messages.CreateMessage(
//                //    thread.Id,
//                //    MessageRole.User,
//                //    "Hi ExcelToJsonAgent");

//                //ThreadRun run = agentsClient.Runs.CreateRun(
//                //    thread.Id,
//                //    agent.Id);

//                //// Poll until the run reaches a terminal status
//                //do
//                //{
//                //    await Task.Delay(TimeSpan.FromMilliseconds(500));
//                //    run = agentsClient.Runs.GetRun(thread.Id, run.Id);
//                //}
//                //while (run.Status == RunStatus.Queued
//                //    || run.Status == RunStatus.InProgress);
//                //if (run.Status != RunStatus.Completed)
//                //{
//                //    throw new InvalidOperationException($"Run failed or was canceled: {run.LastError?.Message}");
//                //}

//                //Pageable<PersistentThreadMessage> messages = agentsClient.Messages.GetMessages(
//                //    thread.Id, order: ListSortOrder.Ascending);

//                //// Display messages
//                //foreach (PersistentThreadMessage threadMessage in messages)
//                //{
//                //    Console.Write($"{threadMessage.CreatedAt:yyyy-MM-dd HH:mm:ss} - {threadMessage.Role,10}: ");
//                //    foreach (MessageContent contentItem in threadMessage.ContentItems)
//                //    {
//                //        if (contentItem is MessageTextContent textItem)
//                //        {
//                //            Console.Write(textItem.Text);
//                //        }
//                //        else if (contentItem is MessageImageFileContent imageFileItem)
//                //        {
//                //            Console.Write($"<image from ID: {imageFileItem.FileId}");
//                //        }
//                //        Console.WriteLine();
//                //    }
//                //}
//            }
//        }
//    }

//    //using Azure.Core;
//    //using Azure.Identity;
//    //using Newtonsoft.Json;
//    //using Newtonsoft.Json.Linq;
//    //using System.Net.Http.Headers;
//    //using System.Text;

//    //namespace AgentDemo
//    //{
//    //    class Program
//    //    {
//    //        static async Task Main(string[] args)
//    //        {
//    //            var obj =  await CreateAgent();
//    //            string id = "";
//    //            await CallAgent(id);

//    //            await CreateAgentStandard();
//    //        }

//    //        private static async Task CreateAgentStandard()
//    //        {
//    //            try
//    //            {

//    //                //https://demo-cm-hackathon.services.ai.azure.com/api/projects/demoProject
//    //                //string foundryEndpoint = "https://demo-cm-hackathon.services.ai.azure.com/api/projects/demoProject";//"https://demo-cm-hackathon.services.ai.azure.com/";
//    //                string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6InlFVXdtWFdMMTA3Q2MtN1FaMldTYmVPYjNzUSIsImtpZCI6InlFVXdtWFdMMTA3Q2MtN1FaMldTYmVPYjNzUSJ9.eyJhdWQiOiJodHRwczovL2FpLmF6dXJlLmNvbSIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0L2I3MjdhNTMwLWEwZDUtNGZiOC1iZDQwLWQ4Zjk3NjNlOTdkYi8iLCJpYXQiOjE3NjIyNDcxMTYsIm5iZiI6MTc2MjI0NzExNiwiZXhwIjoxNzYyMjUyNzI0LCJhY3IiOiIxIiwiYWlvIjoiQVhRQWkvOGFBQUFBZ3Y2anVTcUNBeWJxV3JndkZtSDZuWEpKSE1WRVA0RGVCaUl1bmw4RDY4OXJ3blVRV0xaQlpWQXZEc0JjRXJYbHcwb09lUnh1YzhhajhJY3l2bGI2a08rMllGcFFVcDRnMlNhOVRoNjZacG5RSDhNTVdBZ2hFZk04d3Exc0xpMVllUHdoK1ZYd3ZwL3g4RGVjZjlKV3Z3PT0iLCJhbXIiOlsicHdkIiwibWZhIl0sImFwcGlkIjoiYjY3N2MyOTAtY2Y0Yi00YThlLWE2MGUtOTFiYTY1MGE0YWJlIiwiYXBwaWRhY3IiOiIwIiwiZmFtaWx5X25hbWUiOiJHdXJ1cmFuaSIsImdpdmVuX25hbWUiOiJDaGFuZHJhIiwiZ3JvdXBzIjpbImE3N2JmZjAzLTc2ZDktNGE0NC1iMmM4LWQ1MzgyYjZkOTYzNiIsImQxZWUxMzE1LTI1NjgtNDMzNi1hMWI1LWI5YWI3NDhhMDE0MSIsImJjMDBlYTE5LTA2MDEtNGVmNy05ZjI5LTg3M2M3NTU0MTNmMSIsIjA0NTliYjI1LWU3MmUtNDExMi04YzBlLWQ4ZDQ0ODZlM2M1NiIsImQ0ZDZiZTI1LWJlYzEtNGVlZS1iNDkyLTM3YTYxNGI0MDM1YiIsIjQ0ZDc3NzM0LTA5OWMtNDc2Ni05YThhLTljMTdhNzY4NjczNSIsImIwYjQzMTQzLTMwNjEtNDhhZS1iMDI2LTllNzVkMDM4ZWRjNiIsImYyYmQzYjU2LWQ3NjAtNGYxZS04ZTA0LTZlZDRiYWVjYjQxMSIsIjcxYWMwZTU3LWY3YzEtNDc4Mi05NzE1LTBjN2IzNjJmNzljYSIsImFlYzJlNDVjLWY3NmUtNGI3Yy1hZTIwLTg0NDVkZTJmN2Q4MiIsIjBkODdiNjYxLTk1ZTAtNGZlNS1hYzljLWNhY2YwZDljNTRlNSIsIjNkZWFjNThhLWU1MDUtNDBmMi1hYTQ1LTU3NTNlYTIxNGI3ZiIsImYzZGNkNTllLWI1Y2YtNGI2Yy05MzMyLTI5YmZhN2MzNzFjZSIsIjc2NTExOWFkLWM2YzItNDA2My1hMDRhLWI4MmJjNzY0YzVkZCIsImNjZjBjMmM4LWE3Y2MtNDQ3ZS04OGEyLTM0MzY5ZWM1ODU3OCIsImY1NTNkM2U3LTM1NGYtNDI2Yi1iMGVkLTQ0NzMyN2ZlZGZkMSIsImNjNDc0ZWZhLTFiMTktNGFiMy05MDQwLWQyZTQzNTRkMTUxZCJdLCJpZHR5cCI6InVzZXIiLCJpcGFkZHIiOiIxMjguMTg1LjE4Ni42NiIsIm5hbWUiOiJDaGFuZHJhIE1vaGFuIEd1cnVyYW5pIiwib2lkIjoiOWQ3YzFiMTEtYjBjYy00MjkyLTlkM2EtZDAxY2QyN2M0MThhIiwib25wcmVtX3NpZCI6IlMtMS01LTIxLTI3MjQwMzIzMTQtMzg0MDA2OTY5NS0yNDI5NzYxMDEyLTEwMDA4NSIsInB1aWQiOiIxMDAzMjAwMjU2MTMwMzkyIiwicmgiOiIxLkFYSUFNS1VudDlXZ3VFLTlRTmo1ZGo2WDIxOXZwaGpmMnhkTW5kY1dOSEVxbkw0d0FXbHlBQS4iLCJzY3AiOiJ1c2VyX2ltcGVyc29uYXRpb24iLCJzaWQiOiIwMDljNDdlOS00ZTlkLWEzMjgtMTk1ZC00ZThkMDk1OGE5N2YiLCJzdWIiOiI3MW5KbUtNQzhHaWVUcXRiR3o2cTNXQ0FLbkhIdUJ0SkpoRkJ4WDhrTncwIiwidGlkIjoiYjcyN2E1MzAtYTBkNS00ZmI4LWJkNDAtZDhmOTc2M2U5N2RiIiwidW5pcXVlX25hbWUiOiJDaGFuZHJhLkd1cnVyYW5pQGNvZm9yZ2UuY29tIiwidXBuIjoiQ2hhbmRyYS5HdXJ1cmFuaUBjb2ZvcmdlLmNvbSIsInV0aSI6IkFuVUhFVHpUNjBtUmY1N2RKdm9NQUEiLCJ2ZXIiOiIxLjAiLCJ4bXNfYWN0X2ZjdCI6IjMgNSIsInhtc19mdGQiOiJkaGQ4bzhxa1A1dDhMeVJtNGVRSEZCYUwzWWQtYXNJelpPLXR5SE1peXF3QmEyOXlaV0Z6YjNWMGFDMWtjMjF6IiwieG1zX2lkcmVsIjoiMzAgMSIsInhtc19zdWJfZmN0IjoiNCAzIn0.HICRmxgLuo7nkU_F6koVd6WcFSWFPk30BrROsYMeJcXB6cI-RDKBo3T9Jqad4hI-lOlu_5rE6XpA_yj_uGvLODI7DoTutgeZC_gLjE4359x_IiOA518EHkLA19rPn2lRQz2kof7AwFHU8crcgTE16x3OuD6jwNZARTB3T313M-I8cIW4Wr0p2CoW9dWN3-FSC09AfAg3NqlP0BQL5adddc6xb152X17EaL3YglcGPRJfWJZwD8R1Od--c6OOf1Wxbf5xDvypH0FNi6g2QXMXLjeIMUZHEOq2nGMzcKpLdAf-CBMfj_FdvzNSfzJ8K7TUDvdO_iky6cRwX9ouroTj9A"; // Get via Azure CLI: az account get-access-token

//    //                var url = "https://demo-cm-hackathon.services.ai.azure.com/api/projects/demoProject/assistants?api-version=v1";

//    //                var client = new HttpClient();
//    //                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

//    //                var payload = new
//    //                {
//    //                    model = "gpt-4o",
//    //                    name = "MyAgentAddition",
//    //                    instructions = "You are a helpful assistant.",
//    //                    temperature = 0.7
//    //                };

//    //                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
//    //                var response = await client.PostAsync(url, content);

//    //                string result = await response.Content.ReadAsStringAsync();

//    //                Console.WriteLine($"Response: {result}");
//    //            }
//    //            catch (Exception ex)
//    //            {
//    //                Console.WriteLine("Here is the error message", ex.Message);
//    //            }
//    //        }

//    //        private static async Task<string> CreateAgent() 
//    //        {
//    //            var tenantId = "<your-tenant-id>";
//    //            var clientId = "<your-client-id>";
//    //            var clientSecret = "<your-client-secret>";
//    //            var projectName = "demoProject";
//    //            var resourceName = "demo-cm-hackathon";

//    //            var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
//    //            //var token = await credential.GetTokenAsync(new TokenRequestContext(new[] { "https://ai.azure.com/.default" }));
//    //            var token = "";
//    //            var client = new HttpClient();
//    //            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

//    //            var url = $"https://{resourceName}.services.ai.azure.com/api/projects/{projectName}/assistants?api-version=v1";

//    //            var payload = new
//    //            {
//    //                model = "gpt-4o",
//    //                name = "AdderAgent",
//    //                instructions = "You are an assistant that adds two numbers provided by the user.",
//    //                temperature = 0.2
//    //            };

//    //            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
//    //            var response = await client.PostAsync(url, content);
//    //            var responseBody = await response.Content.ReadAsStringAsync();

//    //            Console.WriteLine(responseBody);
//    //            return responseBody;

//    //        }

//    //        private static async Task CallAgent(string newAgent)
//    //        {
//    //            var client = new HttpClient();
//    //            var projectName = "demoProject";
//    //            var resourceName = "demo-cm-hackathon";

//    //            string agentId = newAgent;
//    //            var runUrl = $"https://{resourceName}.services.ai.azure.com/api/projects/{projectName}/assistants/{agentId}/runs?api-version=v1";

//    //            var runPayload = new
//    //            {
//    //                input = new
//    //                {
//    //                    messages = new[]
//    //                    {
//    //            new { role = "user", content = "Add 23 and 42" }
//    //                }
//    //                }
//    //            };

//    //            var runContent = new StringContent(JsonConvert.SerializeObject(runPayload), Encoding.UTF8, "application/json");
//    //            var runResponse = await client.PostAsync(runUrl, runContent);
//    //            var runResult = await runResponse.Content.ReadAsStringAsync();

//    //            Console.WriteLine(runResult);

//    //        }
//    //    }
//    //}

//}
//}


//////////////////////////////////////////
///
//using System;
//using System.IO;
//using System.Threading.Tasks;
//using Azure.Identity;
//using Azure.AI.Projects;
//using Azure.AI.Agents.Persistent;
//using Azure;
//using Azure.Core;

//class Program
//{
//    static async Task Main(string[] args)
//    {
//        await RunAgentConversation();
//    }

//    static async Task RunAgentConversation()
//    {
//        try
//        {

//            var endpoint = new Uri("https://demo-cm-hackathon.services.ai.azure.com/api/projects/demoProject");
//            string accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6InlFVXdtWFdMMTA3Q2MtN1FaMldTYmVPYjNzUSIsImtpZCI6InlFVXdtWFdMMTA3Q2MtN1FaMldTYmVPYjNzUSJ9.eyJhdWQiOiJodHRwczovL2FpLmF6dXJlLmNvbSIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0L2I3MjdhNTMwLWEwZDUtNGZiOC1iZDQwLWQ4Zjk3NjNlOTdkYi8iLCJpYXQiOjE3NjIzMjA0MzcsIm5iZiI6MTc2MjMyMDQzNywiZXhwIjoxNzYyMzI1MDAzLCJhY3IiOiIxIiwiYWlvIjoiQWFRQVcvOGFBQUFBU1hBdDBUQXVGVVpoTjZFTHV1dmpzc0hUNStMM0JNQVpISFVCY3VXdVdyOGlhbVI3Sld3YXBvY0FOZXhnbWtjWGE1cUZJM0lMWFNHbkhvRkZDblVDMEJidTNHQWpNWkxhVlluVHdOVXNLTGxLRzF3MjREdUdvZTNGeFlWeVF5cC8zR2pOK2s2RGFlcHhUMzR5VjJNZEJZVHZQQW1pYnlxOGRtZFdNbHF1clB0dXNDZXd0UTVQYTNUVXNrWUZ2Vk1qNGJxTzU3eTNNSzF1MDFEaVlseXZuZz09IiwiYW1yIjpbInB3ZCIsIm1mYSJdLCJhcHBpZCI6ImI2NzdjMjkwLWNmNGItNGE4ZS1hNjBlLTkxYmE2NTBhNGFiZSIsImFwcGlkYWNyIjoiMCIsImZhbWlseV9uYW1lIjoiR3VydXJhbmkiLCJnaXZlbl9uYW1lIjoiQ2hhbmRyYSIsImdyb3VwcyI6WyJhNzdiZmYwMy03NmQ5LTRhNDQtYjJjOC1kNTM4MmI2ZDk2MzYiLCJkMWVlMTMxNS0yNTY4LTQzMzYtYTFiNS1iOWFiNzQ4YTAxNDEiLCJiYzAwZWExOS0wNjAxLTRlZjctOWYyOS04NzNjNzU1NDEzZjEiLCIwNDU5YmIyNS1lNzJlLTQxMTItOGMwZS1kOGQ0NDg2ZTNjNTYiLCJkNGQ2YmUyNS1iZWMxLTRlZWUtYjQ5Mi0zN2E2MTRiNDAzNWIiLCI0NGQ3NzczNC0wOTljLTQ3NjYtOWE4YS05YzE3YTc2ODY3MzUiLCJiMGI0MzE0My0zMDYxLTQ4YWUtYjAyNi05ZTc1ZDAzOGVkYzYiLCJmMmJkM2I1Ni1kNzYwLTRmMWUtOGUwNC02ZWQ0YmFlY2I0MTEiLCI3MWFjMGU1Ny1mN2MxLTQ3ODItOTcxNS0wYzdiMzYyZjc5Y2EiLCJhZWMyZTQ1Yy1mNzZlLTRiN2MtYWUyMC04NDQ1ZGUyZjdkODIiLCIwZDg3YjY2MS05NWUwLTRmZTUtYWM5Yy1jYWNmMGQ5YzU0ZTUiLCIzZGVhYzU4YS1lNTA1LTQwZjItYWE0NS01NzUzZWEyMTRiN2YiLCJmM2RjZDU5ZS1iNWNmLTRiNmMtOTMzMi0yOWJmYTdjMzcxY2UiLCI3NjUxMTlhZC1jNmMyLTQwNjMtYTA0YS1iODJiYzc2NGM1ZGQiLCJjY2YwYzJjOC1hN2NjLTQ0N2UtODhhMi0zNDM2OWVjNTg1NzgiLCJmNTUzZDNlNy0zNTRmLTQyNmItYjBlZC00NDczMjdmZWRmZDEiLCJjYzQ3NGVmYS0xYjE5LTRhYjMtOTA0MC1kMmU0MzU0ZDE1MWQiXSwiaWR0eXAiOiJ1c2VyIiwiaXBhZGRyIjoiMTI4LjE4NS4xODYuNjYiLCJuYW1lIjoiQ2hhbmRyYSBNb2hhbiBHdXJ1cmFuaSIsIm9pZCI6IjlkN2MxYjExLWIwY2MtNDI5Mi05ZDNhLWQwMWNkMjdjNDE4YSIsIm9ucHJlbV9zaWQiOiJTLTEtNS0yMS0yNzI0MDMyMzE0LTM4NDAwNjk2OTUtMjQyOTc2MTAxMi0xMDAwODUiLCJwdWlkIjoiMTAwMzIwMDI1NjEzMDM5MiIsInJoIjoiMS5BWElBTUtVbnQ5V2d1RS05UU5qNWRqNlgyMTl2cGhqZjJ4ZE1uZGNXTkhFcW5MNHdBV2x5QUEuIiwic2NwIjoidXNlcl9pbXBlcnNvbmF0aW9uIiwic2lkIjoiMDA5YzQ3ZTktNGU5ZC1hMzI4LTE5NWQtNGU4ZDA5NThhOTdmIiwic3ViIjoiNzFuSm1LTUM4R2llVHF0Ykd6NnEzV0NBS25ISHVCdEpKaEZCeFg4a053MCIsInRpZCI6ImI3MjdhNTMwLWEwZDUtNGZiOC1iZDQwLWQ4Zjk3NjNlOTdkYiIsInVuaXF1ZV9uYW1lIjoiQ2hhbmRyYS5HdXJ1cmFuaUBjb2ZvcmdlLmNvbSIsInVwbiI6IkNoYW5kcmEuR3VydXJhbmlAY29mb3JnZS5jb20iLCJ1dGkiOiJ4OEk3M0poQ1dVQ2lOdFhlQldra0FRIiwidmVyIjoiMS4wIiwieG1zX2FjdF9mY3QiOiIzIDUiLCJ4bXNfZnRkIjoiLW1Jd3hBS3NRYkZudVZVYzBCR1JsY0NQNjNhZWNtM3kxMlEtZHYyQVdKd0JZWE5wWVhOdmRYUm9aV0Z6ZEMxa2MyMXoiLCJ4bXNfaWRyZWwiOiIxIDIyIiwieG1zX3N1Yl9mY3QiOiIzIDE0In0.hDngUQuG-FPRw0EPScXKFig8Ofm92kpAq5IAKb4_aEHxbZg2_Zy1_7puiHwSjtGXA0hrWm2CTp9VD8w01e5agXJ0WFJDZEPuSdiGTTR6HlJ-D_dVBF6Edhj7FRjfAB-bmcAsZ1K9Jp2m36_A7BBi675J3MNfz-IZMtpMyOp5WYsHuwYF4SKHUA_mx6_zQqtkOrh0bTqRBqhWxIk25DK-siuxdwnExaSXRQrX7V5ysWelwCq8o43xR4RqvGXuSmiIsln-FOSeM0gS8htz6e2ly9pP_udUAiS9kA2fUD_gkeOBWynR-7HAQRPo6Zd-8xXvNRjCqg474E0Gm556VCvvIw";
//            var credential = new AccessTokenCredential(accessToken);

//            //var endpoint = new Uri("https://<your-project-endpoint>");
//            var projectClient = new AIProjectClient(endpoint, credential);



//            //AIProjectClient projectClient = new(endpoint, new DefaultAzureCredential());

//            PersistentAgentsClient agentsClient = projectClient.GetPersistentAgentsClient();

//            PersistentAgent agent = agentsClient.Administration.GetAgent("asst_o3ufnEhQVRvo3PuDGHzUrVwr");

//            PersistentAgentThread thread = agentsClient.Threads.CreateThread();
//            Console.WriteLine($"Created thread, ID: {thread.Id}");

//            PersistentThreadMessage messageResponse = agentsClient.Messages.CreateMessage(
//                thread.Id,
//                MessageRole.User,
//                "Hi ExcelToJsonAgent");

//            ThreadRun run = agentsClient.Runs.CreateRun(
//                thread.Id,
//                agent.Id);

//            // Poll until the run reaches a terminal status
//            do
//            {
//                await Task.Delay(TimeSpan.FromMilliseconds(500));
//                run = agentsClient.Runs.GetRun(thread.Id, run.Id);
//            }
//            while (run.Status == RunStatus.Queued
//                || run.Status == RunStatus.InProgress);
//            if (run.Status != RunStatus.Completed)
//            {
//                throw new InvalidOperationException($"Run failed or was canceled: {run.LastError?.Message}");
//            }

//            Pageable<PersistentThreadMessage> messages = agentsClient.Messages.GetMessages(
//                thread.Id, order: ListSortOrder.Ascending);

//            // Display messages
//            foreach (PersistentThreadMessage threadMessage in messages)
//            {
//                Console.Write($"{threadMessage.CreatedAt:yyyy-MM-dd HH:mm:ss} - {threadMessage.Role,10}: ");
//                foreach (MessageContent contentItem in threadMessage.ContentItems)
//                {
//                    if (contentItem is MessageTextContent textItem)
//                    {
//                        Console.Write(textItem.Text);
//                    }
//                    else if (contentItem is MessageImageFileContent imageFileItem)
//                    {
//                        Console.Write($"<image from ID: {imageFileItem.FileId}");
//                    }
//                    Console.WriteLine();
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex.Message);
//        }

//    }


//    public class AccessTokenCredential : TokenCredential
//    {
//        private readonly string _accessToken;

//        public AccessTokenCredential(string accessToken)
//        {
//            _accessToken = accessToken;
//        }

//        public override AccessToken GetToken(TokenRequestContext requestContext, System.Threading.CancellationToken cancellationToken)
//        {
//            return new AccessToken(_accessToken, DateTimeOffset.UtcNow.AddHours(1)); // Set token expiration as needed
//        }

//        public override async ValueTask<AccessToken> GetTokenAsync(TokenRequestContext requestContext, System.Threading.CancellationToken cancellationToken)
//        {
//            return await Task.FromResult(new AccessToken(_accessToken, DateTimeOffset.UtcNow.AddHours(1))); // Set token expiration as needed
//        }
//    }
//}
