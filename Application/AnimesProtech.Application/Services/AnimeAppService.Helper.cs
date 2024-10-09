﻿using System.Linq.Expressions;
using AnimesProtech.Domain.Entities;
using AnimesProtech.Domain.Entities.Base;
using AnimesProtech.Domain.Resourcers;
using AnimesProtech.Infra.CrossCutting.Extensions;

namespace AnimesProtech.Application.Services;

public partial class AnimeAppService
{
    /// <summary>
    /// Valida se uma mercadoria é valida para a inserção no sistema
    /// </summary>
    /// <param name="entity">Entidade a ser validada </param>
    /// <returns>Retorna <c>false</c> se a entity está inválida e <c>true</c> caso válida.</returns>
    private bool Validar(BaseEntity entity)
    {
        var validacao = entity.Validate();

        if (!validacao.IsValid)
        {
            _bus.Notify.NewNotification(validacao.Erros);
            return false;
        }

        return true;
    }
    
    /// <summary>
    /// Salva no banco de dados, caso possua erros, gera uma notificação
    /// </summary>
    /// <returns><c>true</c> caso não haja erros, caso contrário <c>false</c> </returns>
    private bool SaveChanges()
    {
        if (!_animeRepository.SaveChanges())
        {
            _bus.Notify.NewNotification(ErrorMessage.ERRO_SALVAR.Code,
                                        ErrorMessage.ERRO_SALVAR.Message);
            return false;
        }

        return true;
    }
    
    /// <summary>
    /// Monta um filtro para obter animes por diretor, nome ou palavras-chave
    /// </summary>
    /// <param name="diretor">Nome do diretor</param>
    /// <param name="nome">Nome do anime</param>
    /// <param name="palavrasChaves">Palavras-chave separadas por vírgula</param>
    /// <returns>Expressão de filtro montada</returns>
    private static Expression<Func<Anime, bool>> FiltroObterAnime(string? diretor, string? nome, string? palavrasChaves)
    {
        Expression<Func<Anime, bool>> predicate = x => true;
        
        if (!string.IsNullOrWhiteSpace(diretor)) 
            predicate = predicate.And(x => x.Diretor.Equals(diretor));
        
        if (!string.IsNullOrWhiteSpace(nome))
            predicate = predicate.And(x => x.Diretor.Equals(nome));

        if (!string.IsNullOrWhiteSpace(palavrasChaves))
        {
            var listaDePalavrasChaves = palavrasChaves.ToLower()
                                                      .Split(',')
                                                      .Select(x => x.Trim())
                                                      .ToList();
            
            predicate = predicate.And(x => listaDePalavrasChaves.Any(key => x.Resumo.ToLower().Contains(key)));
        }

        return predicate;
    }

}