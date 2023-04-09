namespace PetHospital.Business.Exceptions;

[Serializable]
public class NotFoundException : ApplicationException
{
    public NotFoundException(string name)
        : base($"Entity \"{name}\" was not found.") { }

    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.") { }
}