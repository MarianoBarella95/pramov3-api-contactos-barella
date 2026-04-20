using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

// INYECCIÓN DE DEPENDENCIAS
// ELIJO SINGLETON PARA QUE LA INFORMACIÓN PUEDA PERSISTIR
// LO QUE DURE LA EJECUCIÓN DEL PROGRAMA
builder.Services.AddSingleton<ContactoService>();
// CREACIÓN DEL TOKEN
builder.Services.AddSingleton<TokenProvider>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AuthController>();

// AÑADO EL CONTROLLER
builder.Services.AddControllers();

//
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:secret"])),
            ValidIssuer = builder.Configuration["Jwt:issuer"],
            ValidAudience = builder.Configuration["Jwt:audience"],
            ClockSkew = TimeSpan.Zero
            
        };
    });

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

app.MapControllers();

app.Run();


