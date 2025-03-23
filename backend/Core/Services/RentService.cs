using Core.DTOs;
using Core.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class RentDTOService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILockerRepository _lockerRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IRentRepository _rentRepository;

        public RentDTOService(ICustomerRepository customerRepository,
                              ILockerRepository lockerRepository,
                              ILocationRepository locationRepository,
                              IRentRepository rentRepository)
        {
            _customerRepository = customerRepository;
            _lockerRepository = lockerRepository;
            _locationRepository = locationRepository;
            _rentRepository = rentRepository;
        }

        public async Task<RentDTO> GetRentDTOAsync(int rentId)
        {
            var rent = await _rentRepository.GetByIdAsync(rentId);
            var customer = await _customerRepository.GetByIdAsync(rent.IdCustomer);
            var locker = await _lockerRepository.GetByIdAsync(rent.IdLocker);
            var location = await _locationRepository.GetByIdAsync(locker.IdLocation);
            

            if (customer is null || locker is null || rent is null || location is null)
                return null;

            var rentDTO = new RentDTO
            {
                Id = rent.Id,
                IdCustomer = rent.IdCustomer,
                Customer = customer.Name,
                IdLocker = rent.IdLocker,
                Locker = $"{locker.SerialNumber} + {location.Address}",
                RentalDate = rent.RentalDate,
                ReturnDate = rent.ReturnDate,
                UserName = rent.UserName
            };

            return rentDTO;
        }

        public async Task<IEnumerable<RentDTO>> GetAllRentsAsync()
        {
            var rents = await _rentRepository.GetAllAsync();

            if (rents is null || !rents.Any())
                return Enumerable.Empty<RentDTO>();

            var rentsDTO = new List<RentDTO>();

            foreach (var rent in rents)
            {
                var locker = await _lockerRepository.GetByIdAsync(rent.IdLocker);
                var location = await _locationRepository.GetByIdAsync(locker.IdLocation);
                var customer = await _customerRepository.GetByIdAsync(rent.IdCustomer);


                if (locker is null || customer is null || location is null)
                    continue;

                var rentDTO = new RentDTO
                {
                    Id = rent.Id,
                    IdCustomer = rent.IdCustomer,
                    Customer = customer.Name,
                    IdLocker = rent.IdLocker,
                    Locker = $"{locker.SerialNumber} + {location.Address}",
                    RentalDate = rent.RentalDate,
                    ReturnDate = rent.ReturnDate,
                    UserName = rent.UserName
                };

                rentsDTO.Add(rentDTO);
            }

            return rentsDTO;
        }
    }
}
