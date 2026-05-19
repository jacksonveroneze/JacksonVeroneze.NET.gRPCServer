namespace JacksonVeroneze.NET.GRPCServer.Application.Abstractions.Services;

public interface IDataMapper
{
    TDestination Map<TDestination>(object source);

    TDestination Map<TSource, TDestination>(TSource source);
}
