using MedicalSearchingPlatform.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Business.Interfaces
{
    public interface IAppoimentMedicalService
    {
        Task CreateAppointmentService(List<AppointmentsServices> data);
    }
}
