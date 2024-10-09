using AnimesProtech.Application.ValueObjects.Dto;
using AnimesProtech.Application.ValueObjects.ViewModels;

namespace AnimesProtech.Application.Interfaces;

public interface IAnimeAppService
{
    /// <summary>
    /// Insere um anime no banco de dados
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    CriarAnimeViewModel? CriarAnime(CriarAnimeDto dto);
    EditarAnimeViewModel? EditarAnime(Guid id, EditarAnimeDto dto);
}