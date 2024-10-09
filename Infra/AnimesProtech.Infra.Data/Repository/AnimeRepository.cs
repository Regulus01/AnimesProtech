using AnimesProtech.Domain.Interface.Repository;
using AnimesProtech.Infra.Data.Context;
using AnimesProtech.Infra.Data.Repository.Base;
using Microsoft.Extensions.Logging;

namespace AnimesProtech.Infra.Data.Repository;

public class AnimeRepository : BaseRepository<AnimesDbContext, AnimeRepository>, IAnimeRepository
{
    public AnimeRepository(AnimesDbContext context, ILogger<AnimeRepository> logger) : base(context, logger)
    {
    }
}