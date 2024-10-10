using AnimesProtech.Application.ValueObjects.Dto;
using AnimesProtech.Domain.Entities;

namespace AnimesProtech.Application.Test.AnimeAppServiceTest;

public class AnimeAppServiceFactory
{
    public static CriarAnimeDto GerarCriarAnimeDto(string nome = "jojo", string resumo = "Uma aventura bizarra", 
        string diretor = "Hirohiko Araki")
    {
        return new CriarAnimeDto
        {
            Nome = nome,
            Resumo = resumo,
            Diretor = diretor
        };
    }
    
    public static Anime GerarAnime(string nome = "jojo", string resumo = "Uma aventura bizarra", 
        string diretor = "Hirohiko Araki")
    {
        return new Anime(nome, resumo, diretor);
    }
}