using Microsoft.EntityFrameworkCore;
using EstudoMinimalAPI.DbContexts;
using EstudoMinimalAPI.EndpointHandlers;
using EstudoMinimalAPI.Extesions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RangoDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("RangoDbConStr"))
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// currentdomain = dominio atual
// pega todos os assemblies criados e vai puxar de quem herda o profile, e acrescenta para o builder.

var app = builder.Build();

app.RegisterRangosEndpoints();
app.RegisterIngredientesEndpoints();


app.Run();