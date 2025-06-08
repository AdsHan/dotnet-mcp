# dotnet-mcp
Demonstração do uso de um servidor MCP com .NET permitindo que modelos de linguagem (LLMs) acessem dados externos, ferramentas e sistemas personalizados.

## 🚀 Como executar:
- Clonar / baixar o repositório em seu workspace.
- Baixar o .NET Core SDK e o Visual Studio / Visual Studio Code mais recentes.
- Você pode integrar esse servidor com assistentes de IA como Claude Desktop e GitHub Copilot no Visual Studio.

### 🔧 Configurando o Claude Desktop

Para usar com o **Claude Desktop**, edite o arquivo de configuração localizado em:

C:\Users\SeuUsuario\AppData\Roaming\Claude\claude_desktop_config.json

Adicione a seguinte configuração:

```json
{
  "mcpServers": {
    "MCPServerBasic": {
      "command": "dotnet",
      "type": "stdio",
      "args": [
        "run",
        "--project",
        "C:\\SeuWorkspace\\dotnet-mcp\\src\\ModelContextProtocol.ServerBasic",
        "--no-build"
      ]
    },
    "MCPServerProduct": {
      "command": "dotnet",
      "type": "stdio",
      "args": [
        "run",
        "--project",
        "C:\\SeuWorkspace\\dotnet-mcp\\src\\ModelContextProtocol.Server",
        "--no-build"
      ]
    }
  }
}
```

# Sobre
Este projeto foi desenvolvido por Anderson Hansen sob [MIT license](LICENSE).