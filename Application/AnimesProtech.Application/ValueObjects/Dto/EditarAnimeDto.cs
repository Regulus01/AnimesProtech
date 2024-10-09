using System.ComponentModel.DataAnnotations;

namespace AnimesProtech.Application.ValueObjects.Dto;

public class EditarAnimeDto
{
    /// <summary>
    /// Nome do anime
    /// </summary>
    [Required]
    public string Nome { get; set; }
    
    /// <summary>
    /// Breve resumo do anime
    /// </summary>
    [Required]
    public string Resumo { get; set; }
    
    /// <summary>
    /// Nome do diretor responsável pelo anime
    /// </summary>
    [Required]
    public string Diretor { get; set; }
}