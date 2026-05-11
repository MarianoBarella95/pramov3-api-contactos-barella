using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer", 
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header, 
        Description = "Ingrese el token JWT en este formato: Bearer {token}"   
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        } 
    });
});

// BBDD
builder.Services.AddDbContext<pramov3_ao_barella.Models.DbA358b2Pam3Context>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// INYECCIÓN DE DEPENDENCIAS
// CAMBIO DE SINGLETON A SCOPED PARA UNA MEJOR
// INTERACCIÓN CON EF Y LA BBDD
builder.Services.AddScoped<ContactoService>();
// CREACIÓN DEL TOKEN
builder.Services.AddSingleton<TokenProvider>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthController>();

// AÑADO EL CONTROLLER
builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:secret"] ?? "ClaveSecretaDeRespaldo")),
            ValidIssuer = builder.Configuration["Jwt:issuer"],
            ValidAudience = builder.Configuration["Jwt:audience"],
            ClockSkew = TimeSpan.Zero
            
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// OBTENER CONTACTOS MEDIANTE MINIMAL API
app.MapGet("/minimal/contactos", (ContactoService service) =>
{
    return Results.Ok(service.ObtenerTodos());
})
.WithTags("Minimal")
.WithName("MinimalApi")
.WithOpenApi();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


