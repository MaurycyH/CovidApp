using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using CovidApp.SignUpPageView;
using System.Threading.Tasks;

namespace CovidApp.LoginPageView
{
    public class LoginPageViewModel : NotifyViewModel
    {
        private string mUsernameContent;
        private string mPasswordsContent;
        public string UsernameContent
        {
            get 
            {
                return mUsernameContent;
            }

            set
            {
                mUsernameContent = value;
                OnPropertyChanged(nameof(UsernameContent));
            }
        }
        public string PasswordContent
        {
            get
            {
                return mPasswordsContent;
            }
            set
            {
                mPasswordsContent = value;
                OnPropertyChanged(PasswordContent);
            }
        }
        public ICommand OnLoginButtonClick { get; }
        public ICommand OnSignUpButtonClick { get; }
        public LoginPageViewModel()
        {
            OnLoginButtonClick = new Command(async () =>
            {
                await OnLoginButtonClicked();
            });
            OnSignUpButtonClick = new Command(() =>
             {
                 OnSignUpButtonClicked();
             });
        }

        void OnSignUpButtonClicked()
        {
            App.Current.MainPage = new SignUpPage();
        }

        async Task OnLoginButtonClicked()
        {
            var isValid = AreCredentialsCorrect();
            if (isValid)
            {
                App.Database.ActualUserId = App.Database.GetUserUsername(UsernameContent).Result.ID;
                UsernameContent = "";
                PasswordContent = "";
                Application.Current.MainPage = new NavigationPage(new MainPageView.MainPage());

            }
            else
            {
                UsernameContent = "";
                PasswordContent = "";
                await App.Current.MainPage.DisplayAlert("Error", "Błędna nazwa użytkownika lub hasło", "OK");
            }
        }
        bool AreCredentialsCorrect()
        {
            var UserDb = App.Database.GetUserUsername(UsernameContent);
            if (UserDb.Result != null)
            {
                return UsernameContent == UserDb.Result.Username && PasswordContent == UserDb.Result.Password;
            }
            else
            {
                return false;
            }
        }
    }
}
