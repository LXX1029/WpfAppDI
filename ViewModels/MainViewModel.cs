using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WpfAppDI.Log;
using WpfAppDI.Navigation;
using WpfAppDI.Services;

namespace WpfAppDI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationService navigationService;
        private readonly ISampleService sampleService;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger logProvider;
        private readonly AppSettings appSettings;

        public MainViewModel(NavigationService navigationService, ISampleService sampleService, IOptions<AppSettings> options, IHttpClientFactory httpClientFactory, ILoggerProvider loggerProvider)
        {
            this.navigationService = navigationService;
            this.sampleService = sampleService;
            this.appSettings = options.Value;
            this.httpClientFactory = httpClientFactory;
            this.logProvider = loggerProvider.CreateLogger(nameof(MainViewModel));

        }

        private int _number = 100;
        /// <summary>
        /// 属性
        /// </summary>
        public int Number
        {
            get => _number;
            set => Set(ref _number, value);
        }


        private ICommand _setNumberCmd;

        public ICommand SetNumberCmd
        {
            get => this._setNumberCmd ??= new RelayCommand(async () =>
            {
                this.logProvider.LogInformation("execute SetNumberCmd");
                await navigationService.ShowAsync(CurrentWindows.detailWindow, this.Number);
            });

        }
        private ICommand _httpClientCmd;

        public ICommand HttpClientCmd
        {
            get => this._httpClientCmd ??= new RelayCommand(async () =>
            {
                var client = this.httpClientFactory.CreateClient();
                var result = await client.GetStringAsync("https://bbs.csdn.net/forums/DotNET");

            });

        }


    }
}
