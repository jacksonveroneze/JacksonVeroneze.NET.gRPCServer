namespace JacksonVeroneze.NET.GRPCServer.Api.Rest.Endpoints.Extensions;

internal static class LocationBuilder
{
    public static Uri ForNamedRoute(
        LinkGenerator linkGenerator,
        HttpContext context,
        string routeName,
        object id)
    {
        return ForNamedRoute(linkGenerator, context, routeName,
            new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["id"] = id,
            });
    }

    public static Uri ForNamedRoute(
        LinkGenerator linkGenerator,
        HttpContext context,
        string routeName,
        params (string Key, object Value)[] routeParams)
    {
        Dictionary<string, object> values = routeParams
            .ToDictionary(p => p.Key, p => p.Value,
                StringComparer.OrdinalIgnoreCase);

        return ForNamedRoute(linkGenerator, context, routeName, values);
    }

    private static Uri ForNamedRoute(
        LinkGenerator linkGenerator,
        HttpContext context,
        string routeName,
        IDictionary<string, object> routeValues)
    {
        string uri = linkGenerator.GetUriByName(context, routeName, routeValues)
                     ?? throw new InvalidOperationException($"Route '{routeName}' not found or invalid.");

        return new Uri(uri);
    }
}
