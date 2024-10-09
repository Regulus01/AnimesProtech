using AnimesProtech.Application.ValueObjects.ViewModels.Base;

namespace AnimesProtech.Application.ValueObjects.ViewModels;

public class CriarAnimeViewModel : BaseViewModel
{
    public string Nome { get; private set; }
    public string Resumo { get; private set; }
    public string Diretor { get; private set; }
}