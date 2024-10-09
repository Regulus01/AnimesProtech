using AnimesProtech.Domain.Entities.Base;
using NotificationDomain = AnimesProtech.Domain.Notification.Notification;

namespace AnimesProtech.Domain.Entities;

public class Anime : BaseEntity
{
    public string Nome { get; private set; }
    public string Resumo { get; private set; }
    public string Diretor { get; private set; }
    public bool Deletado { get; private set; }

    public Anime(string nome, string resumo, string diretor)
    {
        Nome = nome;
        Resumo = resumo;
        Diretor = diretor;
    }

    public void Editar(string nome, string resumo, string diretor)
    {
        Nome = nome;
        Resumo = resumo;
        Diretor = diretor;
    }

    public void Deletar()
    {
        Deletado = true;
    }
    
    public override (bool IsValid, IEnumerable<NotificationDomain> Erros) Validate()
    {
        var erros = new List<NotificationDomain>();

        ValidarNome(erros);
        ValidarResumo(erros);
        ValidarDiretor(erros);

        return (erros.Count == 0, erros);
    }

    private void ValidarDiretor(List<NotificationDomain> erros)
    {
        if (string.IsNullOrWhiteSpace(Diretor))
            erros.Add(new NotificationDomain("Erro", "O nome do anime não pode ser vazio"));

        if(Diretor?.Length > 50)
            erros.Add(new NotificationDomain("Erro", "O nome diretor do anime, não pode ultrapassar 50 caracteres"));
    }

    private void ValidarResumo(List<NotificationDomain> erros)
    {
        if (string.IsNullOrWhiteSpace(Resumo))
            erros.Add(new NotificationDomain("Erro", "A descrição do anime não pode ser vazio"));

        if(Resumo?.Length > 80)
            erros.Add(new NotificationDomain("Erro", "A descrição do anime não pode ultrapassar 50 caracteres"));
    }

    private void ValidarNome(List<NotificationDomain> erros)
    {
        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add(new NotificationDomain("Erro", "O nome do anime não pode ser vazio"));
        
        if(Nome?.Length > 50)
            erros.Add(new NotificationDomain("Erro", "O nome do anime não pode ultrapassar 50 caracteres"));
    }
}