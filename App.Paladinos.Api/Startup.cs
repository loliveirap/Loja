
using System.IO;
using Newtonsoft.Json;
using App.Paladinos.Shared;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using App.Paladinos.Infra.StoreContext.Services;
using App.Paladinos.Domain.StoreContext.Services;
using App.Paladinos.Infra.StoreContext.Repositories;
using App.Paladinos.Infra.StoreContext.DataContexts;
using App.Paladinos.Domain.StoreContext.Repositories;
using App.Paladinos.Domain.StoreContext.Commands.Handlers;

namespace App.Paladinos.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Settings.ConnectionString = Configuration["ConnectionStrings:PaladinosDB"];
            Configuration = builder.Build();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opcoes =>
                {
                    opcoes.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    opcoes.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opcoes.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddResponseCompression();

            services.AddScoped<DataContext, DataContext>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<INotificacaoServices, NotificacaoServico>();

            services.AddTransient<IClienteRepository, ClienteRpository>();
            services.AddTransient<ClienteHandler, ClienteHandler>();
            services.AddTransient<IEnderecoRepository, EnderecoRepository>();
            services.AddTransient<EnderecoHandler, EnderecoHandler>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<ProdutoHandler, ProdutoHandler>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddTransient<PedidoHandler, PedidoHandler>();
            services.AddTransient<ICondicaoPagamentoRepository, CondicaoPagamentoRepository>();
            services.AddTransient<CondicaoPagamentoHandler, CondicaoPagamentoHandler>();

            services.AddSwaggerGenNewtonsoftSupport();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "App Paladinos", Version = "v1" });
            });
        }
       
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseResponseCompression();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "App Paladinos"));
        }
    }
}
