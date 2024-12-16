namespace Practice_2
{
    public class VehicleSpeedDetails
    {
        public VehicleSpeedDetails(string vehicleNumber, int vehicleSpeed, int placeID, string vehicleType)
        {
            VehicleNumber = vehicleNumber;
            VehicleSpeed = vehicleSpeed;
            PlaceID = placeID;
            VehicleType = vehicleType;
        }

        public string VehicleNumber { get; set; }
        public int VehicleSpeed { get; set;}
        public int PlaceID { get; set; }
        public string VehicleType { get; set; }
    }

   
}