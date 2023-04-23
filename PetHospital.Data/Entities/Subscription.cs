using PetHospital.Data.Entities.Abstract;
using PetHospital.Data.Entities.Identity;
using PetHospital.Data.Enums;

namespace PetHospital.Data.Entities;

public class Subscription : BaseEntity
{
    public SubscriptionType SubscriptionType { get; set; }
    public virtual User User { get; set; } = new User();
}