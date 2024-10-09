using AnimesProtech.Domain.Entities.Base;
using AnimesProtech.Domain.Resourcers;

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

}