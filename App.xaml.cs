using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using WpfAppDI.Log;
using WpfAppDI.Navigation;
using WpfAppDI.Services;
using WpfAppDI.ViewModels;

namespace WpfAppDI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        private readonly IHost host;
        public App()
        {
            host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    //build.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    Configuration = builder.Build();
                })
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(services);
                }).ConfigureLogging((context, builder) =>
                {
                    builder.AddConsole();
                    builder.AddDebug();
                }).Build();
            ServiceProvider = host.Services;
        }


        protected override async void OnStartup(StartupEventArgs e)
        {
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            //Configuration = builder.Build();

            //var serviceCollecion = new ServiceCollection();
            //this.ConfigureServices(serviceCollecion);

            //ServiceProvider = serviceCollecion.BuildServiceProvider();
            //var window = ServiceProvider.GetService<MainWindow>();
            //window.ShowDialog();
            await host.StartAsync();
            //var window = ServiceProvider.GetRequiredService<MainWindow>();
            //window.Show();
            var navigationService = ServiceProvider.GetRequiredService<NavigationService>();
            await navigationService.ShowAsync(CurrentWindows.mainWindow);

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (host)
                await host.StopAsync();

            base.OnExit(e);
        }

        private void ConfigureServices(IServiceCollection services)
        {

            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            services.AddScoped(provider =>
            {
                var navigationService = new Navigation.NavigationService(provider);
                navigationService.Configure(CurrentWindows.mainWindow, typeof(MainWindow));
                navigationService.Configure(CurrentWindows.detailWindow, typeof(DetailWindow));
                return navigationService;
            });
            services.AddHttpClient();


            services.AddScoped<ISampleService, SampleService>();

            services.AddSingleton(typeof(ILoggerProvider), typeof(LogProvider));

            services.AddSingleton(typeof(MainViewModel));
            services.AddSingleton(typeof(DetailViewModel));

            services.AddSingleton(typeof(MainWindow));
            services.AddTransient(typeof(DetailWindow));
        }
    }

    public static class CurrentWindows
    {
        public const string mainWindow = nameof(MainWindow);
        public const string detailWindow = nameof(DetailWindow);

    }
}
