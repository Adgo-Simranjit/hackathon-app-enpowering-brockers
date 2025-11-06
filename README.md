# 📘 README.md

# hackathon-app-enpowering-brockers
 Empowering Brokers with Smart, Lightweight Automation(P&amp;C)

# Github Url
- https://github.com/Adgo-Simranjit/hackathon-app-enpowering-brockers

## 🛠 Tech Stack Used
- **Programming Language**: C#
- **Backend Orchestration**: MCP Server
- **External APIs**: Exact APIs 
- **Frontend**: Custom UI (Angular App)
- **AI Agent Layer**: Custom AI agent built using Azure AI Foundry, designed to interact with brokers via a lightweight interface. It connects to the MCP orchestration tool to execute tasks such as get policy.

## ⚙️ Setup Instructions
1. **Clone the Repository**
   ```bash
   git clone https://github.com/Adgo-Simranjit/hackathon-app-enpowering-brockers
   cd hackathon-app-enpowering-brockers
   ```

2. **Configure MCP Server**
   - Set up environment variables for Exact API keys and endpoints.

3. **Run the Agent**
   - Build and launch the .NET Core WebAPI that connects with the custom AI Agent.
   - Ensure MCP server is running and connected to Exact APIs.
   - Build and run the Angular application for the UI (local Url is http://localhost:4200/chat).
   - Confirm that the Angular app communicates with the WebAPI and MCP server for orchestrating Exact API calls.
   - Alternatively, you can run the ConsoleApp to test the agent.

4. **Test the Workflow**
   - Sample prompt: Get policy {policyreference}
   - Use sample policy: HackathonTest

## 🧠 Key Architecture & Design Decisions
- **MCP Server as Middleware**: Chosen to decouple the agent from Exact, allowing modular orchestration and simplified API management.
- **Thin-use Optimization**: Focused on essential tasks to reduce complexity and cost for brokers.
- **AI Agent Interface**: Designed to abstract technical workflows into intuitive interactions.
- **Modular API Integration**: Each Exact API is wrapped as a service module for scalability.

## 🚧 Known Limitations & Future Improvements
### Limitations
- Currently supports only a subset of Exact APIs.
- Basic UI without adaptive learning or personalization.

### Future Improvements
- Add support for additional Exact modules.
- Integrate LLMs for smarter broker interaction.
- Build analytics dashboard for returned data.
- Enhance security with OAuth2 and audit logging.
