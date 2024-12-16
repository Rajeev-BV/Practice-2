using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Practice_2
{
    public class SpeedLimitProvider
    {
        private HttpClient _httpClient;

        public SpeedLimitProvider(HttpClient httpClient) { _httpClient = httpClient; }
        public async Task<List<SpeedLimitForAVehicleTypeInALocation>> GetSpeedLimitForALocation(int placeID)
        {
            var response = await _httpClient.GetAsync("/api/getSpeedLimits");
            var body = await response.Content.ReadAsStringAsync();
            var speedLimitForAVehicleTypeInALocation = JsonSerializer.Deserialize<List<SpeedLimitForAVehicleTypeInALocation>>(body);
            return speedLimitForAVehicleTypeInALocation;
        }
    }
}
