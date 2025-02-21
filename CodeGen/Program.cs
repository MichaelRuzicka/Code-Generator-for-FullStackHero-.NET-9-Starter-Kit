using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraWaitForm;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CodeGen
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(ServiceProvider.GetRequiredService<frmMain>());
        }


        public static IServiceProvider ServiceProvider { get; private set; }
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                services.AddTransient<ICodebuilder, Codebuilder>();
                services.AddTransient<ITemplateLineHandler, ModuleNameLineHandler>();
                services.AddTransient<ITemplateLineHandler, Module_NamespaceLineHandler>();
                services.AddTransient<ITemplateLineHandler, EntitySetLineHandler>();
                services.AddTransient<ITemplateLineHandler, EntityLineHandler>();
                services.AddTransient<ITemplateLineHandler, EntityLowerCaseLineHandler>();
                services.AddTransient<ITemplateLineHandler, Entity_PropertyIdLineHandler>();
                services.AddTransient<ITemplateLineHandler, RequestFieldsLineHandler>();
                services.AddTransient<ITemplateLineHandler, ServiceKeyLineHandler>();
                services.AddTransient<ITemplateLineHandler, ServicesLineHandler>();
                services.AddTransient<ITemplateLineHandler, RoutesLineHandler>();

                services.AddTransient<frmMain>();
            });
        }
    }
}
