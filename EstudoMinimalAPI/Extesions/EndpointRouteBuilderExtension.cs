using EstudoMinimalAPI.EndpointHandlers;

namespace EstudoMinimalAPI.Extesions;

public static class EndpointRouteBuilderExtension
{
    public static void RegisterRangosEndpoints(this IEndpointRouteBuilder endpointRouteBuilder) {
        var rangosEndpoints = endpointRouteBuilder.MapGroup("rangos/");
        var rangosComIdEndpoints = rangosEndpoints.MapGroup("/{rangoId:int}");

        rangosEndpoints.MapGet("", RangosHandlers.GetAllRangosAsync);

        rangosComIdEndpoints.MapGet("", RangosHandlers.GetByIdRangosAsync);

        rangosEndpoints.MapPost("", RangosHandlers.PostRangoAsync);

        rangosComIdEndpoints.MapPut("", RangosHandlers.PutRangoAsync);

        rangosComIdEndpoints.MapDelete("", RangosHandlers.DeleteRangoAsync);
    }

    public static void RegisterIngredientesEndpoints(this IEndpointRouteBuilder endpointRouteBuilder) {
        var ingredientesEndpoint = endpointRouteBuilder.MapGroup("/{rangoId:int}");
        ingredientesEndpoint.MapGet("", IngredientesHandlers.GetRangosIngredientesAsync);
    }
}
