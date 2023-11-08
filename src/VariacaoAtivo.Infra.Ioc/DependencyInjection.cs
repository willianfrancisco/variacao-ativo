using Microsoft.Extensions.DependencyInjection;
using VariacaoAtivo.Application.Interfaces;
using VariacaoAtivo.Application.Mappings;
using VariacaoAtivo.Application.Services;
using VariacaoAtivo.Domain.Interfaces;
using VariacaoAtivo.Infra.Data.Repositories;

namespace VariacaoAtivo.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapeamentoEntidadeParaDTO));
            return services;
        }

        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IAtivoRepository, AtivoRepository>();
            services.AddScoped<IAtivoService, AtivoService>();
            return services;
        }
    }
}