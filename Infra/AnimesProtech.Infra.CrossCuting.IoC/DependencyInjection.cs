using System.Data;
using System.Reflection;
using AnimesProtech.Application.Interfaces;
using AnimesProtech.Application.Mapper;
using AnimesProtech.Application.Services;
using AnimesProtech.Domain.Bus;
using AnimesProtech.Domain.Interface.Bus;
using AnimesProtech.Domain.Interface.Notification;
using AnimesProtech.Domain.Interface.Repository;
using AnimesProtech.Infra.CrossCutting.Notification;
using AnimesProtech.Infra.Data.Context;
using AnimesProtech.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Npgsql;

namespace AnimesProtech.Infra.CrossCuting.IoC;

public static class DependencyInjection
{
    /// <summary>
    ///  Configura e adiciona as infraestruturas necessárias para a aplicação.
    /// </summary>
    /// <param name="services">Coleção de serviços</param>
    /// <param name="configuration">Configurações da aplicação</param>
    public static void AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddSwagger(services);
        AddDbContext(services, configuration);
        AddBus(services);
        AddMapper(services);
        ConfigureIoC(services);
    }

    /// <summary>
    /// Configura a inversão de dependências
    /// </summary>
    /// <param name="services">Coleção de serviços</param>
    private static void ConfigureIoC(IServiceCollection services)
    {
        services.AddScoped<IAnimeAppService, AnimeAppService>();
        services.AddScoped<IAnimeRepository, AnimeRepository>();
    }

    /// <summary>
    /// Configura os serviços que irão fazer parte do Bus
    /// </summary>
    /// <param name="services">Coleção de serviços</param>
    private static void AddBus(IServiceCollection services)
    {
        services.AddScoped<INotify, Notify>();
        services.AddScoped<IBus, Bus>();
    }

    /// <summary>
    /// Configura os dbContext da aplicação
    /// </summary>
    /// <param name="services">Coleção de serviços</param>
    /// <param name="configuration">Configurações da aplicação</param>
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<AnimesDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        services.AddSingleton<IDbConnection>(db => new NpgsqlConnection(connectionString));
    }

    /// <summary>
    /// Configura o swagger da aplicação
    /// </summary>
    /// <param name="services">Coleção de serviços</param>
    private static void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            var xmlFileService = $"{Assembly.Load("AnimesProtech.Api").GetName().Name}.xml";
            var xmlPathService = Path.Combine(AppContext.BaseDirectory, xmlFileService);
            options.IncludeXmlComments(xmlPathService);
            
            var xmlFileApplication = $"{Assembly.Load("AnimesProtech.Application").GetName().Name}.xml";
            var xmlPathApplication = Path.Combine(AppContext.BaseDirectory, xmlFileApplication);
            options.IncludeXmlComments(xmlPathApplication);
            
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Animes protech Api",
                Description = "Api para gerenciamento de animes",
                Contact = new OpenApiContact
                {
                    Name = "Email",
                    Email = "jose.csousa22@gmail.com"
                }
            });
        });
    }

    /// <summary>
    /// Configura o automapper
    /// </summary>
    /// <param name="services">Coleção de serviços</param>
    private static void AddMapper(IServiceCollection services)
    {
        var mapper = MappingConfiguration.RegisterMappings().CreateMapper();
        services.AddSingleton(mapper);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}