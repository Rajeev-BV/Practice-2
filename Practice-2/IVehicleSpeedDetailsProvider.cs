namespace Practice_2
{
    public interface IVehicleSpeedDetailsProvider
    {
        public List<VehicleSpeedDetails> GetVehicleSpeedInALocation(int placeID);
    }
}