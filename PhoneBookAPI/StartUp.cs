using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PhoneBookAPI.Common;
using PhoneBookAPI.Data;
using PhoneBookAPI.Infrastructure;
using PhoneBookAPI.Infrastructure.Interfaces;
using PhoneBookAPI.Repositories.Interfaces;
using PhoneBookAPI.Repositories.Respositories;
using PhoneBookAPI.Services.Interfaces;
using PhoneBookAPI.Services.Services;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace PhoneBookAPI
{

    public class StartUp
    {

        public IConfiguration configuration { get; }
        public StartUp(IConfiguration config)
        {
            configuration = config;
        }



        private void ConfigureCors(IServiceCollection services)        {
            services.AddCors(options =>            {                options.AddPolicy("PhoneBook",                    builder => builder.SetIsOriginAllowed(x => _ = true)                    .AllowAnyMethod()                    .AllowAnyHeader()                    .AllowCredentials());            });
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "PhoneBook",
            //        policy =>
            //        {
            //            policy.WithOrigins("http://localhost:4200");
            //        });

            //});

        }
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCors(services);
            var mapperconfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mapperconfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc();
            services.AddControllers();
            var conn = configuration["ConnectionStrings:PhoneBookConnectionString"];
            services.AddDbContext<PhoneBookDBContext>(options => options.UseSqlServer(configuration["ConnectionStrings:PhoneBookConnectionString"]));
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme= "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement                {                    {                        new OpenApiSecurityScheme                        {                            Reference = new OpenApiReference                            {                                Type = ReferenceType.SecurityScheme,                                Id = "Bearer"                            }                        },                        new string[] {}                    }                });
              

            });
            
            services.AddAuthentication().AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                      configuration["SecretKey"])
                  )

                };
            });

            #region D.I Repositories
            services.AddTransient<IUserContactsRespository, UserContactsRespository>();
            services.AddTransient<IUserRepostory, UserRepository>();
            #endregion
            #region D.I Services
            services.AddTransient<IUserContactsService, UserContactsService>();
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            #endregion

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PhoneBook API V1");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("PhoneBook");
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseStaticFiles();

        }

    }
}
