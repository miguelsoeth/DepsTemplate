using DepsTemplate.Infrastructure.Data;
using DepsTemplate.Infrastructure.Identity;
using DepsTemplate.SharedKernel.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DepsTemplate.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services) =>
            services.AddDbContext<AppDbContext>(options =>                
                options.UseNpgsql(AmbienteUtil.GetValue("POSTGRES_CONNECTION"))
            ); // will be created in web project root
                

        public static void ConfigureJwt(this IServiceCollection services) => JwtStartupSetup.RegisterJWT(services);
    }
}
