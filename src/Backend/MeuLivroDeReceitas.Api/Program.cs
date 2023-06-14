using MeuLivroDeReceitas.Api.Filtros;
using MeuLivroDeReceitas.Application;
using MeuLivroDeReceitas.Application.Services.AutoMapper;
using MeuLivroDeReceitas.Domain.Extension;
using MeuLivroDeReceitas.Infrastructure;
using MeuLivroDeReceitas.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositorio(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddMvc(options => options.Filters.Add(typeof(FiltroDasExceptions)));

builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperConfiguration());
}).CreateMapper());

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

AtualizarBasedeDados();

app.Run();

void AtualizarBasedeDados()
{
    var conexao = builder.Configuration.GetConexao();
    var databaseName = builder.Configuration.GetNomeDatabase();
    Database.CriarDatabase(conexao, databaseName);
    app.MigratebancoDeDados();
}
