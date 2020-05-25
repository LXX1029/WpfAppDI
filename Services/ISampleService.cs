using System;
using System.Collections.Generic;
using System.Text;

namespace WpfAppDI.Services
{
    public interface ISampleService
    {
        string GetCurrentDate();
    }
    public class SampleService : ISampleService
    {
        public string GetCurrentDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }
    }
}
