using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Common;
using VehicleTracking.Common.DTO;
using VehicleTracking.Common.Interfaces;

namespace VehicleTracking.Engine
{
    public class TrackingManager : IDisposable
    {
        #region Private Properties
        private ITrackingRepository _trackingRepository;       
        private WebClient _webClient;
        private WebClient WebClient
        {
            get
            {
                if (_webClient == null)
                {
                    _webClient = new WebClient();
                }
                return _webClient;
            }
        }
        #endregion

        #region Constructor
        public TrackingManager(ITrackingRepository trackingRepository)
        {
            _trackingRepository = trackingRepository;
        }
        #endregion

        #region Public Methods
        public async Task<bool> Record(Tracking trackingEntry)
        {
            try
            {
                trackingEntry.Time = DateTime.UtcNow.Ticks; // Using UTC time to normalize the time zone.
                await _trackingRepository.Create(trackingEntry);
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Error accured while recording tracking data for VehicleID:{trackingEntry.VehicleID}");
                throw; // Let the consumer decide what they want to do.
            }
           
        }
        public async Task<Tracking> GetLastTrackingRecord(int vehicleID)
        {
            try
            {
                var record = await _trackingRepository.GetLastTrackingEntry(vehicleID);
                record.Address = await ReverseGeocode(record.Lat, record.Long);
                return record;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Error accured while finding tracking data for VehicleID:{vehicleID}");
                throw;
            }
         
        }
        public async Task<SearchResult> Search(TrackingSearcher searcher)
        {
            try
            {
                var result = await _trackingRepository.Search(searcher);
                for (int i = 0; i < result.Path.Length; i++)
                {
                    result.Path[i].Address = await ReverseGeocode(result.Path[i].Lat, result.Path[i].Long);
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Error accured while searching tracking data for VehicleID:{searcher.VehicleID} from {(new DateTime(searcher.StartTime)).ToLongDateString()} to {(new DateTime(searcher.EndTime)).ToLongDateString()}");
                throw;
            }

        }
        #endregion

        #region Private Methods
        private async Task<string> ReverseGeocode(double lat, double lng)
        {
            // Google geocode api has a usage limit. To better utalize the limit we can keep the result in the database. 
            string address = string.Empty;
            try
            {
                var API_KEY = "AIzaSyD52P1DQ7uC_XiVlgabLywaXOudzFAXf5I"; // I have generated API key. It is forcing me to enable billing. This method is written based on the api documentation.
                //You must enable Billing on the Google Cloud Project at https://console.cloud.google.com/project/_/billing/enable Learn more at https://developers.google.com/maps/gmp-get-started
                var url = $"https://maps.googleapis.com/maps/api/geocode/json?key={API_KEY}&latlng={lat},{lng}";
               
                var json = await WebClient.DownloadStringTaskAsync(url);
                //JObject data = JsonConvert.DeserializeObject(SAMPLE_JSON) as JObject; // Only for testing.
                JObject data = JsonConvert.DeserializeObject(json) as JObject; // Working API is required.
                var status = data["status"];
                if (status.ToString().Equals("ok", StringComparison.OrdinalIgnoreCase))
                {
                    address = data["results"][0]["formatted_address"].ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex,"Error accured while getting address from coordinates"); // This is secondary feature. Ignore the error.
            }
            return address;
        }
        #endregion
       
        #region IDisposible
        public void Dispose()
        {
            _trackingRepository?.Dispose();
            _webClient?.Dispose();
        }
        #endregion

        #region Dummy Json
        private const string SAMPLE_JSON = @"{
   'plus_code' : {
      'compound_code' : 'P27Q+MC New York, NY, USA',
      'global_code' : '87G8P27Q+MC'
   },
   'results' : [
      {
         'address_components' : [
            {
               'long_name' : '279',
               'short_name' : '279',
               'types' : [ 'street_number' ]
            },
            {
               'long_name' : 'Bedford Avenue',
               'short_name' : 'Bedford Ave',
               'types' : [ 'route' ]
            },
            {
               'long_name' : 'Williamsburg',
               'short_name' : 'Williamsburg',
               'types' : [ 'neighborhood', 'political' ]
            },
            {
               'long_name' : 'Brooklyn',
               'short_name' : 'Brooklyn',
               'types' : [ 'political', 'sublocality', 'sublocality_level_1' ]
            },
            {
               'long_name' : 'Kings County',
               'short_name' : 'Kings County',
               'types' : [ 'administrative_area_level_2', 'political' ]
            },
            {
               'long_name' : 'New York',
               'short_name' : 'NY',
               'types' : [ 'administrative_area_level_1', 'political' ]
            },
            {
               'long_name' : 'United States',
               'short_name' : 'US',
               'types' : [ 'country', 'political' ]
            },
            {
               'long_name' : '11211',
               'short_name' : '11211',
               'types' : [ 'postal_code' ]
            }
         ],
         'formatted_address' : '279 Bedford Ave, Brooklyn, NY 11211, USA',
         'geometry' : {
            'location' : {
               'lat' : 40.7142484,
               'lng' : -73.9614103
            },
            'location_type' : 'ROOFTOP',
            'viewport' : {
               'northeast' : {
                  'lat' : 40.71559738029149,
                  'lng' : -73.9600613197085
               },
               'southwest' : {
                  'lat' : 40.71289941970849,
                  'lng' : -73.96275928029151
               }
            }
         },
         'place_id' : 'ChIJT2x8Q2BZwokRpBu2jUzX3dE',
         'plus_code' : {
            'compound_code' : 'P27Q+MC Brooklyn, New York, United States',
            'global_code' : '87G8P27Q+MC'
         },
         'types' : [
            'bakery',
            'cafe',
            'establishment',
            'food',
            'point_of_interest',
            'store'
         ]
      }
   ],
   'status' : 'OK'
}
";
        #endregion
    }
}
