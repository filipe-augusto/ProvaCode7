using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjetoCallCenter.Shared;

namespace ProjetoCallCenter.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDbContext<AppDbContext>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cliente, ClienteViewModel>();
                cfg.CreateMap<ClienteViewModel, Cliente>();

                cfg.CreateMap<Cliente, ClienteListModelView>();
                cfg.CreateMap<ClienteListModelView, Cliente>();
                

                cfg.CreateMap<Endereco, EnderecoViewModel>();
                cfg.CreateMap<EnderecoViewModel, Endereco>();


                cfg.CreateMap<Produto, ProdutoListModelView>();
                cfg.CreateMap<ProdutoListModelView, Produto>();

                cfg.CreateMap<StatusCliente, StatusClienteListModelView>();
                cfg.CreateMap<StatusClienteListModelView, StatusCliente>();

            });

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
