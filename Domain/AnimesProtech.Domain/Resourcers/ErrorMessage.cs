namespace AnimesProtech.Domain.Resourcers;

public partial class ErrorMessage
{
    public static (string Code, string Message) ERRO_SALVAR =
        ("00x1", "Um erro ocorreu ao salvar os dados.");

    public static (string Code, string Message) ERRO_ATUALIZAR =
        ("00x2", "Um erro ocorreu ao atualizar os dados.");

    public static (string Code, string Message) ERRO_DELETAR =
        ("00x3", "Um erro ocorreu ao deletar os dados.");

    public static (string Code, string Message) DADO_NAO_ENCONTRADO =
        ("00x4", "Registro não encontrado.");
}