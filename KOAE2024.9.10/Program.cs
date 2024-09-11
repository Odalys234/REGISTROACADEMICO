//importar el espacio de nombres necesarios para la autenticacion por cookies
using Microsoft.AspNetCore.Authentication.Cookies;
//importar el especio de nombres necesario para trabajar con JSON
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

//agregar servicios al contenedor de despendencias.
//agregar el servicio de controladores al contenedor
builder.Services.AddControllers();
//agregar el servicio para la exploracion de API de puntos finales
builder.Services.AddEndpointsApiExplorer();
//agrega el servicio para la generacion de Swagger
builder.Services.AddSwaggerGen();

//configuracion para la autenticacion por cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        //Configura el nombre del parametro de URL para redireccionamiento no autorizado
        options.ReturnUrlParameter = "unauthorized";
        options.Events = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                //cambia el codigo de estado a no autorizado
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                //Establece el tipo de contenido como JSON (u otro formato deseado)
                context.Response.ContentType = "application/json";
                var message = new
                {
                    error = "No autorizado",
                    message = "Debe iniciar sesion para acceder a este recurso. "
                };
                //Serializa el objeto 'message' en formato 
                //JSON (puedes usar otro serializador JSON si lo prefieres)
                var jsonMessage = JsonSerializer.Serialize(message);
                //Escribe el mensaje JSON en la respuesta HTTP 
                return context.Response.WriteAsync(jsonMessage);
            }
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //Habilita la interfaz de usuario de Swagger en entorno de desarrollo
    app.UseSwagger();
    //Habilita la interfaz de usuario de Swagger en entorno de desarrollo
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();//habilita la redireccion Https
app.UseAuthentication();//habilita la autenticacion
app.UseAuthorization(); //habilita la autorizacion
app.MapControllers();// mapea los controladores

app.Run();// inicializa la aplicacion