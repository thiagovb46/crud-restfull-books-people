using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RestWith.NET.Context;
using RestWith.NET.Business;
using RestWith.NET.Business.Implementations;
using RestWith.NET.Repository;
using RestWith.NET.Repository.Generic;
using Microsoft.Net.Http.Headers;
using RestWith.NET.Hypermedia.Filters;
using RestWith.NET.Hypermedia.Enricher;
using RestWith.NET.Services;
using RestWith.NET.Services.Implementations;
using RestWith.NET.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using src.Repository;
using src.Business;
using src.Business.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using src.Services.InitializeDb;
using System.Collections.Generic;

namespace RestWith.NET
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
            services.AddCors(options=>options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
            var tokenConfigurations = new TokenConfiguration();     
            new ConfigureFromConfigurationOptions<TokenConfiguration> ( 
                Configuration.GetSection("TokenConfigurations")
            )
            .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);
            services.AddAuthentication(options=>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true, 
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenConfigurations.Issuer,
                    ValidAudience = tokenConfigurations.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
                };
            });

            services.AddAuthorization(auth=>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());

            });
            var ConnectionString = Configuration.GetSection("ConnectionStrings.Default");

            services.AddControllers();
            services.AddDbContext<DataContext>(options=>options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddScoped<IPersonBusiness,PersonBusinessImplementation>();
            services.AddScoped<IBookBusiness,BookBusinessImplementation>();
            services.AddScoped (typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddTransient<ILoginBusiness, LoginBusinessImplementation>();
            services.AddTransient<IUsersBusiness, UsersBusinessImplementation>();
            services.AddTransient<ITokenService, TokenServiceImplementation>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IFileBusiness, FileBusinessImplementation>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        

            services.AddMvc(options=>{
                options.
                RespectBrowserAcceptHeader = true; //aceita propriedade setada do header da request
                
                options.
                FormatterMappings.
                SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/xml"));

                options.
                FormatterMappings.
                SetMediaTypeMappingForFormat ("json",MediaTypeHeaderValue.Parse("application/json"));
            }).AddXmlSerializerFormatters();

            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentReponseEnricherList.Add(new PersonEnricher ());
            services.AddSingleton(filterOptions);
            services.AddApiVersioning();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestWith.NET", Version = "v1" });
            });
        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext context)
        {
             context.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestWith.NET v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();    
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller = values}/{id}");
            });
           
        }
    }
}
