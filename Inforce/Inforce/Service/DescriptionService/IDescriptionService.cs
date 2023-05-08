using DataLayer.Model;
using Inforce.Model;

namespace Inforce.Service.DescriptionService;

public interface IDescriptionService
{
    Task<Description> GetDescription(int shortUrlId,
        CancellationToken cancellationToken = default);

    Task UpdateDescription(int shortUrlId,
        UpdateDescription text, CancellationToken cancellationToken = default);
}