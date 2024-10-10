using System.Linq.Expressions;
using AnimesProtech.Application.Mapper;
using AnimesProtech.Application.Services;
using AnimesProtech.Application.Test.Base;
using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interface.Bus;
using AnimesProtech.Domain.Interface.Notification;
using AnimesProtech.Domain.Interface.Repository;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Moq.AutoMock;

namespace AnimesProtech.Application.Test.AnimeAppServiceTest;

public class AnimeAppServiceFixture : BaseFixture
{
    public AnimeAppService GetAppServiceFixture()
    {
        Mocker = new AutoMocker();
        
        Mocker.Use(MappingConfiguration.RegisterMappings().CreateMapper());
        
        var bus = Mocker.GetMock<IBus>();
        
        bus.Setup(b => b.Notify)
            .Returns(Mocker.GetMock<INotify>().Object);
        
        return Mocker.CreateInstance<AnimeAppService>();
    }
    
    public void SetupQuery(IEnumerable<Anime> entities)
    {
        Mocker.GetMock<IAnimeRepository>()
            .Setup(x => x.Query(
                It.IsAny<Expression<Func<Anime, bool>>?>(),
                It.IsAny<int?>(), It.IsAny<int?>(),
                It.IsAny<Func<IQueryable<Anime>, IIncludableQueryable<Anime, object>>?>()))
            .Returns<Expression<Func<Anime, bool>>?, int?, int?,
                Func<IQueryable<Anime>, IIncludableQueryable<Anime, object>>?>(
                (filter, _, _, _) => entities.Where(filter.Compile()));
    }
}