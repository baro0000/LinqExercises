namespace PlayGround
{
    public class Car
    {
        public int Year { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public double Displacement { get; set; }
        public int Cylinders { get; set; }
        public int City { get; set; }
        public int Highway { get; set; }
        public int Combined { get; set; }

        public override string ToString()
        {
            return $"Car name: {Name}, \n\tManufacturer: {Manufacturer}, \n\tYear: {Year}";
        }
    }
}
