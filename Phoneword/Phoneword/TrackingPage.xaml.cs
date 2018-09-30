using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Net.Http;


namespace Phoneword
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrackingPage : ContentPage
    {

        public TrackingPage()
        {
            InitializeComponent();
            AccelDB db = App.Database;
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            float accelY = data.Acceleration.Y;
            AccelDisplay.Text = accelY.ToString("00.000");
            if (Math.Abs(data.Acceleration.Y) > 0.02)
            {
                if (Math.Abs(data.Acceleration.Y) > 12) OnEmergency();
                accelY = data.Acceleration.Y; //actual acceleration in Y axis, measured in Gs
                //TrackingPage.AccelDisplay.Text = f.ToString("00.000");
                AccelerationDataPoint point = new AccelerationDataPoint();
                point.time = new DateTime();
                point.accelY = accelY;
                App.Database.SaveItemAsync(point);
                //return accelY;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Accelerometer.Start(SensorSpeed.UI);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Accelerometer.Stop();
           // postToClientAsync();
        }

        private async void OnEmergency()
        {
            var markup = "{0}";
            var message = "This is an automated message to alert the proper authorities that Daniel Tao has crashed at address {0}. This is not a drill. Please send emergency vehicles to {0} immediately.";
            var address = await GetAddress();
            message = String.Format(message, address);
            var response = String.Format("{0}", message);
            markup = String.Format(markup, response);

            // Find your Account Sid and Token at twilio.com/console
            const string accountSid = "ACdc059b550aade187b46217992328ed12";
            const string authToken = "fdb3a13ab99704850ae0be34b01d63cc";

            TwilioClient.Init(accountSid, authToken);

            var call = MessageResource.Create(
                body: markup,
                from: new Twilio.Types.PhoneNumber("+18473830634"),
                to: new Twilio.Types.PhoneNumber("+12243585571")
            );

            Console.WriteLine(call.Sid);
        }

        private async Task<string> GetAddress()
        {
            try
            {
                //var location = new Location(40.728678, -73.995732);
                //if (location != null)
                //{
                //    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                //}
                //string lol = "https://reverse.geocoder.api.here.com/6.2/reversegeocode.json?app_id={0}&app_code={1}&mode=retrieveAddresses&prox={2},{3},{4}";
                //string id = "4D9ur19D8qHeuAAjvRCU";
                //string key = "9UBHFcD_4okAJZcTWQM99w";
                //HttpClient client = new HttpClient();
                //client.MaxResponseContentBufferSize = 256000;
                //var response = await client.GetAsync(String.Format(lol, id, key, location.Latitude.ToString(), location.Longitude.ToString(), 500.ToString()));
                //var content = await response.Content.ReadAsStringAsync();
                //content = content.Substring(content.IndexOf("\"Label\": "));
                //content = content.Substring(0, content.IndexOf("\""));
                //Return hardcoded address for demo purposes
                return "251 Mercer Street, New York, NY";
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception

            }
            catch (PermissionException pEx)
            {
                // Handle permission exception

            }
            catch (Exception ex)
            {
                // Unable to get location

            }
            return String.Empty;
        }

        private async Task<AccelEvent[]> ProcessData()
        {
            var entries = await App.Database.GetAllItemsAsync();
            var firstEntry = entries[0];
            var temp = new List<AccelEvent>();
            var previousEntry = entries[0];
            for (int i = 1; i < entries.Length; i++)
            {
                var currentEntry = entries[i];
                if ((currentEntry.time - previousEntry.time).Milliseconds > 5000)
                {
                    temp.Add(new AccelEvent((firstEntry.time - previousEntry.time).Seconds, Math.Abs((firstEntry.accelY + previousEntry.accelY) / 2)));
                    firstEntry = currentEntry;
                    previousEntry = currentEntry;
                }
            }
            /*var value = new Dictionary<string,>
            {
                {"accelData", entries}
            };
            */
            string readable;
            foreach (AccelerationDataPoint element in entries)
            {
                var value = JsonConvert.SerializeObject(element);
                readable = string.Concat(value);
            }
                var content = new FormUrlEncodedContent(readable);
                var response = await client.PostAsync("http://3d55d47a.ngrok.io/", content);
                var responseString = await response.Content.ReadAsStringAsync();



            return temp.ToArray();
        }
        /*accepts array, string to jsonObject?
        protected async Task postToClientAsync()
        {

        }
        */

    }

}