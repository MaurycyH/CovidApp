using CovidApp.PopUpViews;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;


namespace CovidApp.LocalRequestPageView
{
    public class LocalRequestPageViewModel
    {
        List<RequestDB> RequestDbs = new List<RequestDB>();

        public ICommand MovedToLocalArea { get; }
        public ICommand RefreshedLocalArea { get; }

        public Xamarin.Forms.GoogleMaps.Map Map { get; private set; }

        public LocalRequestPageViewModel()
        {
            GetPinListAsync();
            MapSpan mapSpan = new MapSpan(new Position(52.229676, 20.012229),9,9);
            Map = new Xamarin.Forms.GoogleMaps.Map
            {
                MyLocationEnabled = true          
            };
            Map.MoveToRegion(mapSpan);
            ArrangePinsOnMap();
            MovedToLocalArea = new Command(async () =>
            {
               await MoveToLocalArea();
            });
            RefreshedLocalArea = new Command(async () =>
            {
               await RefreshLocalArea();
            });
            Map.InfoWindowClicked += InfoWindow_Clicked;
        }

        private async void InfoWindow_Clicked(object sender, InfoWindowClickedEventArgs e)
        {
            RequestPopViewModel requestPopViewModel = new RequestPopViewModel();
            requestPopViewModel.RequestTitle = e.Pin.Label;
            requestPopViewModel.PinTagHelper = (PinTagHelper)e.Pin.Tag;
            var page = new RequestPopUpView(requestPopViewModel);

            await PopupNavigation.Instance.PushAsync(page);
        }

        public async Task MoveToLocalArea()
        {

            var position =  await Geolocation.GetLastKnownLocationAsync();
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromKilometers(0.5)));
        }
        public async Task RefreshLocalArea()
        {
            await GetPinListAsync();
            ArrangePinsOnMap();
        }
        public async Task GetPinListAsync()
        {
            RequestDbs = await App.Database.GetRequestsAsync().ConfigureAwait(false);
        }
        public void ArrangePinsOnMap()
        {
            foreach (var requestDB in RequestDbs)
            {          
                Pin pin = new Pin();
                pin.Label = requestDB.Title;
                pin.Address = MakeShortDescription(requestDB.Description);
                pin.Tag = new PinTagHelper
                {
                    BuyList = MakeListFromString(requestDB.BuyList),
                    LongDescription = requestDB.Description,
                    RequestId = requestDB.ID
                };
                pin.Position = new Position(requestDB.Latitude, requestDB.Longitude);
                Map.Pins.Add(pin);
            }

        }
        public string MakeShortDescription(string Description)
        {
            if (Description.Length > 25)
            {
                Description = Description.Remove(25) + @"...";
                return Description;
            }
            else
            {
                return Description;
            }
        }
        public List<string> MakeListFromString(string StingToList)
        {
            List<string> newList = new List<string>();
            return newList = StingToList.Split(',').ToList();
        }

    }
}
