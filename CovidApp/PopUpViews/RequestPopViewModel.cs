using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CovidApp.PopUpViews
{
    public class RequestPopViewModel : NotifyViewModel
    {
        private string mRequestTitle;
        private string mRequestDescription;
        private List<string> mBuyList;
        private PinTagHelper mPinTagHelper;

        public string RequestTitle
        {
            get
            { 
                return mRequestTitle;
            }
            set
            { 
                mRequestTitle = value;
                OnPropertyChanged(nameof(RequestTitle));
            }
        }
        public string RequestDescription
        {
            get 
            { 
                return mRequestDescription; 
            }
            set 
            { 
                mRequestDescription = value; 
                OnPropertyChanged(nameof(RequestDescription));
            }
        }

        public List<string> BuyList
        {
            get
            {
                return mBuyList;
            }
            set
            {
                mBuyList = value;
                OnPropertyChanged(nameof(BuyList));
            }
        }
        public PinTagHelper PinTagHelper
        {
            get
            {
                return mPinTagHelper;
            }
            set
            {
                mPinTagHelper = value;
                RequestDescription = PinTagHelper.LongDescription;
                BuyList = PinTagHelper.BuyList;
            }
        }
        public ICommand RequestAccept;
        public RequestPopViewModel()
        {

        }
 
    }
}
