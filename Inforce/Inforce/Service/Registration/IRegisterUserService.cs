using Inforce.Model;

namespace Inforce.Service.Registration;

public interface IRegisterUserService
{
    public string GetId();
    public Task Save(CancellationToken cancellationToken);
}