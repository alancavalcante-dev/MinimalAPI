using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using EstudoMinimalAPI.DbContexts;
using EstudoMinimalAPI.Entities;

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
    (RangoDbContext db) => {

    IQueryable<Rango> entity = db.Rangos;
    var itemRango = await entity.AsNoTracking().ToListAsync();
    if (itemRango == null || itemRango.Count == 0)
        return TypedResults.NoContent();
    return TypedResults.Ok(itemRango);
       
});

app.MapGet("rango/{rangoId:int}", async Task<Results<Ok<Rango>, NoContent>>
    (RangoDbContext db, int rangoId) => {

    IQueryable<Rango> entity = db.Rangos;
    var itemRango = await entity.AsNoTracking().FirstOrDefaultAsync(r => r.Id == rangoId);
    
    if (itemRango == null) 
        return TypedResults.NoContent();
    return TypedResults.Ok(itemRango);

});


app.MapGet("rango/{rangoId:int}/ingredientes", async Task<Results<Ok<Rango>, NoContent>>
    (RangoDbContext db, int rangoId) => {

        IQueryable<Rango> entity = db.Rangos;
        var itemRango = await entity.AsNoTracking().Include(r => r.Ingredientes).FirstOrDefaultAsync(r => r.Id == rangoId);

        if (itemRango == null)
            return TypedResults.NoContent();
        return TypedResults.Ok(itemRango);

    });



app.MapPost("", async Task<Results<Ok<Rango>, BadRequest<Exception>>>
    (RangoDbContext db, [FromBody] Rango rango) => {
        db.Rangos.Add(rango);
        await db.SaveChangesAsync();
        return TypedResults.Ok(rango);
});



app.Run();