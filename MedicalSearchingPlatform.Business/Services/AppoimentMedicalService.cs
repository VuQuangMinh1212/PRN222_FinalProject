using MedicalSearchingPlatform.Business.Interfaces;
using MedicalSearchingPlatform.Data.Entities;
using MedicalSearchingPlatform.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Business.Services
{
    public class AppoimentMedicalService : IAppoimentMedicalService
    {
        private readonly IAppoimentMedicalServiceRepository _appointmentMedicalServiceRepository;
        public AppoimentMedicalService(IAppoimentMedicalServiceRepository appoimentMedicalServiceRepository)
        {
            _appointmentMedicalServiceRepository = appoimentMedicalServiceRepository;
        }
        public async Task CreateAppointmentService(List<AppointmentsServices> data)
        {
            await _appointmentMedicalServiceRepository.CreateAppointmentMedicalService(data);
        }
    }
}
