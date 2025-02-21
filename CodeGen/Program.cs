using DevExpress.CodeParser;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraWaitForm;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
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
                services.AddTransient<Codebuilder>();
                services.AddTransient<ITemplateLineHandler, ModuleNameLineHandler>();
                services.AddTransient<ITemplateLineHandler, Root_NamespaceLineHandler>();
                services.AddTransient<ITemplateLineHandler, Module_NamespaceLineHandler>();
                services.AddTransient<ITemplateLineHandler, EntitySetLineHandler>();
                services.AddTransient<ITemplateLineHandler, EntityLineHandler>();
                services.AddTransient<ITemplateLineHandler, EntityLowerCaseLineHandler>();
                services.AddTransient<ITemplateLineHandler, Entity_PropertyIdLineHandler>();
                services.AddTransient<ITemplateLineHandler, RequestFieldsLineHandler>();
                services.AddTransient<ITemplateLineHandler, EntityItemFieldsLineHandler>();
                services.AddTransient<ITemplateLineHandler, ServiceKeyLineHandler>();
                services.AddTransient<ITemplateLineHandler, ServicesLineHandler>();
                services.AddTransient<ITemplateLineHandler, RoutesLineHandler>();
                services.AddTransient<ITemplateLineHandler, PropertyListingLineHandler>();
                services.AddTransient<ITemplateLineHandler, PropertyConstructorLineHandler>();
                services.AddTransient<frmMain>();
            });
        }


        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ShowExceptionDetails(e.ExceptionObject as Exception);
            System.Windows.Forms.Application.Exit();
        }

        static void ShowExceptionDetails(Exception Ex)
        {

            MessageBox.Show(Ex.Message, Ex.TargetSite.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
