using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using ShopApi.Data;
using ShopApi.Filters;
using ShopApi.Repositories;
using ShopApi.Services;
using ShopApi.Logic;
using System.IdentityModel.Tokens.Jwt;

namespace ShopApi
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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options =>
               options.UseSqlServer(connectionString));
 

            //Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Version = "v1", Description = "", Title = "", TermsOfService = "" });

                c.DescribeAllEnumsAsStrings();
            });
            Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
           
            services.AddAutoMapper(typeof(Startup).Assembly);
 
            services.AddCors(o => o.AddPolicy("ApplicationPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            AddDependencyIntection(services);
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //use Identity server for Authentication
            services.AddAuthentication("Bearer")
          .AddJwtBearer("Bearer", options =>
          {
              options.Authority = "http://localhost:5000";
              options.RequireHttpsMetadata = false;
              options.Audience = "api1";
          });



            services.AddMvc(config =>
            {
                config.Filters.Add(new CustomExceptionFilterAttribute());
                config.Filters.Add(new ValidateModelFilterAttribute());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env )
        {
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           
            app.UseAuthentication();
 
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopApi V1");
            });

            //enable cross domain
            app.UseCors("ApplicationPolicy");
            //app.UseHttpsRedirection();
 
            app.UseMvc();
        }

        private static void AddDependencyIntection(IServiceCollection services)
        {
            services.AddScoped<IApplicationContext, ApplicationContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IItemLogic, ItemLogic>();
        }

    }
}
