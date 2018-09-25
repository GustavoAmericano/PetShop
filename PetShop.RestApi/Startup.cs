using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PetShop.Core.ApplicationService;
using PetShop.Core.ApplicationService.Impl;
using PetShop.Core.DomainService;
using PetShop.Data;
using PetShop.Data.SQLRepo;


//https://docs.google.com/spreadsheets/d/1w_iVW4kp51oNoKh3bJWZijuUsY8tOtSR_EbGAxxDGqY/edit#gid=0

namespace PetShop.RestApi
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

            // Tell Context that we use SQLite
            services.AddDbContext<PetShopContext>(
                opt => opt.UseSqlite("Data Source=PetShopDB.db"));

            // Dependency Inject st00f
            services.AddScoped<IPetRepository, PetRepo>();
            services.AddScoped<IOwnerRepository, OwnerRepo>();
            services.AddScoped<IColorRepository, ColorRepo>();

            services.AddScoped<IPetService, PetService>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IColorService, ColorService>();

            // Ensure we do not loop entities within entities.
            // E.g. When getting a specific pet,
            // the owner within will not show it's pets.
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) // If in Dev..
            {
                app.UseDeveloperExceptionPage();
                using (var scope = app.ApplicationServices.CreateScope()) // When done, dispose. Get accesspoint to stuff in the services
                {
                    var ctx = scope.ServiceProvider.GetService<PetShopContext>(); // Set ctx to reference to PetShopContext
                    DBSeed.SeedDB(ctx);
                }
            }
            app.UseMvc();
        }
    }
}
