using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_2
{
    public class SpeedLimitChecker
    {
        public bool CheckSpeedLimitAndCost(Dictionary<string, int> Location, int SpeedOfVehicle)
        {
            bool speedLimitExceeded = false;
            foreach (var location in Location)
            {
                if (SpeedOfVehicle > location.Value)
                {
                    speedLimitExceeded = true;
                }
            }
          
            return speedLimitExceeded;
        }

   

        public int CostOfFine(string VehicleType, Dictionary<string, int> FineMap) 
        {
            int FineCost = 0;
            foreach (var fine in FineMap)
            {
                if (fine.Key == VehicleType){
                    FineCost = fine.Value;
                }
            }
            return FineCost;
        
        }

      
    }
}
