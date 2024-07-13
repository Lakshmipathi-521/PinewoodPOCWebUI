public static class HttpResponseMessageExtensions
{
    public static void WriteRequestToConsole(this HttpResponseMessage response)
    {
        if (response is null)
        {
            return;
        }

        var request = response.RequestMessage;

        Console.WriteLine($"{request?.Method}");
        Console.WriteLine($"{request?.RequestUri}");
        Console.WriteLine($"HTTP/{request?.Version}");
    }
}
