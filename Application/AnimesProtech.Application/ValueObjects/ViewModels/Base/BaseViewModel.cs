using System.Text.Json.Serialization;

namespace AnimesProtech.Application.ValueObjects.ViewModels.Base;

public class BaseViewModel
{
    /// <summary>
    /// Identificador único da entidade
    /// </summary>
    [JsonPropertyOrder(-1)]
    public Guid Id { get; set; }
    
    /// <summary>
    /// Data e hora em que a entidade foi criada
    /// </summary>
    public DateTimeOffset DataDeCriacao { get; set; }
    
    /// <summary>
    /// Data e hora da última alteração realizada na entidade
    /// </summary>
    public DateTimeOffset DataDeAlteracao { get; set; }
}