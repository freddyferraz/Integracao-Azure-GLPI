
using AutoMapper;
using IntegracaoGLPI_DevOps.Service;
using IntegracaoGLPI_DevOps.Service.Interfaces;
using IntegracaoGLPI_DevOps.ViewModel;
using IntegracaoGLPI_DEvOps.Service.DTO;
using IntegracaoGLPI_DEvOps.Service.Services;

namespace Integracao_Glpi_DevOps;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        #region AutoMapper
        var autoMapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TicketGLPIViewModel, TicketGLPIDTO>().ReverseMap();
        });
        services.AddSingleton(autoMapperConfig.CreateMapper());
        #endregion

        #region Services
        services.AddScoped<IIntegraGLPIService, IntegraGLPIService>();
        #endregion


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