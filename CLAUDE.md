# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

Sample .NET projects that demonstrate Claude API usage (Anthropic C# SDK), tool use, and MCP. The single solution file lives inside the WinForms project folder: `ClaudeTestApp/ClaudeTestApp.sln`. It references all four projects. There are no test projects and no lint configuration.

| Project | Target | Role |
|---|---|---|
| `ClaudeTestApp` | net9.0-windows (WinForms) | Desktop chat UI. `Program.cs` launches `FrmChat`. `FrmIntent` is a second form that returns JSON intent classification. |
| `ClaudeTestApp.AI` | net9.0 class library | All Claude/MCP logic, shared by the UI and the MCP server |
| `ClaudeTestApp.MCP` (assembly `ClaudeTestApp.MCPServer`) | net9.0 ASP.NET Core | HTTP MCP server that exposes order and refund tools |
| `PersistentResearchAgents` | net8.0 console | Standalone sample, independent of the other projects |

## Commands

```bash
dotnet build ClaudeTestApp/ClaudeTestApp.sln

# The MCP server must be running before you use the chat UI (http://localhost:5271/mcp)
dotnet run --project ClaudeTestApp.MCP --launch-profile http

dotnet run --project ClaudeTestApp            # WinForms chat app (Windows only)
dotnet run --project PersistentResearchAgents # writes checkpoints to bin/.../checkpoints
```

## Configuration

`ClaudeTestApp/App.config` holds two `appSettings`, which `ClaudeTestApp.AI` reads through `System.Configuration.ConfigurationManager`:
- `AnthropicApiKey`: never commit a real key. An earlier commit ("remove keus") scrubbed keys from this file.
- `McpServer`: defaults to `http://localhost:5271/mcp`, which matches the `http` profile in `ClaudeTestApp.MCP/Properties/launchSettings.json`.

Because these values come from `ConfigurationManager`, the AI library only works when hosted by a process that has this App.config.

## Architecture: chat + tool-use loop (ClaudeTestApp.AI)

- **`ChatService.SendMessageAsync`** turns `Microsoft.Extensions.AI.ChatMessage` history into Anthropic `MessageParam`s. It calls `Messages.Create` with a hardcoded model (`claude-sonnet-4-5-20250929`), an ephemeral-cached system prompt, and `ToolChoiceAuto`. It then loops for as long as the response reports `ExecutedTool`.
- **Tools run inside the `Models.ChatResponse` constructor.** Any `ToolUseBlock` in the message is executed right away through `ToolService.ExecuteTool`. The tool result is appended to `ChatResponse.Message`, and `ChatService` adds that text back to the history as a plain **user text message**. The loop does not use `tool_result` blocks.
- **`ToolConstants.UseMCP`** (default `true`) selects where tools come from:
  - MCP: `McpService` connects to the MCP server via `McpClientFactory`, which uses the HTTP transport. It converts MCP tool JSON schemas into Anthropic `Tool`s and runs calls with `CallToolAsync`.
  - Local: tools are defined statically in `ToolConstants` and handled by the switch in `ToolService`.
  - Note: `ToolService.GetToolsList` creates an `McpService` (an MCP connection) on every call, even in local mode, so the MCP server has to be reachable either way.
- **`SendIntentMessageAsync`** sends no tools and no system prompt, and parses the response as `ChatResponseMessageType.JSON` (only the first content block is read). `FrmIntent` uses it.
- **`RAGService`** is a separate sample. It uses the beta `ToolRunner` with a `BetaRunnableTool` and a stubbed `search_knowledge_base`.

## Architecture: MCP server (ClaudeTestApp.MCP)

`AddMcpServer().WithHttpTransport().WithToolsFromAssembly()` discovers classes marked `[McpServerToolType]` in `Tools/` and maps them at `/mcp`. Tool classes receive `IOrderService` (a scoped, stubbed `OrderService`) through constructor injection. The domain models (`Order`, `OrderRefund`, enums) come from `ClaudeTestApp.AI/Models`. When you add or rename a tool, keep the local `ToolConstants` definitions and the `ToolService` switch in sync so local mode still works.

## Architecture: PersistentResearchAgents

`ResearchCoordinator` runs three `ResearchAgent` branches in parallel: legal-risk, market-impact, and public-health. Each branch saves its findings and citations independently through `IResearchStateStore` (`JsonResearchStateStore` writes to disk). Synthesis and `ResumeAsync` reload the branch artifacts from the store instead of relying on in-memory state. `DemoResearchLlm` is a stub; real model calls belong in an `IResearchLlm` implementation.
