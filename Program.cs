using System.Text;
using fornecedor_api;
using fornecedor_api.Data.Context;
using fornecedor_api.Data.Repositories;
using fornecedor_api.Interfaces;
using fornecedor_api.Models.Entities;
using fornecedor_api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Fornecedores", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() 
    { 
        Name = "Authorization", 
        Type = SecuritySchemeType.ApiKey, 
        Scheme = "Bearer", 
        BearerFormat = "JWT", 
        In = ParameterLocation.Header, 
        Description = "Autenticação JWT no header usando Bearer scheme." +
                      "\r\n Insira o prefixo Bearer em seguida o token JWT" +
                      "\r\n Exemplo Bearer 1234567"
    }); 
    c.AddSecurityRequirement(new OpenApiSecurityRequirement 
    { 
        { 
            new OpenApiSecurityScheme 
            { 
                Reference = new OpenApiReference 
                { 
                    Type = ReferenceType.SecurityScheme, 
                    Id = "Bearer" 
                } 
            }, 
            new string[] {} 
        } 
    }); 
});
var connectionString = builder.Configuration.GetConnectionString("FornecedoresContext");
builder.Services.AddDbContext<FornecedoresContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddScoped(typeof(IRepository<Fornecedor>),typeof(Repository<Fornecedor>));
builder.Services.AddScoped(typeof(IRepository<EnderecoFornecedor>),typeof(Repository<EnderecoFornecedor>));
builder.Services.AddScoped(typeof(IRepository<Usuario>),typeof(Repository<Usuario>));
builder.Services.AddScoped(typeof(IUsuarioService), typeof(UsuarioService));
builder.Services.AddScoped(typeof(IAuthServices), typeof(AuthServices));
builder.Services.AddScoped(typeof(IFornecedorService),typeof(FornecedorService));
builder.Services.AddScoped(typeof(IEnderecoFornecedorService), typeof(EnderecoFornecedorService));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var key = Encoding.UTF8.GetBytes(Settings.Secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})    .AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});;


var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
