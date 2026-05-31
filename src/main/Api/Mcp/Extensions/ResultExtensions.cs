using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.GRPCServer.Api.Mcp.Util;
using JacksonVeroneze.NET.Result;
using ModelContextProtocol.Protocol;

namespace JacksonVeroneze.NET.GRPCServer.Api.Mcp.Extensions;

[ExcludeFromCodeCoverage]
public static class ResultExtensions
{
    extension<T>(Result<T> result)
    {
        public CallToolResult ToCallToolResultError()
        {
            var res = McpToolResult.Error(
                code: "APPLICATION_ERROR",
                message: result.FirstError?.Message ?? string.Empty);

            return res;
        }

        public CallToolResult ToCallToolResultSuccess()
        {
            var res = McpToolResult.Success(
                message: "Created successfully",
                structuredContent: new
                {
                    data = result.Value,
                });
        
            return res;
        }
    }
    
    extension(Result.Result result)
    {
        public CallToolResult ToCallToolResultError()
        {
            var res = McpToolResult.Error(
                code: "APPLICATION_ERROR",
                message: result.FirstError?.Message ?? string.Empty);

            return res;
        }

        public CallToolResult ToCallToolResultSuccess()
        {
            var res = McpToolResult.Success(
                message: "Successfully",
                structuredContent: new
                {
                    data = "Successfully",
                });
        
            return res;
        }
    }
}
