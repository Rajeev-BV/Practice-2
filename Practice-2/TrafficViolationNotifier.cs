using System.Net.Http;
using System.Text.Json;

namespace Practice_2
{
    public class TrafficViolationNotifier
    {       
        private IVehicleSpeedDetailsProvider _vehicleSpeedDetailsProvider;
        private HttpClient _httpClient;

        public TrafficViolationNotifier(HttpClient httpClient, IVehicleSpeedDetailsProvider vehicleSpeedDetailsProvider) 
        {
            _httpClient = httpClient;
            _vehicleSpeedDetailsProvider = vehicleSpeedDetailsProvider;
        }

        public async Task<List<string>> NotifyViolations(int placeID)
        {
            List<VehicleSpeedDetails> vehicleSpeedDetailsList = _vehicleSpeedDetailsProvider.GetVehicleSpeedInALocation(placeID);

            SpeedLimitProvider speedLimitProvider = new SpeedLimitProvider(_httpClient);
            List<SpeedLimitForAVehicleTypeInALocation> speedLimitForALocation = await speedLimitProvider.GetSpeedLimitForALocation(placeID);

            List<string> trafficViolatingVehicleList = DetermineVehiclesViolatingSpeedLimit(vehicleSpeedDetailsList, speedLimitForALocation);

            return trafficViolatingVehicleList;
        }

        private static List<string> DetermineVehiclesViolatingSpeedLimit(List<VehicleSpeedDetails> vehicleSpeedDetailsList, List<SpeedLimitForAVehicleTypeInALocation> speedLimitForAVehicleTypeInALocation)
        {
            List<string> vehicleDetailsList = new List<string>();
            foreach (var vehicle in vehicleSpeedDetailsList)
            {
                var speedLimit = speedLimitForAVehicleTypeInALocation.Where(speed => vehicle.VehicleType == speed.vehicleType).First();
                if (vehicle.VehicleSpeed > speedLimit.speedLimit)
                {
                    vehicleDetailsList.Add(vehicle.VehicleNumber);
                }
            }
            return vehicleDetailsList;
        }
    }
}