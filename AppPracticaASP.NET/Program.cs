using AppPracticaASP.NET.Filters;
using AppPracticaASP.NET.Models;
using AppPracticaASP.NET.Repository;
using AppPracticaASP.NET.Repository.Interfaces;
using AppPracticaASP.NET.Services;
using AppPracticaASP.NET.Services.Auth;
using AppPracticaASP.NET.Services.Interfaces;
using AppPracticaASP.NET.Services.Interfaces.Auth;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Registramos el DbContext en el contenedor de inyección de dependencias (DI),
// con una función lambda configuramos el proveedor de BD como SQL Server,
// y obtenemos la cadena de conexión desde appsettings.json
// mediante la clave "connectionDB".
builder.Services.AddDbContext<BddEcomerceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connectionDB"))
);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//Para que nos da las apis documentadas
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService,UsuarioService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IProductosRepository, ProductosRepository>(); 
builder.Services.AddScoped<IProductosService,ProductosServices>();


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});



builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(opt =>
{
    //Creamos una firma con la clave del JWT
    var firma = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]));
    //Firmamos con ese algoritomo de seguridad
    var firmaCredenciales = new SigningCredentials(firma, SecurityAlgorithms.HmacSha256Signature);

    opt.RequireHttpsMetadata = false;

    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = firma
    };


});

//Añadimos el servicio:
builder.Services.AddScoped<IsLoggedFilter>();

var app = builder.Build();

//Redireccionamos a nuestra api de usuario
app.MapGet("/", (HttpContext context) =>
{
    context.Response.Redirect("swagger/index.html",permanent:false);
});





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
