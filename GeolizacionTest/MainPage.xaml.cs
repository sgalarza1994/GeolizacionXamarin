using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace GeolizacionTest
{
  public partial class MainPage : ContentPage
  {
    public MainPage()
    {
      InitializeComponent();
      Localizar();
    }

    public async void Localizar()
    {
      PermissionStatus geo = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();
      if (geo != PermissionStatus.Restricted)
        geo = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();

      var locator = CrossGeolocator.Current;
      locator.DesiredAccuracy = 30;

      if(locator.IsGeolocationAvailable)
      {
        if(locator.IsGeolocationEnabled)
        {
          if (!locator.IsListening)
            await locator.StartListeningAsync(TimeSpan.FromSeconds(1), 5);

          locator.PositionChanged += (cambio, args) =>
          {
            var loc = args.Position;
            txtLongitud.Text = loc.Longitude.ToString();
            ttxtLatitud.Text = loc.Latitude.ToString();
          };
        }
      }
    }
  }
}
