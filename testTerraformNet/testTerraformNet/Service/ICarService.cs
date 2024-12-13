using testTerraformNet.Entites;

namespace testTerraformNet.Service
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task<Car> GetCarByIdAsync(Guid id);
        Task<Car> CreateCarAsync(Car car);
        Task<Car> UpdateCarAsync(Guid id, Car updatedCar);
        Task<bool> DeleteCarAsync(Guid id);
    }
}
