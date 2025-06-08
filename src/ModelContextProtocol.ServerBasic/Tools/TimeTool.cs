using ModelContextProtocol.Server;
using System.ComponentModel;

[McpServerToolType]
public static class TimeTool
{
    [McpServerTool, Description("Retorna a data e hora atual.")]
    public static string Agora() => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    [McpServerTool, Description("Retorna a data atual.")]
    public static string DataHoje() => DateTime.Today.ToString("yyyy-MM-dd");

    [McpServerTool, Description("Adiciona dias à data atual e retorna o resultado.")]
    public static string AdicionarDias(int dias) => DateTime.Now.AddDays(dias).ToString("yyyy-MM-dd HH:mm:ss");

    [McpServerTool, Description("Calcula a diferença em dias entre duas datas no formato yyyy-MM-dd.")]
    public static int DiferencaDias(string dataInicial, string dataFinal)
    {
        if (!DateTime.TryParse(dataInicial, out var inicio)) throw new ArgumentException("Data inicial inválida.");
        if (!DateTime.TryParse(dataFinal, out var fim)) throw new ArgumentException("Data final inválida.");
        return (fim - inicio).Days;
    }
}
