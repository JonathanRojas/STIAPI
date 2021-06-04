using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace STIAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //ESR Comentario: 
            //ENABLE CORS
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder
                        .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization)
                        .AllowCredentials()
                        .SetIsOriginAllowed(_ => true)
                        .WithExposedHeaders("Content-Disposition");
                    });
            });
            //







            //ESR Comentario
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                options.CustomSchemaIds(x => x.FullName);
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

            });
            //

            //ESR Comentario
            //Configuracionación para que los "case" de los nombres de las propiedades JSON usen las mima definicion de las Clases
            services.AddControllers()
                .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
            //


            //ESR Comentario


            //Las siguientes configuraciones son necesarias para controlar y validar las sesiones de usuario y sus privilegios
            //Comentar Si es una WebApi publica.
            //INICIO CONTROL DE USUARIO
            //Para agregar session state
            services.AddHttpContextAccessor();

            // CONFIGURACIÓN DEL SERVICIO DE AUTENTICACIÓN JWT
            //El SecretKey,Issuer y Audience se debe almacenar appsettings.json
            //El webtoken, por lo tanto la sesion del usario es válido por 24 hrs.
            //El cierre de la sesión debe realizarse por front End

            CamaraDeDiputadosLibrary.Application.AuthorizationServer.SetOptions(Configuration["JwtSettings:SecretKey"],
                                                                                Configuration["JwtSettings:Issuer"],
                                                                                Configuration["JwtSettings:Audience"],
                                                                                86400);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(CamaraDeDiputadosLibrary.Application.AuthorizationServer.JwtBearerOptions());


            //CONFIGURACIÓN SESION ACTIVA, 
            //Para  que las llamadas a las WebAPI se validen contra los privilegios de usuario
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ValidSession", policy =>
                    policy.Requirements.Add(new CamaraDeDiputadosLibrary.Application.AuthorizationServer.ValidSession()));
            });

            services.AddSingleton<IAuthorizationHandler, CamaraDeDiputadosLibrary.Application.AuthorizationServer.ValidSessionHandler>();


            ////La siguiente linea determina como se conectarán la aplicación a la DB
            ////Si se incluye las llamadas a la DB se realizarán con el usuario especificado
            ////si se omite las llamada a la DB se realizarán con los usaurios de sesion definidor por los métodos SetCurrentUser
            ////El Username y PAssword se debe almacenar appsettings.json
            ////Esta configuración es necesaria si es una WebApi Publica

            CamaraDeDiputadosLibrary.User.Usuario vUsuario = new CamaraDeDiputadosLibrary.User.Usuario("sistemascamara", "sistemascamara");
            //CamaraDeDiputadosLibrary.User.Usuario vUsuario = new CamaraDeDiputadosLibrary.User.Usuario("iosorioo", "io.1964");
            CamaraDeDiputadosLibrary.Application.AuthorizationServer.UserDB = vUsuario;

            //FIN CONTROL DE USUARIO

            //DEFINICION DE LOS DATOS 
            CamaraDeDiputadosLibrary.DB.AdministrativoServer.InProduction = false;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
