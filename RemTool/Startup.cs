using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

using RemTool.Infrastructure.Interfaces.Services;
using RemTool.Infrastructure.Additional;
using RemTool.Services.MongoDB;
using RemTool.Services.FileSystem;


namespace RemTool
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // ����������� ���������� ��� �������������� appsetings.json � ������������ connectionString � databaseName
            services.Configure<RemToolMongoDBsettings>(
                Configuration.GetSection(nameof(RemToolMongoDBsettings)));

            services.AddSingleton<IRemToolMongoDBsettings>(sp =>
                sp.GetRequiredService<IOptions<RemToolMongoDBsettings>>().Value);


            services.AddTransient<IFileImageService, FileImageService>();
            services.AddSingleton<IBrandService, BrandService>();
            services.AddSingleton<IToolService, ToolService>();
            services.AddSingleton<ISparePartService, SparePartService>();



            services.AddControllers();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // ���������� ����� ��� �������� �������� ���� ��� �� ������
            string path = env.WebRootPath;
            DirectoryInfo di_images = new DirectoryInfo(path + "/images/");
            if (!di_images.Exists)
            {
                di_images.Create();
            }


            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });


        }
    }
}
