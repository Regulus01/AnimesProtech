using System.Linq.Expressions;
using AnimesProtech.Application.Interfaces;
using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interface.Notification;
using AnimesProtech.Domain.Interface.Repository;
using AnimesProtech.Domain.Notification;
using Microsoft.EntityFrameworkCore.Query;
using Moq;

namespace AnimesProtech.Application.Test.AnimeAppServiceTest;

public class AnimeAppServiceTest : IClassFixture<AnimeAppServiceFixture>
{
    private readonly AnimeAppServiceFixture _fixture;
    private readonly IAnimeAppService _appService;

    public AnimeAppServiceTest(AnimeAppServiceFixture fixture)
    {
        _fixture = fixture;
        _appService = fixture.GetAppServiceFixture();
    }

    [Fact(DisplayName = "Criar_Anime_Sucesso")]
    public void Criar_Anime_Successo()
    {
        //Arrange
        var dto = AnimeAppServiceFactory.GerarCriarAnimeDto();

        //Setup
        _fixture.SetupSaveChanges<IAnimeRepository>();

        //Act
        var response = _appService.CriarAnime(dto);

        //Assert
        Assert.Equal(response.Nome, dto.Nome);
        Assert.Equal(response.Resumo, dto.Resumo);
        Assert.Equal(response.Diretor, dto.Diretor);

        _fixture.NeverNotifications();

        _fixture.Mocker.GetMock<IAnimeRepository>()
            .Verify(x => x.Query(
                    It.IsAny<Expression<Func<Anime,bool>>?>(), 
                    It.IsAny<int?>(), 
                    It.IsAny<int?>(), 
                    It.IsAny<Func<IQueryable<Anime>,IIncludableQueryable<Anime,object>>?>()), 
                Times.Once);
        
        _fixture.Mocker.GetMock<IAnimeRepository>()
            .Verify(x => x.Add(It.IsAny<Anime>()), Times.Once);

        _fixture.Mocker.GetMock<IAnimeRepository>()
            .Verify(x => x.SaveChanges(), Times.Once);
    }

    [Fact(DisplayName = "Criar_AnimeExistente_Falha")]
    public void Criar_AnimeExistente_Falha()
    {
        //Arrange
        var dto = AnimeAppServiceFactory.GerarCriarAnimeDto();

        var animeNoFiltro = AnimeAppServiceFactory.GerarAnime();
        var animeForaDoFiltro1 = AnimeAppServiceFactory.GerarAnime();
        var animeForaDoFiltro2 = AnimeAppServiceFactory.GerarAnime();

        var animes = new List<Anime>()
        {
            animeForaDoFiltro1,
            animeNoFiltro,
            animeForaDoFiltro2
        };

        //Setup
        _fixture.SetupQuery(animes);
        _fixture.SetupSaveChanges<IAnimeRepository>();

        //Act
        var response = _appService.CriarAnime(dto);

        //Assert
        Assert.Null(response);

        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(
                    It.Is<string>(y => y.Equals("Erro")),
                    It.Is<string>(y => y.Equals("O nome do anime informado já está cadastrado no sistema"))),
                Times.Once);

        _fixture.Mocker.GetMock<IAnimeRepository>()
            .Verify(x => x.Query(
                    It.IsAny<Expression<Func<Anime,bool>>?>(), 
                    It.IsAny<int?>(), 
                    It.IsAny<int?>(), 
                    It.IsAny<Func<IQueryable<Anime>,IIncludableQueryable<Anime,object>>?>()), 
                Times.Once);
        
        _fixture.Mocker.GetMock<IAnimeRepository>()
            .Verify(x => x.Add(It.IsAny<Anime>()), Times.Never);

        _fixture.Mocker.GetMock<IAnimeRepository>()
            .Verify(x => x.SaveChanges(), Times.Never);
    }
    
    [Fact(DisplayName = "Criar_NomeInvalido_Falha")]
    public void Criar_NomeInvalido_Falha()
    {
        //Arrange
        var dto = AnimeAppServiceFactory.GerarCriarAnimeDto(" ");

        //Setup
        _fixture.SetupSaveChanges<IAnimeRepository>();

        //Act
        var response = _appService.CriarAnime(dto);

        //Assert
        Assert.Null(response);

        _fixture.Mocker.GetMock<INotify>()
            .Verify(x => x.NewNotification(
                    It.Is<IEnumerable<Notification>>(y => y.First().Message.Equals("O nome do anime não pode ser vazio"))),
                Times.Once);

        _fixture.Mocker.GetMock<IAnimeRepository>()
            .Verify(x => x.Query(
                    It.IsAny<Expression<Func<Anime,bool>>?>(), 
                    It.IsAny<int?>(), 
                    It.IsAny<int?>(), 
                    It.IsAny<Func<IQueryable<Anime>,IIncludableQueryable<Anime,object>>?>()), 
                Times.Once);
        
        _fixture.Mocker.GetMock<IAnimeRepository>()
            .Verify(x => x.Add(It.IsAny<Anime>()), Times.Never);

        _fixture.Mocker.GetMock<IAnimeRepository>()
            .Verify(x => x.SaveChanges(), Times.Never);
    }
}