using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CovidApp.MyRequestPageView
{
    public class MyRequestPageViewModel : NotifyViewModel
    {


        private List<RequestDB> mRawRequests = new List<RequestDB>();

        public List<RequestDB> RawRequests
        {
            get
            {
                return mRawRequests;
            }
            set
            {
                mRawRequests = value;
                OnPropertyChanged(nameof(RawRequests));
            }
        }

        public MyRequestPageViewModel()
        {
            GetMyRequests();
        }

        public async void GetMyRequests()
        {
            RawRequests = await App.Database.GetUserRequestsAsync().ConfigureAwait(false);
        }
    }
}