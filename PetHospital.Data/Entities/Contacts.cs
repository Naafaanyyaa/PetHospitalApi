using PetHospital.Data.Entities.Abstract;
using PetHospital.Data.Entities.Identity;

namespace PetHospital.Data.Entities;

public class Contacts : BaseEntity
{
    public string? Telegram { get; set; }
    public string? Instagram { get; set; }
    public string? Facebook { get; set; }
    public string? Viber { get; set; }
    public string? Phone { get; set; }
    public string UserId { get; set; } = string.Empty;
    public virtual User User { get; set; } = new User();
}