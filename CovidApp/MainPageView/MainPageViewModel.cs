using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using CovidApp.NewRequestPageView;
using CovidApp.LocalRequestPageView;
using CovidApp.LoginPageView;
using CovidApp.MyRequestPageView;

namespace CovidApp.MainPageView
{
    class MainPageViewModel
    {
        public ICommand LocalRequest { get; }
        public ICommand NewRequest { get; }
        public ICommand MyRequest { get; }
        public ICommand OnLogOutButtonClick { get; }

        public MainPageViewModel()
        {
            LocalRequest = new Command(async () =>
            {
                LocalRequestPageViewModel localrequestPageViewModel = new LocalRequestPageViewModel();
                LocalRequestPage localRequestPage = new LocalRequestPage();
                localRequestPage.BindingContext = localrequestPageViewModel;
                await Application.Current.MainPage.Navigation.PushAsync(localRequestPage);
            });
            NewRequest = new Command(async () =>
            {
                NewRequestPageViewModel requestPageViewModel = new NewRequestPageViewModel();
                NewRequestPage newrequestPage = new NewRequestPage();
                newrequestPage.BindingContext = requestPageViewModel;
                await Application.Current.MainPage.Navigation.PushAsync(newrequestPage);

            });            
            MyRequest = new Command(async () =>
            {
                MyRequestPageViewModel myRequestPageViewModel = new MyRequestPageViewModel();
                MyRequestPage myRequestPage = new MyRequestPage();
                myRequestPage.BindingContext = myRequestPageViewModel;
                await Application.Current.MainPage.Navigation.PushAsync(myRequestPage);
            });

            OnLogOutButtonClick = new Command(() =>
            {
                App.Current.MainPage = new LoginPage();
            });
        }

    }
}
