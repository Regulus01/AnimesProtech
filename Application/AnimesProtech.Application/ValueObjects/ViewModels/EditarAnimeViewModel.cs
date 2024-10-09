using AnimesProtech.Application.ValueObjects.ViewModels.Base;

namespace AnimesProtech.Application.ValueObjects.ViewModels;

public class EditarAnimeViewModel : BaseViewModel
{
    public string Nome { get; set; }
    public string Resumo { get; set; }
    public string Diretor { get; set; }
}