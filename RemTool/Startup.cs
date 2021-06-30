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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using RemTool.Infrastructure.Interfaces.Services;
using RemTool.Infrastructure.Additional;
using RemTool.Services.MongoDB;
using RemTool.Services.FileSystem;
using RemTool.Services.Additional;
using Microsoft.Extensions.FileProviders;
using MongoDB.Driver;
using RemTool.Models;
using MongoDB.Bson;

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
            services.Configure<RemToolMongoDBsettings>(
                Configuration.GetSection(nameof(RemToolMongoDBsettings)));

            services.AddSingleton<IRemToolMongoDBsettings>(sp =>
                sp.GetRequiredService<IOptions<RemToolMongoDBsettings>>().Value);

            services.AddSingleton<IMetaDataService, MetaDataService>();
            services.AddSingleton<IRtMailSettingsService, RtMailSettingsService>();
            services.AddSingleton<RtMailMessageService>();
            services.AddTransient<IFileImageService, FileImageService>();
            services.AddSingleton<ISparePartService, SparePartService>();
            services.AddSingleton<IToolTypeService, ToolTypeService>();
            services.AddSingleton<IClickCounterService, ClickCounterService>();
            services.AddSingleton<IRtRequestService, RtRequestService>();
            services.AddSingleton<IBackUpService, BackUpService>();
            services.AddSingleton<IToolTypeSearchService, ToolTypeSearchService>();


            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = "RemToolBack",
                        ValidAudience = "RemToolFront",

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("whoWillSaveYouNow?123456789+-"))
                    };
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            InitMainFolders(env.WebRootPath, env.ContentRootPath);
            InitRtMailSettings();
            InitMetaData();

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseCors();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

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

        public void InitMainFolders(string webRootPath, string appPath)
        {
            string path = webRootPath;
            string pathToApp = appPath;

            // images folder
            DirectoryInfo di_images = new DirectoryInfo(path + "/images/");
            if (!di_images.Exists)
            {
                di_images.Create();
            }

            // backUpFolder
            DirectoryInfo di_backUp = new DirectoryInfo(pathToApp + "/backup/");
            if (!di_backUp.Exists)
            {
                di_backUp.Create();
            }

            DirectoryInfo di_backUpTemp = new DirectoryInfo(pathToApp + "/backup/" + "/temp/");
            if (!di_backUpTemp.Exists)
            {
                di_backUpTemp.Create();
            }

            DirectoryInfo di_backUpTempImg = new DirectoryInfo(pathToApp + "/backup/" + "/temp/" + "/images/");
            if (!di_backUpTempImg.Exists)
            {
                di_backUpTempImg.Create();
            }

            DirectoryInfo di_backUpTempJson = new DirectoryInfo(pathToApp + "/backup/" + "/temp/" + "/json/");
            if (!di_backUpTempJson.Exists)
            {
                di_backUpTempJson.Create();
            }

            DirectoryInfo di_backUpArch = new DirectoryInfo(pathToApp + "/backup/" + "/archive/");
            if (!di_backUpArch.Exists)
            {
                di_backUpArch.Create();
            }
        }

        public void InitRtMailSettings()
        {
            // первоначальная инициализация настроек Mail
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("RemTool");

            var _rtMailSettingsCol = database.GetCollection<RtMailSettings>("RtMailSettings");

            // проверка существования объекта
            if (_rtMailSettingsCol.Find(new BsonDocument()).FirstOrDefault() == null)
            {
                // если объекта нет - то создать по умолчанию
                RtMailSettings rtms = new RtMailSettings()
                {
                    // флаг об отправке оповещений на почту админа
                    SendNotificationToHQ = false,

                    // почта админа для оповещений
                    HQeMail = "",

                    // флаг об отправке на почту клиенту
                    SendNotificationToClient = false,

                    // сообщение по умолчанию в письме запросящему
                    DefaultMessageToClient = "< h3 > Ваш запрос передан, с Вами свяжутся...</ h3 >",

                    // почта за счёт которой идёт отправка
                    Credentials_Name = "",

                    Credentials_Pass = "",

                    // SMTP сервер предоставляющий услуги отправки почты
                    SmtpServer_Host = "smtp.yandex.ru",

                    SmtpServer_Port = "587"  //465 // 587
                };

                _rtMailSettingsCol.InsertOne(rtms);

            }
        }

        public void InitMetaData()
        {
            // первоначальная инициализация метаданных - телефон и т.д.
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("RemTool");

            var _rtMetaData = database.GetCollection<MetaData>("RtMetaData");

            // проверка существования объекта
            if (_rtMetaData.Find(new BsonDocument()).FirstOrDefault() == null)
            {
                // если объекта нет - то создать по умолчанию
                MetaData md = new MetaData()
                {
                    PhoneNumber = "+70123456789"
                };

                _rtMetaData.InsertOne(md);
            }
        }
    }
}
