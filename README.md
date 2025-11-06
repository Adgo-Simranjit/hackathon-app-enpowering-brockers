# 📘 README.md

# hackathon-app-enpowering-brockers
 Empowering Brokers with Smart, Lightweight Automation(P&amp;C)

## 🛠 Tech Stack Used
- **Programming Language**: C#
- **Backend Orchestration**: MCP Server
- **External APIs**: Exact APIs 
- **Frontend**: Custom UI (WPF / Web-based, depending on implementation)
- **AI Agent Layer**: Rule-based or LLM-integrated agent for workflow simplification

## ⚙️ Setup Instructions
1. **Clone the Repository**
   ```bash
   git clone https://github.com/Adgo-Simranjit/hackathon-app-enpowering-brockers
   cd hackathon-app-enpowering-brockers
   ```

2. **Configure MCP Server**
   - Set up environment variables for Exact API keys and endpoints.

3. **Run the Agent**
   - Build and launch the C# application.
   - Ensure MCP server is running and connected to Exact APIs.

4. **Test the Workflow**
   - Use sample policy.

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
