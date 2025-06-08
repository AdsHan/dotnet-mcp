using ModelContextProtocol.Server;
using System.ComponentModel;

[McpServerToolType]
public static class EchoTool
{
    [McpServerTool, Description("Retorna a mensagem enviada pelo cliente.")]
    public static string Eco(string mensagem) =>
        $"Olá do C#: {mensagem}";

    [McpServerTool, Description("Retorna a mensagem enviada pelo cliente invertida.")]
    public static string EcoInvertido(string mensagem) =>
        new string(mensagem.Reverse().ToArray());

    [McpServerTool, Description("Retorna a mensagem e o tamanho dela.")]
    public static string EcoComTamanho(string mensagem) =>
        $"Mensagem: {mensagem} (Tamanho: {mensagem.Length})";

    [McpServerTool, Description("Retorna a mensagem em letras maiúsculas.")]
    public static string EcoMaiusculo(string mensagem) =>
        mensagem.ToUpperInvariant();

    [McpServerTool, Description("Repete a mensagem um número específico de vezes.")]
    public static string RepetirEco(string mensagem, int vezes)
    {
        if (vezes <= 0) return string.Empty;
        return string.Concat(Enumerable.Repeat(mensagem, vezes));
    }
}
