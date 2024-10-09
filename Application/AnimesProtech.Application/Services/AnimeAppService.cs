using AnimesProtech.Application.Interfaces;
using AnimesProtech.Application.ValueObjects.Dto;
using AnimesProtech.Application.ValueObjects.ViewModels;
using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Interface.Bus;
using AnimesProtech.Domain.Interface.Repository;
using AutoMapper;

namespace AnimesProtech.Application.Services;

public partial class AnimeAppService : IAnimeAppService
{
    private readonly IAnimeRepository _animeRepository;
    private readonly IMapper _mapper;
    private readonly IBus _bus;

    public AnimeAppService(IAnimeRepository animeRepository, IMapper mapper, IBus bus)
    {
        _animeRepository = animeRepository;
        _mapper = mapper;
        _bus = bus;
    }

    /// <inheritdoc />
    public CriarAnimeViewModel? CriarAnime(CriarAnimeDto dto)
    {
        var animeExistente = _animeRepository.Query<Anime>(x => x.Nome.Equals(dto.Nome)).FirstOrDefault();

        if (animeExistente != null)
        {
            _bus.Notify.NewNotification("Erro", "O nome do anime informado já está cadastrado no sistema");
            return null;
        }
        
        var anime = new Anime(dto.Nome, dto.Resumo, dto.Diretor);
    
        if (!Validar(anime))
            return null;
        
        _animeRepository.Add(anime);

        if (!SaveChanges())
            return null;

        return _mapper.Map<CriarAnimeViewModel>(anime);
    }
    
    /// <inheritdoc />
    public EditarAnimeViewModel? EditarAnime(Guid id, EditarAnimeDto dto)
    {
        var anime = _animeRepository.Query<Anime>(x => x.Id.Equals(id)).FirstOrDefault();

        if (anime == null)
        {
            _bus.Notify.NewNotification("Erro", "Anime não encontrado");
            return null;
        }
        
        anime.EditarAnime(dto.Nome, dto.Resumo, dto.Diretor);
    
        if (!Validar(anime))
            return null;
        
        _animeRepository.Update(anime);

        if (!SaveChanges())
            return null;

        return _mapper.Map<EditarAnimeViewModel>(anime);
    }

    public IEnumerable<ObterAnimeViewModel>? ObterAnime(string? diretor, string? nome, string? palavrasChaves,
                                                        int? skip, int? take)
    {
        var predicate = FiltroObterAnime(diretor, nome, palavrasChaves);

        var animes = _animeRepository.Query(predicate, skip, take).ToList();

        if (animes.Count == 0)
            return new List<ObterAnimeViewModel>();
        
        return _mapper.Map<IEnumerable<ObterAnimeViewModel>>(animes);
    }
}