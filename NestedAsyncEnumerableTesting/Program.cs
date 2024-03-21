using System.Threading.Tasks.Dataflow;



var enqueueBlock = new TransformBlock<string, IAsyncEnumerable<string>>(ProcessFirstSequenceStrings);

var printBlock = new ActionBlock<IAsyncEnumerable<string>>(
    async sequence =>
    {
        await foreach (var item in sequence)
        {
            Console.WriteLine(item);
        }
    });

enqueueBlock.LinkTo(
    printBlock,
    new DataflowLinkOptions
    {
        Append = true,
        PropagateCompletion = true
    }
);

await foreach (var item in GetFirstStringSequence())
{
    enqueueBlock.Post(item);
}
    
await printBlock.Completion;

static async IAsyncEnumerable<string> GetFirstStringSequence()
{
    var count = 0;
    
    yield return $"First Sequence {++count}";
    yield return $"First Sequence {++count}";
    yield return $"First Sequence {++count}";
    yield return $"First Sequence {++count}";
    yield return $"First Sequence {++count}";
    yield return $"First Sequence {++count}";
    yield return $"First Sequence {++count}";
    yield return $"First Sequence {++count}";
    yield return $"First Sequence {++count}";

    await Task.CompletedTask;
}

static async IAsyncEnumerable<string> ProcessFirstSequenceStrings(string item)
{
    foreach (var ch in item)
    {
        yield return ch.ToString();
    }

    await Task.CompletedTask;
}