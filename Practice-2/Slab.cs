namespace Practice_2
{
    public class Slab
    {
        public int MinSlab { get; set; }    
        public int MaxSlab { get; set;}
        public float TaxPercent { get; set; }
        public Slab(int minSlab, int maxSlab, float taxPercent)
        {
            MinSlab = minSlab;
            MaxSlab = maxSlab;
            TaxPercent = taxPercent;
        }
    }
}