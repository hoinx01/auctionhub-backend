using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using SrvCornet.Core;
using SrvCornet.Http;
using Startup.Components;

namespace Iken.Startup
{
    public class Startup
    {
        public IHostingEnvironment HostingEnvironment { get; }
        public IConfiguration Configuration { get; }
        public Startup(IHostingEnvironment env, IConfiguration config)
        {
            HostingEnvironment = env;
            Configuration = config;
            AppSettings.SetConfig(config);
        }

        // Use this method to add services to the container.
        //optional
        public void ConfigureServices(IServiceCollection services)
        {
            //Đăng kí cho automapper
            AutoMapperInitiator.Init();
            //Đăng kí cho mvc, có thằng này mới viết controller mvc được
            services.AddSingleton<IApiDescriptionGroupCollectionProvider, ApiDescriptionGroupCollectionProvider>();
            
            services.AddMvc(options => {
                options.Filters.Add(new CustomApiExceptionFilter());
            });
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
            });
            //Đăng kí dependency
            DependencyRegister.Register(services);
            //Thiết lập 1 static instance của ServiceProvider để có thể gọi resolve 1 dependency instance 1 cách linh hoạt
            DependencyManager.SetServiceProvider(services.BuildServiceProvider());
        }

        // Use this method to configure the HTTP request pipeline.
        //require
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
