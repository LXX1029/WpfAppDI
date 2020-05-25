using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace WpfAppDI.Navigation
{

    public interface IActivable
    {
        Task ActivateAsync(object parameter);
    }

    public class NavigationService
    {
        private Dictionary<string, Type> windows = new Dictionary<string, Type>();

        private IServiceProvider serviceProvider { get; }
        public NavigationService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Configure(string key, Type windowType)
        {
            if (windows.ContainsKey(key)) return;
            this.windows.Add(key, windowType);
        }

        private async Task<Window> GetAndActiveWindowAsync(string windowKey, object parameter = null)
        {
            var window = this.serviceProvider.GetRequiredService(this.windows[windowKey]) as Window;
            if (window.DataContext is IActivable activable)
                await activable.ActivateAsync(parameter);
            return window;
        }

        public async Task ShowAsync(string windowKey, object parameter = null)
        {
            var window = await this.GetAndActiveWindowAsync(windowKey, parameter);
            window.Show();
        }
        public async Task ShowDialogAsync(string windowKey, object parameter = null)
        {
            var window = await this.GetAndActiveWindowAsync(windowKey, parameter);
            window.ShowDialog();
        }

    }

}
