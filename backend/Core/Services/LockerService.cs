using Core.DTOs;
using Core.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class LockerDTOService
    {
        private readonly ILockerRepository _lockerRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IPriceRepository _priceRepository;

        public LockerDTOService(ILockerRepository lockerRepository,
                                  ILocationRepository locationRepository,
                                  IPriceRepository priceRepository)
        {
            _lockerRepository = lockerRepository;
            _locationRepository = locationRepository;
            _priceRepository = priceRepository;
        }

        public async Task<LockerDTO> GetLockerDTOAsync(int lockerId)
        {
            var locker = await _lockerRepository.GetByIdAsync(lockerId);
            var location = await _locationRepository.GetByIdAsync(locker.IdLocation);
            var price = await _priceRepository.GetByIdAsync(locker.IdPrice);

            if (locker is null || location is null || price is null)
            {
                return null;
            }

            var lockerDTO = new LockerDTO
            {
                Id = locker.Id,
                SerialNumber = locker.SerialNumber,
                Location = location.Address,
                IdLocation = location.Id,
                IdPrice = price.Id,
                Price = price.Value,
                Rented = locker.Rented
            };

            return lockerDTO;
        }

        public async Task<IEnumerable<LockerDTO>> GetAllLockersAsync()
        {
            var lockers = await _lockerRepository.GetAllAsync();

            if (lockers is null || !lockers.Any())
                return Enumerable.Empty<LockerDTO>();

            var lockersDTO = new List<LockerDTO>();

            foreach (var locker in lockers)
            {
                var location = await _locationRepository.GetByIdAsync(locker.IdLocation);
                var price = await _priceRepository.GetByIdAsync(locker.IdPrice);


                if (location is null || price is null || locker is null)
                    continue;

                var lockerDTO = new LockerDTO
                {
                    Id = locker.Id,
                    SerialNumber = locker.SerialNumber,
                    Location = location.Address,
                    IdLocation = location.Id,
                    Price = price.Value,
                    IdPrice = price.Id,
                    Rented = locker.Rented
                };

                lockersDTO.Add(lockerDTO);
            }

            return lockersDTO;
        }
    }
}
