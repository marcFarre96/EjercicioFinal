using Domain.Interfaces;
using Domain.Models;
using FruitShop.V1.Controllers.Buys.Service;
using FruitShop.V1.Controllers.Buys.Service.Interfaces;
using FruitShop.V1.Controllers.Customers.Service;
using FruitShop.V1.Controllers.Customers.Service.Interfaces;
using FruitShop.V1.Controllers.Fruits.Service;
using FruitShop.V1.Controllers.Fruits.Service.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FruitShop
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<FruitStoreDbContext>(options => options.UseSqlServer("Data Source=.;Initial Catalog=FruitShop;Integrated Security=True"));
            services.AddTransient<IRepository<Buy>, BuyRepository>();
            services.AddTransient<IRepository<Customer>, CustomerRepository>();
            services.AddTransient<IRepository<Fruit>, FruitRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IFruitService, FruitService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IBuyService, BuyService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
