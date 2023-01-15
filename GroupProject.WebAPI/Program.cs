using GroupProject.Data;
using GroupProject.Services.CauseOfDeath;
using GroupProject.Services.Composer;
using GroupProject.Services.Genre;
using GroupProject.Services.Period;
using GroupProject.Services.Instrument;
using Microsoft.EntityFrameworkCore;
using GroupProject.Services.Composition;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("TestConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IComposerService, ComposerService>();
builder.Services.AddScoped<ICauseOfDeathService, CauseOfDeathService>();
builder.Services.AddScoped<IPeriodService, PeriodService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IInstrumentService, InstrumentService>();
builder.Services.AddScoped<ICompositionService, CompositionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
