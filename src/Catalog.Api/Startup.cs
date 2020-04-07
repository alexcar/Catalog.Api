using Catalog.Api.Controllers;
using Catalog.Api.Extensions;
using Catalog.Api.Middleware;
using Catalog.Api.Responses;
using Catalog.Domain.Extensions;
using Catalog.Domain.Repositories;
using Catalog.Domain.Responses.Item;
using Catalog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RiskFirst.Hateoas;

namespace Catalog.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services                
                .AddCatalogContext(Configuration.GetSection("DataSource:ConnectionString").Value)
                .AddResponseCaching()
                .AddMemoryCache()
                .AddScoped<IItemRepository, ItemRepository>()
                .AddScoped<IArtistRepository, ArtistRepository>()
                .AddScoped<IGenreRepository, GenreRepository>()
                .AddMappers()
                .AddServices()
                .AddControllers()
                //.AddNewtonsoftJson(options =>
                //{
                //    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                //})
                .AddValidation()
                .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);

            services
                .AddHealthChecks()
                .AddSqlServer(Configuration.GetSection("DataSource:ConnectionString").Value);


            services.AddLinks(config =>
            {
                config.AddPolicy<ItemHateoasResponse>(policy =>
                {
                    policy
                        .RequireRoutedLink(nameof(ItemsHateoasController.Get), nameof(ItemsHateoasController.Get))
                        .RequireRoutedLink(nameof(ItemsHateoasController.GetById),
                            nameof(ItemsHateoasController.GetById), _ => new { id = _.Data.Id })
                        .RequireRoutedLink(nameof(ItemsHateoasController.Post), nameof(ItemsHateoasController.Post))
                        .RequireRoutedLink(nameof(ItemsHateoasController.Put), nameof(ItemsHateoasController.Put),
                            x => new { id = x.Data.Id })
                        .RequireRoutedLink(nameof(ItemsHateoasController.Delete), nameof(ItemsHateoasController.Delete),
                            x => new { id = x.Data.Id });
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(cfg =>
            {
                cfg.AllowAnyOrigin();
            });

            app.UseRouting();
            app.UseHttpsRedirection();            
            app.UseAuthorization();
            app.UseResponseCaching();
            app.UseHealthChecks("/health");
            app.UseMiddleware<ResponseTimeMiddlewareAsync>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
