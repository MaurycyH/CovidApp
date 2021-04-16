using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CovidApp.PopUpViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequestPopUpView : PopupPage
    {
        RequestPopViewModel RequestPopViewModel = new RequestPopViewModel();
        public RequestPopUpView(RequestPopViewModel requestPopViewModel)
        {
            InitializeComponent();
            RequestPopViewModel = requestPopViewModel;
            BindingContext = RequestPopViewModel;
        }
        private async void OnClose(object sender, EventArgs e)
        {
           await  PopupNavigation.Instance.PopAsync();
        }
        private async void OnAccept(object sender, EventArgs e)
        {
            var IsTaken = await App.Database.UpdateRequest(RequestPopViewModel.PinTagHelper.RequestId);
            if (!IsTaken)
            {

                await Navigation.PushPopupAsync(new RequestAcceptedPopUpView());
            }
            else
            {
                await Navigation.PushPopupAsync(new RequestDeclinedPopUpView());
            }
            AcceptButton.IsEnabled = false;
            AcceptButton.Opacity = 0.5;
        }
    }
}