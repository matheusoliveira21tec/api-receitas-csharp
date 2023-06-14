namespace MeuLivroDeReceitas.Comunicacao.Response;

public class ResponseErrorJson
{
    public List<string> Messages { get; set; }
    public ResponseErrorJson(List<string> messages)
    {
        Messages = messages;
    }
    public ResponseErrorJson(string message)
    {
        Messages = new List<string>()
        {
            message
        };
    }
}
