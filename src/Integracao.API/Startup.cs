
using AutoMapper;
using Integracao.Infra;
using Integracao.Infra.Abstractions;
using IntegracaoGLPI_DevOps.Service.Interfaces;
using IntegracaoGLPI_DevOps.ViewModel;
using IntegracaoGLPI_DEvOps.Service.DTO;
using IntegracaoGLPI_DEvOps.Service.Interfaces;
using IntegracaoGLPI_DEvOps.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace Integracao_Glpi_DevOps;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {


        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    if (Configuration["ASPNETCORE_ENVIRONMENT"] == "Dev")
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    }
                    else
                    {
                        var section = Configuration.GetSection($"CorsOrigins");
                        var corsOrigins = section.Get<string[]>();

                        builder.WithOrigins(corsOrigins)
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                    }
                });
        });

        #region AutoMapper
        var autoMapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TicketGLPIViewModel, TicketGLPIDTO>().ReverseMap();
            cfg.CreateMap<CardDevOpsViewModel, CardDevOpsDTO>().ReverseMap();
        });
        services.AddSingleton(autoMapperConfig.CreateMapper());
        #endregion

        #region Services
        services.AddScoped<IIntegraGLPIService, IntegraGLPIService>();
        services.AddScoped<IIntegraDevOpsService, IntegraDevOpsService>();
        #endregion

        services.AddDbContext<IntegracaoContext>(options => options.UseSqlServer(Configuration["connectionStrings"]));
       
        services.AddScoped<IDbSession, DbSession>();

        services.AddControllers();
        services.AddSwaggerGen();
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Integracao-Glpi-DevOps v1"));
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

}