using Microsoft.VisualBasic;
using PlayGround.CSVFiles;
using System.Text;
using System.Xml.Linq;

namespace PlayGround
{
    public class App : IApp
    {
        private readonly ICSVRearer _csvReader;
        public App(ICSVRearer csvReader)
        {
            _csvReader = csvReader;
        }

        public void Run()
        {
            var cars = _csvReader.ProcessCars("CSVFiles\\fuel.csv");
            var manufacturers = _csvReader.ProcessManufacturers("CSVFiles\\manufacturers.csv");
            ////-----LINQ-------
            ////Select
            //var carManufacturersNames = GetSpecificColumns(cars);
            //Display(carManufacturersNames);

            ////Select anonymous class
            //var carManufacturersNamesAnonymous = Anonymous(cars);
            //Console.WriteLine(carManufacturersNamesAnonymous);

            ////OrderBy
            //var sortedList = OrderByManufacturer(cars);
            //Display(sortedList);

            ////OrderBy descending
            //var sortedList = OrderByManufacturerDescending(cars);
            //Display(sortedList);

            ////OrderBy manufacturer and name
            //var sortedList = OrderByManufacturerAndName(cars);
            //Display(sortedList);

            ////where starts with
            //var filteredlist = WhereStartsWith(cars, "aston");
            //Display(filteredlist);

            ////Where starts with and combined less more 15
            //var filteredList = WhereStartsWithAndCombined(cars, "Aston");
            //Display(filteredList);

            ////Where starts with manufacturer - extension
            //var filteredList = cars.ByManufacturer("Aston").ToList();
            //Display(filteredList);

            ////First
            //var result = FirstByManufacturer(cars, "BMW");
            //Console.WriteLine(result);
            //FirstOrDefaultByManufacturer(cars, "BMW");
            //Console.WriteLine(result);

            ////Last
            //result = LastOrDefaultByManufacturer(cars, "BMW");
            //Console.WriteLine(result);

            ////Take
            //var filteredList = TakeCars(cars, 3);
            //Display(filteredList);
            //filteredList = TakeCars(cars, 2..4);
            //Display(filteredList);

            ////TakeWhile
            //var filteredList = TakeCarsWhile(cars, "Aston");
            //Display(filteredList);

            ////Skip
            //filteredList = cars.Skip(5).ToList();
            //Display(filteredList);

            ////Distinct
            //var listOfManufacturers = DistinctManufacturers(cars);
            //Display(listOfManufacturers);

            ////Chunk
            //var chunkList = ChunkCars(cars, 5);
            //foreach (var chunk in chunkList)
            //{
            //    foreach (var car in chunk)
            //    {
            //        Console.WriteLine(car);
            //    }
            //    Console.WriteLine("###### END OF CHUNK #######");
            //}

            ////Linq Group
            //LinqGroup(cars);

            ////Linq Join
            //LinqJoin(cars, manufacturers);

            ////Linq Group i Join
            //LinqGroupJoin(cars, manufacturers);

            ////XML docs
            //SaveAndLoadXml(cars);


        }

        private List<Car[]> ChunkCars(List<Car> cars, int num)
        {
            return cars.Chunk(num).ToList();
        }

        private List<string> DistinctManufacturers(List<Car> cars)
        {
            return cars.Select(x => x.Manufacturer)
                            .Distinct()
                            .OrderBy(x => x)
                            .ToList();
        }

        private List<Car> TakeCarsWhile(List<Car> cars, string prefix)
        {
            return cars.OrderBy(x => x.Manufacturer)
                            .TakeWhile(x => x.Manufacturer.StartsWith(prefix))
                            .ToList();
        }

        private List<Car> TakeCars(List<Car> cars, int howMany)
        {
            return cars.OrderBy(x => x.Manufacturer)
                            .Take(howMany)
                            .ToList();
        }

        private List<Car> TakeCars(List<Car> cars, Range range)
        {
            return cars.OrderBy(x => x.Manufacturer)
                            .Take(range)
                            .ToList();
        }

        private Car LastOrDefaultByManufacturer(List<Car> cars, string name)
        {
            return cars.LastOrDefault(x => x.Manufacturer == name);
        }

        private Car FirstOrDefaultByManufacturer(List<Car> cars, string name)
        {
            return cars.FirstOrDefault(x => x.Manufacturer == name);
        }

        private Car FirstByManufacturer(List<Car> cars, string name)
        {
            return cars.First(x => x.Manufacturer == name);
        }

