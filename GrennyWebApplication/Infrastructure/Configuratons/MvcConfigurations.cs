﻿namespace GrennyWebApplication.Infrastructure.Configurations
{
    public static class MvcConfigurations
    {
        public static void ConfigureMvc(this IServiceCollection services)
        {
            services
               .AddMvc()
               .AddRazorRuntimeCompilation();
        }
    }
}
