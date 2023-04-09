namespace PetHospital.Data.Entities.Abstract;

public class BaseEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public DateTime CreatedDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }

}