using DataLayer.Model;
using DataLayer.Repository.Interface;
using Inforce.Model;

namespace Inforce.Service.DescriptionService;

public class DescriptionService : IDescriptionService
{
    private readonly IDescriptionRepository _descriptionRepository;

    public DescriptionService(IDescriptionRepository descriptionService)
    {
        _descriptionRepository = descriptionService;
    }

    public async Task<Description> GetDescription(int shortUrlId, CancellationToken cancellationToken = default)
    {
        var a = (await _descriptionRepository.GetAll(cancellationToken)).FirstOrDefault();
        return a;
    }

    public async Task UpdateDescription(int shortUrlId, UpdateDescription text,
        CancellationToken cancellationToken = default)
    {
        await _descriptionRepository.Update(text.Description);
        await _descriptionRepository.Save(cancellationToken);
    }
}