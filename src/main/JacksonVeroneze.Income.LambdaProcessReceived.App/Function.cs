using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using JacksonVeroneze.Income.LambdaProcessReceived.App.Handlers;
using Microsoft.Extensions.DependencyInjection;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace JacksonVeroneze.Income.LambdaProcessReceived.App;

public class Function : IDisposable, IAsyncDisposable
{
    private readonly ServiceProvider _serviceProvider;

    public Function()
    {
        ServiceCollection serviceCollection = new ServiceCollection();

        serviceCollection.AddScoped<ISqsHandler, SqsHandler>();

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    public async Task<SQSBatchResponse> FunctionHandler(
        SQSEvent evnt,
        ILambdaContext context)
    {
        using var scope = _serviceProvider.CreateScope();

        var handler = scope.ServiceProvider
            .GetRequiredService<ISqsHandler>();

        var result = await handler.HandleAsync(
            evnt, CancellationToken.None);

        return result;
    }

    public void Dispose()
    {
        _serviceProvider.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _serviceProvider.DisposeAsync();
    }
}