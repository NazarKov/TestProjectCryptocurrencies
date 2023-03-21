using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TestProjectCryptocurrencies.Model.Interface;

namespace TestProjectCryptocurrencies.Model
{
    public static class Navigation
    {
        public static INavigationServise? NavigationServise { get;private set; }

        public static void Init(Frame frame)
        {
            NavigationServise = new NavigationServise(frame);
        }
    }
}
