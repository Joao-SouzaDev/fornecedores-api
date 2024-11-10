using fornecedor_api.Data.Context;
using fornecedor_api.Data.Repositories;
using fornecedor_api.Interfaces;
using fornecedor_api.Models.Entities;
using fornecedor_api.Services;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("FornecedoresContext");
builder.Services.AddDbContext<FornecedoresContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddScoped(typeof(IRepository<Fornecedor>),typeof(Repository<Fornecedor>));
builder.Services.AddScoped(typeof(IFornecedorService),typeof(FornecedorService));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
