using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace WpfAppDI.ViewModels
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel
        {
            get
            {
                return App.ServiceProvider.GetRequiredService<MainViewModel>();
            }
        }

        public DetailViewModel DetailViewModel
        {
            get
            {
                return App.ServiceProvider.GetRequiredService<DetailViewModel>();
            }
        }
    }
}
