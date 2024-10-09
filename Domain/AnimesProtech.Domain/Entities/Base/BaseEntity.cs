namespace AnimesProtech.Domain.Entities.Base;
using NotificationDomain = Notification.Notification;

public abstract class BaseEntity
{
    public Guid Id { get; private set; }
    public DateTimeOffset DataDeCriacao { get; private set; }
    public DateTimeOffset DataDeAlteracao { get; private set; }

    public void ModificarDataDeCriacao(DateTimeOffset date)
    {
        DataDeCriacao = date;
    }

    public void ModificarDataDeAlteracao(DateTimeOffset date)
    {
        DataDeAlteracao = date;
    }

    /// <summary>
    /// Método responsável por validar a entidade
    /// </summary>
    /// <returns>Tupla com os erros ocorridos durante a validação</returns>
    public abstract (bool IsValid, IEnumerable<NotificationDomain> Erros) Validate();
}