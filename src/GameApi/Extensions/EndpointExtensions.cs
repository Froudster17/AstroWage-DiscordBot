using GameApi.Common.Interface;
using System.Reflection;

namespace GameApi.Extensions;

public static class EndpointExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpointTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t =>
                typeof(IEndpoint).IsAssignableFrom(t) &&
                t is { IsInterface: false, IsAbstract: false });

        foreach (var type in endpointTypes)
        {
            var endpoint = (IEndpoint)Activator.CreateInstance(type)!;

            endpoint.MapEndpoint(app);
        }
    }
}