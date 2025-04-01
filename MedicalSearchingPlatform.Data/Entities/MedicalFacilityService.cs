namespace MedicalSearchingPlatform.Data.Entities
{
    public class MedicalFacilityService
    {
        public string FacilityId { get; set; }
        public virtual MedicalFacility Facility { get; set; }

        public string ServiceId { get; set; }
        public virtual MedicalService Service { get; set; }
    }
}
