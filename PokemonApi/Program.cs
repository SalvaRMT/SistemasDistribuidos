using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using PokemonApi.Infrastructure;
using PokemonApi.Services;
using SoapCore;
using PokemonApi.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSoapCore();

builder.Services.AddSingleton<IPokemonService, PokemonService>(); //AddSingleton
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddSingleton<IHobbiesService,HobbiesService>(); //AddSingleton
builder.Services.AddScoped<IHobbiesRepository,HobbiesRepository>();
builder.Services.AddSingleton<IBookService, BookService>(); //AddSingleton
builder.Services.AddScoped<IBookRepository, BookRepository>();
//host.docker.internal

builder.Services.AddDbContext<RelationalDbContext>(options => 
options.UseMySql(
    builder.Configuration.GetConnectionString("DefaultConnection"), 
ServerVersion.AutoDetect( 
    builder.Configuration.GetConnectionString("DefaultConnection")),mySqlOptions => mySqlOptions.EnableRetryOnFailure()));
        
var app = builder.Build();

app.UseSoapEndpoint<IPokemonService>("/PokemonService.svc", new SoapEncoderOptions());
app.UseSoapEndpoint<IHobbiesService>("/AlanSalvadorHobbiesService.svc", new SoapEncoderOptions());
app.UseSoapEndpoint<IBookService>("/BookService.svc", new SoapEncoderOptions());

app.Run();