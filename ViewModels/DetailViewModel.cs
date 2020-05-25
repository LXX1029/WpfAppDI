using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using WpfAppDI.Navigation;
using WpfAppDI.Services;

namespace WpfAppDI.ViewModels
{
    public class DetailViewModel : ViewModelBase, IActivable
    {

        private readonly ISampleService sampleService;
        public DetailViewModel(ISampleService sampleService)
        {
            this.sampleService = sampleService;
        }

        private string _number;

        public string Number
        {
            get => _number;
            set => Set(ref _number, value);
        }


        public Task ActivateAsync(object parameter)
        {
            this.Number = parameter?.ToString();
            return Task.CompletedTask;
        }
    }
}
