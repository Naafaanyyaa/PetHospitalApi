using PetHospital.Business.Interfaces;

namespace PetHospital.Api.Extensions;

public static class HostExtensions
{
    public static IHost SetupIdentity(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<IRoleInitializer>();
        initializer.InitializeIdentityData();

        return host;
    }
}