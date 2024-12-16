using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practice_2;

namespace PracticeTest
{
    internal class SpeedLimitChecker_Test
    {
        [Test]
        public void Test_When_Vehicle_Is_Over_Speeding()
        {
            Dictionary<string, int> LocationMap = new Dictionary<string, int>();
            int ExpectedFineCost = 0;
            LocationMap.Add("Location2", 60);
          
           
            bool IsVehicleOverSpeeding;


            SpeedLimitChecker speedLimitChecker = new SpeedLimitChecker();
            IsVehicleOverSpeeding = speedLimitChecker.CheckSpeedLimitAndCost(LocationMap, 60);

            Assert.IsFalse(IsVehicleOverSpeeding);

        }
        [Test]
        public void Test_FineCost_When_Vehicle_Is_Over_Speeding()
        {
            string VehicleType = "Car";
            Dictionary<string, int> FineCostMap= new Dictionary<string, int>();
            int ExpectedFineCost = 0;
            FineCostMap.Add("Car", 1000);
            FineCostMap.Add("Bus", 2000);

            SpeedLimitChecker speedLimitChecker= new SpeedLimitChecker();
            ExpectedFineCost = speedLimitChecker.CostOfFine(VehicleType, FineCostMap);

            Assert.AreEqual(1000, ExpectedFineCost);

        }
    }
}
