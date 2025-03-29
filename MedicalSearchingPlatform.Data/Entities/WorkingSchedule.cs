using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSearchingPlatform.Data.Entities
{
    public class WorkingSchedule
    {
        [Key]
        public string ScheduleId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public DateTime WorkDate { get; set; } = DateTime.Now;

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public bool IsAvailable { get; set; } = true;

        [Required, ForeignKey("DoctorId"), Display(Name = "Doctor")]
        public string DoctorId { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public virtual List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
