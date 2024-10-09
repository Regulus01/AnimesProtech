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
    
    /// <summary>
    /// Edita o anime informado no campo de id
    /// </summary>
    /// <param name="id">Id do anime</param>
    /// <param name="dto"></param>
    /// <returns>Anime editado</returns>
    EditarAnimeViewModel? EditarAnime(Guid id, EditarAnimeDto dto);

    /// <summary>
    /// Filtra um anime a partir do nome do diretor, nome ou palavras chaves
    /// </summary>
    /// <param name="diretor">Nome do diretor do anime</param>
    /// <param name="nome">Nome do anime</param>
    /// <param name="palavrasChaves">Palavras-chave para filtragem</param>
    /// <param name="skip">Número de registros a serem ignorados</param>
    /// <param name="take">Número máximo de registros a serem retornados</param>
    /// <returns>Animes do filtro</returns>
    IEnumerable<ObterAnimeViewModel>? ObterAnime(string? diretor, string? nome, string? palavrasChaves, 
                                                 int? skip, int? take);
    
    /// <summary>
    /// Remove um anime por id
    /// </summary>
    /// <param name="id">Id do anime a ser removido</param>
    void RemoverAnime(Guid id);
}