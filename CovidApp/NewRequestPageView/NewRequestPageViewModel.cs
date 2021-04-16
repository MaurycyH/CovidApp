using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using CovidApp.LocalRequestPageView;
using Xamarin.Forms.PlatformConfiguration;

namespace CovidApp.NewRequestPageView
{
    public class NewRequestPageViewModel : NotifyViewModel
    {
        private string mRequestTitleContent;
        private string mRequestDescriptionContent;
        private string mRequestedLocation;
        private string mBuyListContent;
        private bool mIsUsingOwnLocation = false;
        private bool mIsChecked = true;

        public string RequestTitleContent
        {
            get
            {
                return mRequestTitleContent;
            }
            set
            {
                mRequestTitleContent = value;
                OnPropertyChanged(nameof(RequestTitleContent));
            }
        }
        public string RequestDescriptionContent
        {
            get
            {
                return mRequestDescriptionContent;
            }
            set
            {
                mRequestDescriptionContent = value;
                OnPropertyChanged(nameof(RequestDescriptionContent));
            }
        }
        public string BuyListContent
        {
            get
            {
                return mBuyListContent;
            }
            set
            {
                mBuyListContent = value;
                OnPropertyChanged(nameof(BuyListContent));
            }
        }
        public bool IsUsingOwnLocation
        {
            get
            {
                return mIsUsingOwnLocation;
            }
            set
            {
                mIsUsingOwnLocation = value;
                OnPropertyChanged(nameof(IsUsingOwnLocation));
            }
        }
        public bool IsChecked
        {
            get
            {
                return mIsChecked;
            }
            set
            {
                mIsChecked = value;
                if (value == false)
                {
                    IsUsingOwnLocation = true;
                }
                else
                {
                    IsUsingOwnLocation = false;
                }
                OnPropertyChanged(nameof(IsChecked));
            }
        }
        public string RequestedLocation
        {
            get
            {
                return mRequestedLocation;
            }

            set
            {
                mRequestedLocation = value;
                OnPropertyChanged(nameof(RequestedLocation));
            }
        }
        public ICommand NewRequested { get; }
        public NewRequestPageViewModel()
        {
            NewRequested = new Command(async () =>
           {
                 await NewRequest();
           });
        }

        public async Task NewRequest()
        {
   
            Location location = await Geolocation.GetLastKnownLocationAsync();
            Location usedLocation = location;
            if (IsUsingOwnLocation == true)
            {
                if(RequestedLocation != null)
                {
                    var approximateLocation = await Geocoding.GetLocationsAsync(RequestedLocation);
                    if (approximateLocation == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Problem ze wskazanym zadresem. Spróbuj ponownie", "OK");
                    }
                    else
                    {
                        foreach (var p in approximateLocation)
                        {
                            usedLocation.Latitude = p.Latitude;
                            usedLocation.Longitude = p.Longitude;
                        }
                    }
                }
                else
                {
                  await  App.Current.MainPage.DisplayAlert("Error","Uzupełnij Adres","OK");
                }
            }

            bool validPin = IsPinDataValid(location);
            if (validPin)
            {
                RequestDB requestDb = new RequestDB
                {
                    Title = RequestTitleContent,
                    Description = RequestDescriptionContent,
                    BuyList = BuyListContent,
                    Latitude = usedLocation.Latitude,
                    Longitude = usedLocation.Longitude


                };
                await App.Database.SaveRequestAsync(requestDb);
                RequestDescriptionContent = "";
                RequestTitleContent = "";
                RequestedLocation = "";
                BuyListContent = "";
                await App.Current.MainPage.DisplayAlert("Success", "Twoje zlecenie zostalo przekazane", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Wystąpił bląd przy tworzeniu zlecenia. Sprawdź czy polą sa poprawnie wypełnione", "OK");
            }

        }
        public bool IsPinDataValid(Location position)
        {
            return !string.IsNullOrWhiteSpace(mRequestTitleContent) && !string.IsNullOrWhiteSpace(mRequestDescriptionContent);
        }
    }
}
