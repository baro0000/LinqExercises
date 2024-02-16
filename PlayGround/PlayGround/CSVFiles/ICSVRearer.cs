
namespace PlayGround.CSVFiles
{
    public interface ICSVRearer
    {
        List<Car> ProcessCars(string filePath);
        List<Manufacturer> ProcessManufacturers(string filePath);
    }
}