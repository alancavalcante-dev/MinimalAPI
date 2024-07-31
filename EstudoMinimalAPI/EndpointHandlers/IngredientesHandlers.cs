using AutoMapper;
using EstudoMinimalAPI.DbContexts;
using EstudoMinimalAPI.Entities;
using EstudoMinimalAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstudoMinimalAPI.EndpointHandlers;

public class IngredientesHandlers
{
    public static async Task<Results<Ok<List<IngredienteDTO>>, NotFound>>
    GetRangosIngredientesAsync
    (RangoDbContext db,
    int rangoId,
    IMapper mapper)
    {
        IQueryable<Rango> entity = db.Rangos;
        var rangoEntity = mapper.Map<List<IngredienteDTO>>(
        (
        await entity.AsNoTracking() // Consulta sem rastreamento
        .Include(r => r.Ingredientes) // Carrega os ingredientes relacionados
        .FirstOrDefaultAsync(r => r.Id == rangoId) // Filtra pelo ID especificado
        )
        ?.Ingredientes // Acessa os ingredientes se o objeto não for nulo
        );

        if (rangoEntity == null || rangoEntity.Count == 0)
            return TypedResults.NotFound();
        return TypedResults.Ok(rangoEntity);
    }
}