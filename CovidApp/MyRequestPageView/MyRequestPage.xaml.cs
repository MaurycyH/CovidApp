using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CovidApp.MyRequestPageView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyRequestPage : ContentPage
    {
        public MyRequestPage()
        {
            InitializeComponent();
            this.BindingContext = new MyRequestPageViewModel();
        }
    }
}