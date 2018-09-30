using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Essentials;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Plugin.Geolocator;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Phoneword
{
    class AccelerationReader : INotifyPropertyChanged
    { 
        public SensorSpeed speed = SensorSpeed.UI;
        public float accelY;

        public event PropertyChangedEventHandler PropertyChanged;

        public AccelerationReader()
        {
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            CallEmergency();
        }

        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            if (Math.Abs(data.Acceleration.Y) > 0.02)
            {
                if (Math.Abs(data.Acceleration.Y) > 150) CallEmergency();
                Console.WriteLine($"IN REGULAR PROJ: Reading: X: {data.Acceleration.X}, Y: " +
                $"{data.Acceleration.Y}, Z: {data.Acceleration.Z}");
                accelY = data.Acceleration.Y;

                accelY = data.Acceleration.Y; //actual acceleration in Y axis, measured in Gs
                //TrackingPage.AccelDisplay.Text = f.ToString("00.000");
                AccelerationDataPoint point = new AccelerationDataPoint();
                point.time = new DateTime();
                point.accelY = accelY;
                App.Database.SaveItemAsync(point);
                Console.WriteLine($"Wrote {accelY} to database");
                //return accelY;
            }

            //return 0.0;
        }

        public void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                else
                    Accelerometer.Start(speed);
            }
            catch (FeatureNotSupportedException fns)
            {
                //lmao
            }
            catch (Exception ex)
            {
                //lmao 2 the electric bungaloo
            }

        }

        private async void CallEmergency()
        {
            var markup = "<Response>{0}{1}{2}{3}{4}{5}<Response>";
            var message = "This is an automated message to alert the proper authorities that Daniel Tao has crashed at address {0}. This is not a drill. Please send emergency vehicles to {} immediately.";
            message = String.Format(message, await GetAddress());
            var response = String.Format("<Say>{0}</Say>", message);
            markup = String.Format(markup, message, message, message, message, message);

            // Find your Account Sid and Token at twilio.com/console
            const string accountSid = "ACdc059b550aade187b46217992328ed12";
            const string authToken = "fdb3a13ab99704850ae0be34b01d63cc";

            TwilioClient.Init(accountSid, authToken);

            var call = MessageResource.Create(
                body: "This is the ship that made the Kessel Run in fourteen parsecs?",
                from: new Twilio.Types.PhoneNumber("+18473830634"),
                to: new Twilio.Types.PhoneNumber("+12243585571")
            );

            Console.WriteLine(call.Sid);
        }

        private async Task<string> GetAddress()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            string lol = "https://reverse.geocoder.api.here.com/6.2/reversegeocode.json?app_id={0},&app_code={1}&mode=retrieveAddresses&prox={2},{3},{4}";
            string id = "";
            string key = "";
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            var response = await client.GetAsync(String.Format(lol, id, key, position.Latitude.ToString(), position.Longitude.ToString(), 50.ToString()));
            var content = await response.Content.ReadAsStringAsync();
            content = content.Substring(content.IndexOf("\"Label\": "));
            content = content.Substring(0, content.IndexOf("\""));
            return content;
        }

    }
}
