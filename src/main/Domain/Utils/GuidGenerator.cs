namespace JacksonVeroneze.NET.GRPCServer.Domain.Utils;

public static class GuidGenerator
{
    public static Guid Generate()
    {
        return Guid.CreateVersion7();
    }
}
