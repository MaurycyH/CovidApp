using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CovidApp.SignUpPageView
{
    public class SignUpPageViewModel : NotifyViewModel
    {
		private string mSignUpUsernameContent;
		private string mSignUpPasswordsContent;
		private string mSignUpEmailContent;
		public string SignUpUsernameContent
		{
			get
            {
				return mSignUpUsernameContent;
            }
			set
			{
				mSignUpUsernameContent = value;
				OnPropertyChanged(nameof(SignUpUsernameContent));
			}
		}
		public string SignUpPasswordContent
		{
			get
            {
				return mSignUpPasswordsContent;
			}
			set
			{
				mSignUpPasswordsContent = value;
				OnPropertyChanged(nameof(SignUpPasswordContent));
			}
		}
		public string SignUpEmailContent
		{
			get
            {
				return mSignUpEmailContent;
			}
			set
			{
				mSignUpEmailContent = value;
				OnPropertyChanged(nameof(SignUpEmailContent));
			}
		}
		public ICommand OnSignedUpButtonClick { get; }
		public ICommand OnGoBackLoginPageButtonCick { get; }
		public SignUpPageViewModel()
        {
			OnSignedUpButtonClick = new Command(async () =>
			{
				await OnSignedUpButtonClicked();
			});
			OnGoBackLoginPageButtonCick = new Command(() =>
		   {
			   OnGoBackLoginPageButtonCicked();
		   });

		}
       async Task OnSignedUpButtonClicked()
       {

			bool signUpSucceeded = AreDetailsValid();
			if (signUpSucceeded)
			{
				await App.Database.SavePersonAsync(new UserDB
				{
					Username = mSignUpUsernameContent,
					Password = mSignUpPasswordsContent,
					Email = mSignUpEmailContent
				});
				await App.Current.MainPage.DisplayAlert("Sukces", "Twoje konto zostało poprawnie swtorzonie, Teraz mozesz sie zalogować", "OK");
				App.Current.MainPage = new LoginPageView.LoginPage();
			}
			else
            {
				await App.Current.MainPage.DisplayAlert("Błąd", "Błąd przy Tworzeniu użytkownika", "Kontynuuj");

			}
		}
		void OnGoBackLoginPageButtonCicked()
        {
			mSignUpEmailContent = "";
			mSignUpPasswordsContent = "";
			mSignUpUsernameContent = "";
			App.Current.MainPage = new LoginPageView.LoginPage();
        }

		bool AreDetailsValid()
		{
			return (!string.IsNullOrWhiteSpace(mSignUpUsernameContent) && !string.IsNullOrWhiteSpace(mSignUpPasswordsContent) && !string.IsNullOrWhiteSpace(mSignUpEmailContent) && mSignUpEmailContent.Contains("@"));
		}
	}
}
