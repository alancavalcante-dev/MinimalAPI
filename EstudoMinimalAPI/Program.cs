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

// builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


var app = builder.Build();



app.MapGet("/", () => "Hello World!");


app.MapGet("rango/", async Task<Results<Ok<List<Rango>>, Ok<Rango>, NoContent>>
    (RangoDbContext db) => {

    IQueryable<Rango> entity = db.Rangos;
    var itemRango = await entity.AsNoTracking().ToListAsync();
    if (itemRango == null)
        return TypedResults.NoContent();
    return TypedResults.Ok(itemRango);
       
});

app.MapGet("rango/{id:int}", async Task<Results<Ok<Rango>, NoContent>>
    (RangoDbContext db, int id) => {

    IQueryable<Rango> entity = db.Rangos;
    var itemRango = await entity.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
    return TypedResults.Ok(itemRango);

});



app.MapPost("", async Task<Results<Ok<Rango>, BadRequest<Exception>>>
    (RangoDbContext db, [FromBody] Rango rango) => {
        db.Rangos.Add(rango);
        await db.SaveChangesAsync();
        return TypedResults.Ok(rango);
});



app.Run();