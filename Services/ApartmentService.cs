using System.Collections.Generic;
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

        public List<ApartmentModel> GetAll()
        {
            return _unitOfWork.ApartmentRepository.GetAllApartmentsInfo();
        }

        public ApartmentCreateModel Create()
        {
            return new()
            {
                Apartment = new Apartment(),
                Companies = _unitOfWork.CompanyRepository.GetAll()
            };
        }

        public void Submit(ApartmentCreateModel model)
        {
            _unitOfWork.ApartmentRepository.Insert(model.Apartment);
            _unitOfWork.ApartmentRepository.Save();
        }
    }
}
