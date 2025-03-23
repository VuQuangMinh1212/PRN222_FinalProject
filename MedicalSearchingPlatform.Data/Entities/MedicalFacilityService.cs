namespace MedicalSearchingPlatform.Data.Entities
{
    public class MedicalFacilityService
    {
        public string FacilityId { get; set; }
        public MedicalFacility Facility { get; set; }

        public string ServiceId { get; set; }
        public MedicalService Service { get; set; }
    }
}
