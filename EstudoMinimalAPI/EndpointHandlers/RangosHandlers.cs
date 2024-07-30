namespace EstudoMinimalAPI.EndpointHandlers;
using EstudoMinimalAPI.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using EstudoMinimalAPI.Models;
using EstudoMinimalAPI.Entities;
using AutoMapper;


public class RangosHandlers
{

    public static async Task<Results<Ok<List<RangoDTO>>, NoContent>> GetAllRangosAsync
    (RangoDbContext db,
    IMapper mapper,
    [FromQuery(Name = "nome")] string? rangoNome)
    {

     var entity = mapper.Map<List<RangoDTO>>(await db.Rangos
                                      .AsNoTracking()
                                      .Where(x => rangoNome == null || x.Nome.ToLower().Contains(rangoNome.ToLower()))
                                      .ToListAsync());

    if (entity == null || entity.Count <= 0)
        return TypedResults.NoContent();
    return TypedResults.Ok(entity);
       
    }


    public static async Task<Results<Ok<RangoDTO>, NoContent>> GetByIdRangosAsync
    (RangoDbContext db,
    int rangoId,
    IMapper mapper)
    {
    IQueryable<Rango> entity = db.Rangos;
    var entityRango = mapper.Map<RangoDTO>(await entity.AsNoTracking().FirstOrDefaultAsync(r => r.Id == rangoId));

    if (entityRango == null)
        return TypedResults.NoContent();

    return TypedResults.Ok(entityRango);
    }


    public static async Task<Results<Ok<List<IngredienteDTO>>, NoContent>> 
    GetByIdRangosIngredienteAsync
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
            return TypedResults.NoContent();
        return TypedResults.Ok(rangoEntity);

    }

    public static async Task<Results<Ok<RangoDTO>, BadRequest>> PostRangoAsync 
    (RangoDbContext db,
    [FromBody] RangoParaCriacaoDTO rangoBody,
    IMapper mapper) 
    {
        var entityRango = mapper.Map<Rango>(rangoBody);
        db.Rangos.Add(entityRango);
        await db.SaveChangesAsync();
        var returnToEntity = mapper.Map<RangoDTO>(entityRango);
        return TypedResults.Ok(returnToEntity);
        
    }

    public static async Task<Results<Ok<RangoParaAtualizacaoDTO>, NotFound>> PutRangoAsync
    (RangoDbContext db,
    [FromBody] RangoParaAtualizacaoDTO rangoBody,
    int rangoId,
    IMapper mapper) 
    {  
        var entityRango = await db.Rangos.FirstOrDefaultAsync(x => x.Id == rangoId);
        if (entityRango == null)
            return TypedResults.NotFound();

        mapper.Map(rangoBody, entityRango);
        await db.SaveChangesAsync();
        return TypedResults.Ok(rangoBody);

    }


    public static async Task<Results<Ok<string>, NotFound>> DeleteRangoAsync
    (RangoDbContext db,
    [FromBody] RangoParaDelecaoDTO rangoBody,
    int rangoId,
    IMapper mapper) 
    {

        var entityRango = await db.Rangos.FirstOrDefaultAsync(x => x.Id == rangoId);
        if (entityRango == null)
            return TypedResults.NotFound();

        db.Remove(entityRango);

        await db.SaveChangesAsync();

        return TypedResults.Ok("Deletado com sucesso!");

    }


}
