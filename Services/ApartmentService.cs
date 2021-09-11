using System.Collections.Generic;
using System.Threading.Tasks;
using NTBrokers.DAL;
using NTBrokers.DAL.Entities;
using NTBrokers.Models.Apartments;

namespace NTBrokers.Services
{
    public class ApartmentService
    {
        private readonly UnitOfWork _unitOfWork;

        public ApartmentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ApartmentModel>> GetAllAsync()
        {
            return await _unitOfWork.ApartmentRepository.GetAllApartmentsInfoAsync();
        }

        public async Task<ApartmentCreateModel> CreateAsync()
        {
            return new()
            {
                Apartment = new Apartment(),
                Companies = await _unitOfWork.CompanyRepository.GetAllAsync()
            };
        }

        public async Task SubmitAsync(ApartmentCreateModel model)
        {
            await _unitOfWork.ApartmentRepository.InsertAsync(model.Apartment);
            await _unitOfWork.SaveAsync();
        }
    }
}
