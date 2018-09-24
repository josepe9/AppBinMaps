using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace AppBinMaps
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        double miLatitud = 0;
        double miLongitud = 0;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            int vista = cmbVista.SelectedIndex;
            int sede = cmbSedes.SelectedIndex;
            double longi = 0;
            double lati = 0;
            switch (sede)
            {
                case 1:
                    lati = 4.430005;  //48.858;  4.430005
                    longi = -75.214527; // 2.295;  -75.214527
                    if (vista == 1) { showStreetsideView(lati, longi); } else { if (vista == 2) { display3DLocation(lati, longi); } }
                    break;
                case 2:
                    lati = 4.439730; longi = -75.223361;
                    if (vista == 1) { showStreetsideView(lati, longi); } else { if (vista == 2) { display3DLocation(lati, longi); } }
                    break;
                case 3:
                    lati = 4.832497; longi = -75.689912;
                    if (vista == 1) { showStreetsideView(lati, longi); } else { if (vista == 2) { display3DLocation(lati, longi); } }
                    break;
                case 4:
                    lati = 2.933404; longi = -75.286410;
                    if (vista == 1) { showStreetsideView(lati, longi); } else { if (vista == 2) { display3DLocation(lati, longi); } }
                    break;
                case 5:
                    lati = 4.811328; longi = -75.697628;
                    if (vista == 1) { showStreetsideView(lati, longi); } else { if (vista == 2) { display3DLocation(lati, longi); } }
                    break;
                case 6:
                    lati = 4.621361; longi = -74.105679;
                    if (vista == 1) { showStreetsideView(lati, longi); } else { if (vista == 2) { display3DLocation(lati, longi); } }
                    break;
                case 7:
                    lati = 5.543674; longi = -73.351221;
                    if (vista == 1) { showStreetsideView(lati, longi); } else { if (vista == 2) { display3DLocation(lati, longi); } }
                    break;
                case 8:
                    lati = 6.245105; longi = -75.590591;
                    if (vista == 1) { showStreetsideView(lati, longi); } else { if (vista == 2) { display3DLocation(lati, longi); } }
                    break;
                case 9:
                    lati = 1.147901; longi = -76.647973;
                    if (vista == 1) { showStreetsideView(lati, longi); } else { if (vista == 2) { display3DLocation(lati, longi); } }
                    break;
                case 10:
                    lati = 1.201162; longi = -77.279201;
                    if (vista == 1) { showStreetsideView(lati, longi); } else { if (vista == 2) { display3DLocation(lati, longi); } }
                    break;
            }
        }

        //Streetview
        private async void showStreetsideView(double lati, double longi)
        {
            // Check if Streetside is supported.
            if (myMap.IsStreetsideSupported)
            {
                // Find a panorama near Avenue Gustave Eiffel.
                BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = lati, Longitude = longi };
                Geopoint cityCenter = new Geopoint(cityPosition);
                StreetsidePanorama panoramaNearCity = await StreetsidePanorama.FindNearbyAsync(cityCenter);

                // Set the Streetside view if a panorama exists.
                if (panoramaNearCity != null)
                {
                    // Create the Streetside view.
                    StreetsideExperience ssView = new StreetsideExperience(panoramaNearCity);
                    ssView.OverviewMapVisible = true;
                    myMap.CustomExperience = ssView;
                }
            }
            else
            {
                // If Streetside is not supported
                ContentDialog viewNotSupportedDialog = new ContentDialog()
                {
                    Title = "Streetside is not supported",
                    Content = "\nStreetside views are not supported on this device.",
                    PrimaryButtonText = "OK"
                };
                await viewNotSupportedDialog.ShowAsync();
            }
        }

        //Vista en 3d
        private async void display3DLocation(double lati, double longi)
        {
            if (myMap.Is3DSupported)
            {
                // Set the aerial 3D view.
                myMap.Style = MapStyle.Aerial3DWithRoads;

                // Specify the location.

              //  43.773251
               //     11.255474

                BasicGeoposition hwGeoposition = new BasicGeoposition() { Latitude = lati, Longitude = longi };
                Geopoint hwPoint = new Geopoint(hwGeoposition);

                // Create the map scene.
                MapScene hwScene = MapScene.CreateFromLocationAndRadius(hwPoint,
                                                                                     80, /* show this many meters around */
                                                                                     0, /* looking at it to the North*/
                                                                                     60 /* degrees pitch */);
                // Set the 3D view with animation.
                await myMap.TrySetSceneAsync(hwScene, MapAnimationKind.Bow);
            }
            else
            {
                // If 3D views are not supported, display dialog.
                ContentDialog viewNotSupportedDialog = new ContentDialog()
                {
                    Title = "3D is not supported",
                    Content = "\n3D views are not supported on this device.",
                    PrimaryButtonText = "OK"
                };
                await viewNotSupportedDialog.ShowAsync();
            }
        }


        private void MyMap_MapTapped(Windows.UI.Xaml.Controls.Maps.MapControl sender, Windows.UI.Xaml.Controls.Maps.MapInputEventArgs args)
        {

        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
           
        }

        

        private void BtnGetDirections_click(object sender, RoutedEventArgs e)
        {

        }

        private void Syscafe_click(object sender, RoutedEventArgs e)
        {
            //Oficina principal
            string titulo = "SysCafé SAS, Cr 3 No.38A-18 B/La castellana Sede principal";
            double lati = 4.429893;
            double longi = -75.214561;
            Mapa(titulo, lati, longi);
        }

        //Tomar ubicación actual
        protected  async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Specify a known location.
            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 4.429893, Longitude = -75.214561 };
            Geopoint cityCenter = new Geopoint(cityPosition);
            // Set the map location.
            myMap.Center = cityCenter;
            myMap.ZoomLevel = 12;
            myMap.LandmarksVisible = true;

            // Set your current location.
            var accessStatus = await Geolocator.RequestAccessAsync();
             switch (accessStatus)
             {
                 case GeolocationAccessStatus.Allowed:

                     // Get the current location.
                     Geolocator geolocator = new Geolocator();
                     Geoposition pos = await geolocator.GetGeopositionAsync();
                     Geopoint myLocation = pos.Coordinate.Point;

                     // Set the map location.
                     myMap.Center = myLocation;
                     myMap.ZoomLevel = 12;
                     myMap.LandmarksVisible = true;

                    miLatitud = myLocation.Position.Latitude;
                    miLongitud = myLocation.Position.Longitude;
                    break;

                 case GeolocationAccessStatus.Denied:
                     // Handle the case  if access to location is denied.
                     break;

                 case GeolocationAccessStatus.Unspecified:
                     // Handle the case if  an unspecified error occurs.
                     break;
             }
        }

        //Mostrar rutas
        private async void ShowRouteOnMap(double lati, double longi)
        {
            //posicion actual
            BasicGeoposition startLocation = new BasicGeoposition() { Latitude = miLatitud, Longitude = miLongitud };

            // posicion final
            BasicGeoposition endLocation = new BasicGeoposition() { Latitude = lati, Longitude = longi };


            // Obtener ruta
            MapRouteFinderResult routeResult =
                  await MapRouteFinder.GetDrivingRouteAsync(
                  new Geopoint(startLocation),
                  new Geopoint(endLocation),
                  MapRouteOptimization.Time,
                  MapRouteRestrictions.None);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Yellow;
                viewOfRoute.OutlineColor = Colors.Black;

                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                myMap.Routes.Add(viewOfRoute);

                // Fit the MapControl to the route.
                await myMap.TrySetViewBoundsAsync(
                      routeResult.Route.BoundingBox,
                      null,
                      Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
            }
        }

        private void vistas(object sender, RoutedEventArgs e)
        {
            int estilo = cmbEstilos.SelectedIndex;
            switch (estilo)
            {
                case 1: myMap.StyleSheet = MapStyleSheet.RoadDark(); break;
                case 2: myMap.StyleSheet = MapStyleSheet.Aerial(); break;
                case 3: myMap.StyleSheet = MapStyleSheet.RoadLight(); break;
                case 4: myMap.StyleSheet = MapStyleSheet.AerialWithOverlay(); break;
                case 5: myMap.StyleSheet = MapStyleSheet.RoadHighContrastLight(); break;
            }
        }

        private void Sedes(object sender, RoutedEventArgs e)
        {
            int sede = cmbSedes.SelectedIndex;
            string titulo = "";
            double longi = 0;
            double lati = 0;
            switch (sede)
            {
                case 1:titulo = "SysCafé SAS, Cr 3 No.38A-18 B/La castellana Sede principal";
                    lati = 4.429998; longi = -75.214582; break;
                case 2: titulo = "SysCafé SAS, Centro Comercial la 5ta Cr 5 cll 29 Sala ventas";
                    lati = 4.439730; longi = -75.223361; break;
                case 3: titulo = "SysCafé Pereira, Sala ventas";
                    lati = 4.832497; longi = -75.689912; break;
                case 4: titulo = "SysCafé Neiva, Sala ventas";
                    lati = 2.933404; longi = -75.286410; break;
                case 5:titulo = "SysCafé SAS, Eje cafetero sala de ventas";
                    lati = 4.811328; longi = -75.697628; break;
                case 6: titulo = "SysCafé SAS, Bogotá sala de ventas";
                    lati = 4.621361; longi = -74.105679; break;
                case 7: titulo = "SysCafé SAS, Tunja sala de ventas";
                    lati = 5.543674; longi = -73.351221; break;
                case 8: titulo = "SysCafé SAS, Medellín sala de ventas";
                    lati = 6.245105; longi = -75.590591; break;
                case 9: titulo = "SysCafé SAS, Mocoa sala de ventas";
                    lati = 1.147901; longi = -76.647973; break;
                case 10: titulo = "SysCafé SAS, Pasto sala de ventas";
                    lati = 1.201162; longi = -77.279201; break;
            }
            Mapa(titulo, lati, longi);
        }

        //Mostrar el mapa por sede
        private void Mapa(string titulo, double lati, double longi)
        {
            BasicGeoposition snPosition2 = new BasicGeoposition() { Latitude = lati, Longitude = longi };
            Geopoint snPoint2 = new Geopoint(snPosition2);
            var mapIcon2 = new MapIcon
            {
                Location = snPoint2,
                NormalizedAnchorPoint = new Point(0.5, 1.0),
                Title = titulo,
                ZIndex = 0
            };
            myMap.MapElements.Add(mapIcon2);
            myMap.Center = snPoint2;
            myMap.ZoomLevel = 16;
        }

        private void BtnRuta_click(object sender, RoutedEventArgs e)
        {
            int sede = cmbSedes.SelectedIndex;
            double longi = 0;
            double lati = 0;
            switch (sede)
            {
                case 1: lati = 4.429998; longi = -75.214582; break;
                case 2: lati = 4.439730; longi = -75.223361; break;
                case 3: lati = 4.832497; longi = -75.689912; break;
                case 4: lati = 2.933404; longi = -75.286410; break;
                case 5: lati = 4.811328; longi = -75.697628; break;
                case 6: lati = 4.621361; longi = -74.105679; break;
                case 7: lati = 5.543674; longi = -73.351221; break;
                case 8: lati = 6.245105; longi = -75.590591; break;
                case 9: lati = 1.147901; longi = -76.647973; break;
                case 10: lati = 1.201162; longi = -77.279201; break;
            }
            ShowRouteOnMap(lati,longi);
        }
    }
}
