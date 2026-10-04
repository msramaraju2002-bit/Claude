# Persistent Research Agents

.NET 8 sample showing a coordinator with three research branches:

- Legal-risk
- Market-impact
- Public-health

Each branch persists its findings and citations independently before synthesis.
The coordinator reloads all branch artifacts from durable storage, preventing
citation loss when a branch/session cannot be resumed.

## Run

```bash
dotnet run
```

`DemoResearchLlm` is intentionally a stub. Replace it with an implementation
of `IResearchLlm` using Anthropic, OpenAI, or another model provider.

For production, replace `JsonResearchStateStore` with SQL Server, PostgreSQL,
Cosmos DB, or another durable transactional store.
