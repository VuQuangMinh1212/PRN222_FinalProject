﻿namespace MedicalSearchingPlatform.Data.Entities
{
    public class MedicalService
    {
        public string ServiceId { get; set; } = Guid.NewGuid().ToString();
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public ICollection<MedicalFacilityService> FacilityServices { get; set; } = new List<MedicalFacilityService>();
    }
}
