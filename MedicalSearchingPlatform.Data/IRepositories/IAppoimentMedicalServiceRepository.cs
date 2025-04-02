using MedicalSearchingPlatform.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Data.IRepositories
{
    public interface IAppoimentMedicalServiceRepository
    {
        Task CreateAppointmentMedicalService(List<AppointmentsServices> data);
    }
}
