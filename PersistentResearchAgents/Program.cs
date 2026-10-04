IResearchStateStore store =
    new JsonResearchStateStore(
        Path.Combine(AppContext.BaseDirectory, "checkpoints"));

IResearchLlm llm = new DemoResearchLlm();

var agent = new ResearchAgent(llm, store);
var coordinator = new ResearchCoordinator(agent, store, llm);

string topic =
    "Evaluate the policy implications of a hypothetical pharmaceutical pricing reform.";

var result = await coordinator.ExecuteAsync(topic);

Console.WriteLine($"Research ID: {result.ResearchId}");
Console.WriteLine();
Console.WriteLine(result.Report);
Console.WriteLine();
Console.WriteLine("Checkpoint files are stored in the checkpoints directory.");
