using Microsoft.EntityFrameworkCore;
using testTerraformNet.Entites;

namespace testTerraformNet.Service.impl
{   public class CarService : ICarService
    {
        private readonly MyDbContext _dbContext;

        public CarService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Get all cars from the database
        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _dbContext.Cars.ToListAsync();
        }

        // Get a car by its ID
        public async Task<Car> GetCarByIdAsync(Guid id)
        {
            return await _dbContext.Cars.FirstOrDefaultAsync(c => c.Id == id);
        }

        // Create a new car
        public async Task<Car> CreateCarAsync(Car car)
        {
            car.Id = Guid.NewGuid();  // Generate a new GUID for the car
            await _dbContext.Cars.AddAsync(car); // Add the car to the DbSet
            await _dbContext.SaveChangesAsync(); // Save changes to the database
            return car;
        }

        // Update an existing car
        public async Task<Car> UpdateCarAsync(Guid id, Car updatedCar)
        {
            var car = await _dbContext.Cars.FirstOrDefaultAsync(c => c.Id == id);
            if (car != null)
            {
                car.Brand = updatedCar.Brand;
                car.Model = updatedCar.Model;
                car.Year = updatedCar.Year;
                car.Price = updatedCar.Price;
                car.IsAvailable = updatedCar.IsAvailable;

                await _dbContext.SaveChangesAsync(); // Save changes to the database
            }
            return car;
        }

        // Delete a car
        public async Task<bool> DeleteCarAsync(Guid id)
        {
            var car = await _dbContext.Cars.FirstOrDefaultAsync(c => c.Id == id);
            if (car != null)
            {
                _dbContext.Cars.Remove(car); // Remove the car from the DbSet
                await _dbContext.SaveChangesAsync(); // Save changes to the database
                return true;
            }
            return false;
        }
    }

}
