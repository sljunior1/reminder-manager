# REMINDER MANAGER
**Desenvolvido por: Silas Lima da Silva Júnior**


***1- TECNOLOGIAS UTILIZADAS***

*a. .NET Maui - desenvolvimento do App*

*b. .NET 8 - desenvolvimento da WebApi*

*c. SQL Server - armazenamento de dados*

*d. Docker - criação de conteiner*

***2- FUNCIONALIDADES***

*a. Cadastrar usuário*

*b. Informações sobre a versão do aplicativo e versão do Android do dispositivo*

*c. Autenticação com usuário e senha*

*d. Autenticação biometria e reconhecimento facial*

*e. Listas de lembretes com paginação*

*f. Cadastrar lembrete*

*g. Editar lembrete*

*h. Excluir lembrete*

*i. Calendário iterativo*

*j. Mudança de tema*

*h. Dados do usuário*

*i. Troca de senha*

***3- CONFIGURAÇÕES INICIAIS***

a. Para executar a imagem do SQL Server no Docker, é necessário abrir o Power Shell e executar o seguinte comando: **docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=!WeAZ#Hq2i" -p 1433:1433 --name BDMovisis --hostname BDMovisis -d ` mcr.microsoft.com/mssql/server:2022-latest**

b. Para que o App seja executado no emulador do Visual Studio ou até mesmo no modo desenvolvedor do dispositivo (com cabo USB conectado) é necessário configurar o IP de onde será executado a WebApi (maquina local ou servidor) e a porta na qual será utilizada. Para isso, altere o valor das constantes IP e Porta do arquivo GlobalSettings.cs que encontra-se na raiz do projeto.

c. Para que o App utilize biometria e reconhecimento facial, o emulador e/ou dispositivo deve estar habilitado e com cadastro biométrico.


***4- FLUXO DAS TELAS***

![fluxo drawio](https://github.com/user-attachments/assets/fe19923b-8049-4b21-a113-942a3a2da9d5)

