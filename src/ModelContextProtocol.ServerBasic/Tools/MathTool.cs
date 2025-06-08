using ModelContextProtocol.Server;
using System.ComponentModel;

[McpServerToolType]
public static class MathTool
{
    [McpServerTool, Description("Soma dois números inteiros.")]
    public static int Somar(int a, int b) => a + b;

    [McpServerTool, Description("Subtrai o segundo número do primeiro.")]
    public static int Subtrair(int a, int b) => a - b;

    [McpServerTool, Description("Multiplica dois números inteiros.")]
    public static int Multiplicar(int a, int b) => a * b;

    [McpServerTool, Description("Divide o primeiro número pelo segundo. Retorna zero se o divisor for zero.")]
    public static double Dividir(double a, double b)
    {
        if (b == 0) return 0;
        return a / b;
    }

    [McpServerTool, Description("Calcula o fatorial de um número inteiro não negativo.")]
    public static long Fatorial(int n)
    {
        if (n < 0) throw new ArgumentException("Número deve ser não negativo.");
        long resultado = 1;
        for (int i = 2; i <= n; i++)
            resultado *= i;
        return resultado;
    }
}