        private List<Car> WhereStartsWithAndCombined(List<Car> cars, string prefix)
        {
            return cars.Where(x => x.Manufacturer.StartsWith(prefix) && x.Combined > 15).ToList();
        }

        private List<Car> WhereStartsWith(List<Car> cars, string prefix)
        {
            return cars.Where(x => x.Manufacturer.StartsWith(prefix)).ToList();
        }

        private List<Car> OrderByManufacturerAndName(List<Car> cars)
        {
            return cars.OrderBy(x => x.Manufacturer)
                            .ThenBy(x => x.Name)
                            .ToList();
        }

        private List<Car> OrderByManufacturerDescending(List<Car> cars)
        {
            return cars.OrderByDescending(x => x.Manufacturer).ToList();
        }

        private List<Car> OrderByManufacturer(List<Car> cars)
        {
            return cars.OrderBy(x => x.Manufacturer).ToList();
        }

        private List<string> GetSpecificColumns(List<Car> cars)
        {
            var manufacturers = cars.Select(c => c.Manufacturer).ToList();
            return manufacturers;
        }

        private string Anonymous(List<Car> cars)
        {
            var manufacturers = cars.Select(car => new
            {
                Producent = car.Manufacturer,
                Model = car.Name,
                car.City
            });

            var sb = new StringBuilder();
            foreach (var car in manufacturers)
            {
                sb.AppendLine($"Manufacturer: {car.Producent}");
                sb.AppendLine($"Model: {car.Model}");
                sb.AppendLine($"City: {car.City}");
            }
            return sb.ToString();
        }

        private void Display(List<Car> cars)
        {
            Console.WriteLine("#####################");
            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }
            Console.WriteLine("#####################");
            Console.WriteLine();
        }
        private void Display(List<string> list)
        {
            foreach (var text in list)
            {
                Console.WriteLine(text);
            }
        }
        private void SaveAndLoadXml(List<Car> cars)
        {
            var document = new XDocument();
            var records = new XElement("Cars", cars.Select(x =>
                    new XElement("Car",
                        new XAttribute("Name", x.Name),
                        new XAttribute("Combined", x.Combined),
                        new XAttribute("Manufacturer", x.Manufacturer)
                    )
            ));
            document.Add(records);
            document.Save("fuel.xml");

            document = XDocument.Load("fuel.xml");
            var names = document
                .Element("Cars")?
                .Elements("Car")
                .Select(x => x);

            foreach (var item in names)
            {
                Console.WriteLine(item);
            }
        }

        private void LinqGroupJoin(List<Car> cars, List<Manufacturer> manufacturers)
        {
            var groups = manufacturers.GroupJoin(
                cars,
                manufacturer => manufacturer.Name,
                car => car.Manufacturer,
                (m, g) =>
                new
                {
                    Manufacturer = m,
                    Cars = g
                })
                .OrderBy(x => x.Manufacturer.Name);

            foreach (var car in groups)
            {
                Console.WriteLine(car.Manufacturer.Name);
                Console.WriteLine($"         Cars: {car.Cars.Count()}");
                Console.WriteLine($"         Max: {car.Cars.Max(x => x.Combined)}");
                Console.WriteLine($"         Min: {car.Cars.Max(x => x.Combined)}");
                Console.WriteLine($"         Avg: {car.Cars.Average(x => x.Combined)}");
                Console.WriteLine();
            }
        }

        private void LinqJoin(List<Car> cars, List<Manufacturer> manufacturers)
        {
            var carsInCountry = cars.Join(
                manufacturers,
                x => x.Manufacturer,
                x => x.Name,
                (car, manufacturer) =>
                new
                {
                    manufacturer.Country,
                    car.Name,
                    car.Combined
                })
                .OrderBy(x => x.Combined)
                .ThenBy(x => x.Name);

            foreach (var car in carsInCountry)
            {
                Console.WriteLine($"Country: {car.Country}");
                Console.WriteLine($"         Name {car.Name}");
                Console.WriteLine($"         Combined {car.Combined}");
            }
        }

        private void LinqGroup(List<Car> cars)
        {
            var groups = cars
                .GroupBy(x => x.Manufacturer)
                .Select(g => new
                {
                    Name = g.Key,
                    Max = g.Max(c => c.Combined),
                    Average = g.Average(c => c.Combined),
                })
                .OrderBy(x => x.Average);

            foreach (var group in groups)
            {
                Console.WriteLine($"{group.Name}");
                Console.WriteLine($"    Max {group.Max}");
                Console.WriteLine($"    Average {group.Average}");
            }
        }
    }
}
