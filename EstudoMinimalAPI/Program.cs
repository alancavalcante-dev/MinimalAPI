using Microsoft.EntityFrameworkCore;
using EstudoMinimalAPI.DbContexts;
using EstudoMinimalAPI.EndpointHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RangoDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("RangoDbConStr"))
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// currentdomain = dominio atual
// pega todos os assemblies criados e vai puxar de quem herda o profile, e acrescenta para o builder.

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
var rangos = app.MapGroup("rangos/");
var rango = app.MapGroup("rango/");

rangos.MapGet("", RangosHandlers.GetAllRangosAsync);
rango.MapGet("{rangoId:int}/", RangosHandlers.GetByIdRangosAsync);
rango.MapGet("{rangoId:int}/ingredientes/", RangosHandlers.GetByIdRangosIngredienteAsync);

rangos.MapPost("", RangosHandlers.PostRangoAsync);
rangos.MapPut("{rangoId:int}/", RangosHandlers.PutRangoAsync);
rangos.MapDelete("{rangoId:int}/", RangosHandlers.DeleteRangoAsync);

app.Run();