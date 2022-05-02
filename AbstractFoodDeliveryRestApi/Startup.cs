using AbstractFoodDeliveryBusinessLogic.BusinessLogics;
using AbstractFoodDeliveryContracts.BusinessLogicsContracts;
using AbstractFoodDeliveryContracts.StoragesContracts;
using AbstractFoodDeliveryDatabaseImplement.Implements;
using AbstractFoodDeliveryBusinessLogic.MailWorker;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AbstractFoodDeliveryContracts.BindingModels;

namespace AbstractFoodDeliveryRestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services
        //to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IClientStorage, ClientStorage>();
            services.AddTransient<IOrderStorage, OrderStorage>();
            services.AddTransient<IDishStorage, DishStorage>();
            services.AddTransient<IMessageInfoStorage, MessageInfoStorage>();

            services.AddTransient<IOrderLogic, OrderLogic>();
            services.AddTransient<IClientLogic, ClientLogic>();
            services.AddTransient<IDishLogic, DishLogic>();
            services.AddTransient<IMessageInfoLogic, MessageInfoLogic>();
            services.AddSingleton<AbstractMailWorker, MailKitWorker>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AbstractFoodDeliveryRestApi",
                    Version = "v1"
                });
            });
        }
        // This method gets called by the runtime. Use this method to configure the
        //HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AbstractFoodDeliveryRestApi v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var mailSender =
            app.ApplicationServices.GetService<AbstractMailWorker>();
            mailSender.MailConfig(new MailConfigBindingModel
            {
                MailLogin =
            Configuration?.GetSection("MailLogin")?.ToString(),
                MailPassword =
            Configuration?.GetSection("MailPassword")?.ToString(),
                SmtpClientHost =
            Configuration?.GetSection("SmtpClientHost")?.ToString(),
                SmtpClientPort =
            Convert.ToInt32(Configuration?.GetSection("SmtpClientPort")?.ToString()),
                PopHost = Configuration?.GetSection("PopHost")?.ToString(),
                PopPort =
            Convert.ToInt32(Configuration?.GetSection("PopPort")?.ToString())
            });
        }
    }
}
