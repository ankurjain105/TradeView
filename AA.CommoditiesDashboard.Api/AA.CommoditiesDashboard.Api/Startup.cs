using AA.CommoditiesDashboard.Api.Core;
using AA.CommoditiesDashboard.Api.Models;
using AA.CommoditiesDashboard.Persistence;
using AA.CommoditiesDashboard.Service;
using AA.CommoditiesDashboard.Service.Queries.CurrentSnapshot;
using AA.CommoditiesDashboard.Service.Queries.HistoricalPnl;
using AA.CommoditiesDashboard.Service.Queries.Lookups;
using AA.CommoditiesDashboard.Service.Queries.TradeActionHistory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AA.CommoditiesDashboard.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RepositoryContext>(
                options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
            services.AddControllers();
            services.AddScoped<IClock, Clock>();
            services.AddScoped<ITradeRepository, TradeRepository>();
            services.AddScoped<IDbContext>(f => f.GetService<RepositoryContext>());
            services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<IQueryHandler<TradeActionHistoryQuery, TradeActionHistoryQueryResult>,
                TradeActionHistoryQueryHandler>();
            services.AddScoped<IQueryHandler<HistoricalPnlQuery, HistoricalPnlQueryResult>,
                HistoricalPnlQueryHandler>();
            services.AddScoped<IQueryHandler<CurrentSnapshotQuery, CurrentSnapshotQueryResult>,
                CurrentSnapshotQueryHandler>();
            services.AddScoped<IQueryHandler<LookupQuery, LookupQueryResult>,
                LookupQueryHandler>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                    });
            });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
        }
    }
}
