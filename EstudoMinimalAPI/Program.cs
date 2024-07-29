using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using EstudoMinimalAPI.DbContexts;
using EstudoMinimalAPI.Entities;
using AutoMapper;
using EstudoMinimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<RangoDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("RangoDbConStr"))
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// currentdomain = dominio atual
// pega todos os assemblies criados e vai puxar de quem herda o profile, e acrescenta para o builder.





var app = builder.Build();



app.MapGet("/", () => "Hello World!");


app.MapGet("rango/", async Task<Results<Ok<List<Rango>>, Ok<Rango>, NoContent>>
    (RangoDbContext db,
    [FromQuery(Name = "nome")] string? rangoNome) => {

     var entity = await db.Rangos
                          .AsNoTracking()
                          .Where(x => rangoNome == null || x.Nome.ToLower().Contains(rangoNome.ToLower()))
                          .ToListAsync();

    if (entity == null || entity.Count == 0)
        return TypedResults.NoContent();
    return TypedResults.Ok(entity);
       
});

app.MapGet("rango/{rangoId:int}", async Task<Results<Ok<RangoDTO>, NoContent>>
    (RangoDbContext db, 
    int rangoId,
    IMapper mapper) => {

    IQueryable<Rango> entity = db.Rangos;
    var entityRango = mapper.Map<RangoDTO> (await entity.AsNoTracking().FirstOrDefaultAsync(r => r.Id == rangoId));

    if (entityRango == null)
        return TypedResults.NoContent();

    return TypedResults.Ok(entityRango);
});




app.MapGet("rango/{rangoId:int}/ingredientes", async Task<Results<Ok<List<IngredienteDTO>>, NoContent>>
    (RangoDbContext db, 
    int rangoId,
    IMapper mapper) => {

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

    });





app.MapPost("", async Task<Results<Ok<Rango>, BadRequest<Exception>>>
    (RangoDbContext db, [FromBody] Rango rango) => {
        db.Rangos.Add(rango);
        await db.SaveChangesAsync();
        return TypedResults.Ok(rango);
});



app.Run();