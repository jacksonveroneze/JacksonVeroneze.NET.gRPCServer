using Grpc.Core;

namespace JacksonVeroneze.NET.GRPCServer.Api.Services;

public class ProductService : ProductGRPCService.ProductGRPCServiceBase
{
    public ProductService()
    {
    }

    public override async Task<ImportProductResponse> ImportProductsStream(
        IAsyncStreamReader<ImportProductRequest> requestStream,
        ServerCallContext context)
    {
        ImportProductResponse importResponse = new();

        await foreach (ImportProductRequest importProductItem in
                       requestStream.ReadAllAsync())
        {
            importResponse.Count += 1;

            Console.WriteLine(
                $"- product has been imported. SKU: " +
                $"{importProductItem.Sku} Brand: {importProductItem.Brand}");
        }

        return importResponse;
    }
}