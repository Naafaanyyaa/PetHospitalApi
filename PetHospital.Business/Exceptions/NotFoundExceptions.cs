namespace PetHospital.Business.Exceptions;

[Serializable]
public class NotFoundException : ApplicationException
{
    public NotFoundException(string exception)
        : base(exception) { }

    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.") { }
}