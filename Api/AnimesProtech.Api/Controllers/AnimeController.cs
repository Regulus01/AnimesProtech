﻿using System.ComponentModel.DataAnnotations;
using System.Net;
using AnimesProtech.Application.Interfaces;
using AnimesProtech.Application.ValueObjects.Dto;
using AnimesProtech.Application.ValueObjects.ViewModels;
using AnimesProtech.Domain.Interface.Notification;
using AnimesProtech.Infra.CrossCutting.Controller;
using Microsoft.AspNetCore.Mvc;

namespace AnimesProtech.Api.Controllers;

[Route("api/v1/Anime")]
public class AnimeController : BaseController
{
    private readonly IAnimeAppService _animeAppService;

    public AnimeController(INotify notify, IAnimeAppService animeAppService) : base(notify)
    {
        _animeAppService = animeAppService;
    }

    /// <summary>
    /// Insere um anime no banco de dados
    /// </summary>
    /// <param name="dto"></param>
    /// <returns>Anime criado</returns>
    [ProducesResponseType(typeof(CriarAnimeViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public IActionResult CriarAnime(CriarAnimeDto dto)
    {
        var result = _animeAppService.CriarAnime(dto);

        return Response(HttpStatusCode.Created, result);
    }

    /// <summary>
    /// Edita um anime no banco de dados
    /// </summary>
    /// <param name="id">Id do anime para edição</param>
    /// <param name="dto"></param>
    /// <returns>Anime editado</returns>
    [ProducesResponseType(typeof(EditarAnimeViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut("{id:guid}")]
    public IActionResult EditarAnime([Required] [FromRoute] Guid id, EditarAnimeDto dto)
    {
        var result = _animeAppService.EditarAnime(id, dto);

        return Response(HttpStatusCode.Created, result);
    }

    /// <summary>
    /// Obtem um anime, com a possibilidade de filtros
    /// </summary>
    /// <param name="diretor">Nome do diretor</param>
    /// <param name="nome">Nome do anime</param>
    /// <param name="palavrasChaves">Palavras-chave separadados por vírgula</param>
    /// <param name="skip">Número de registros a serem ignorados</param>
    /// <param name="take">Número máximo de registros a serem retornados</param>
    /// <returns>Animes do filtro</returns>
    [ProducesResponseType(typeof(ObterAnimeViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public IActionResult GetAnime([FromQuery] string? diretor = null, [FromQuery] string? nome = null,
                                  [FromQuery] string? palavrasChaves = null, int? skip = null, int? take = null)
    {
        var result = _animeAppService.ObterAnime(diretor, nome, palavrasChaves, skip, take);
        
        return Response(HttpStatusCode.OK, result);
    }

    /// <summary>
    /// Remove um anime a partir do id
    /// </summary>
    /// <param name="id">Id do anime a ser removido</param>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("{id:guid}")]
    public IActionResult RemoverAnime([FromRoute] Guid id)
    {
        _animeAppService.RemoverAnime(id);
        
        return Response(HttpStatusCode.NoContent);
    }
}