using Api.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Infrastructure.Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SourDictionaryContext>(conf =>
            {
                var connStr = configuration["SourDictionaryDbConntectionString"].ToString();
                conf.UseSqlServer(connStr, opt => 
                {
                    opt.EnableRetryOnFailure();
                });
            });

            return services;
        }
    }
}
