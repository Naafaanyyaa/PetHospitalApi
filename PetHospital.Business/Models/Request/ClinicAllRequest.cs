namespace PetHospital.Business.Models.Request
{
    public class ClinicAllRequest
    {
        public string? SearchString { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
