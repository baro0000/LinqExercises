using System.Globalization;

namespace PlayGround
{
    public static class CarHelper
    {
        public static IEnumerable<Car> ByManufacturer(this IEnumerable<Car> query, string manufacturer)
        {
            return query.Where(x => x.Manufacturer.StartsWith(manufacturer));
        }

        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');
                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3], CultureInfo.InvariantCulture),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }
        }
    }
}
