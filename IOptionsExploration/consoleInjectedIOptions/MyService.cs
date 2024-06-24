using Microsoft.Extensions.Options;

public class MyService
{
    private readonly IOptions<MySettings> options;

    public MyService(IOptions<MySettings> options)
    {
        this.options = options;
    }
    public void DoSomething()
    {
        Console.WriteLine($"Doing something...key1: {options.Value.Key1}, key2: {options.Value.Key2}, key3: {options.Value.Key3}");
    }
}
