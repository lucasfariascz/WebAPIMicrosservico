# Microsserviços com gRPC

Dois microsserviços em C#, o primeiro microsserviço (WebAPIMicrosservico) recebe um objeto no formato JSON via HTTP POST. 
Esse objeto é enviado pelo cliente como parte de uma solicitação HTTP. O microsserviço é responsável
por receber essa solicitação, extrair o objeto JSON da requisição e armazená-lo no
banco de dados não relacional Azure Cosmos DB.
O segundo (WebAPIMicrosservico) microsserviço é responsável por consumir a fila do Azure Service Bus e 
processar os objetos que foram enviados pelo primeiro microsserviço.

O contrato entre os microsserviços foi desenvolvido pelo gRPC. O WebAPIMicrosservico sendo o **GrpcClient** e 
WebAPIMicrosservico sendo **GrpcService**


## Introdução

O sistema consiste em dois microsserviços desenvolvidos em C# utilizando a abordagem de comunicação 
assíncrona com base em mensageria e o protocolo gRPC para definir o contrato de comunicação entre eles.

### Ferramentas
* RestFull para receber objetos enviado via JSON;
* Azure Cosmos DB para armazenar os dados dos objetos enviados;
* Azure Service Bus serve para receber o objetos e enviar mensagem para fila;
* gRPC para contrato entre os microsservicos no envio dos objetos;

## Arquitetura e Estrutura do Projeto

Estrutura do projeto
```
Features
│   └───<feature name>
│       └───Domain
|       |    └─── Models
|       |    |     <feature name>Model.cs
|       |    └─── Repository
|       |    |     <I+feature name>Repository.cs
|       |    └─── UseCases
|       |          <I+feature name>UseCase.cs
|       |          <feature name>UseCase.cs
|       |
│       └───Infra
|       |    └─── Repositories
|       |          <feature name>Repositories.cs
|       |
│       └───Controllers
|       |    <feature name>Controller.cs
|       |    └─── dto
Services
|   └───<service name>
|           <service name>Service.ts
```

- _Features_: Onde os arquivos principais estão hospedados. Dentro da pasta features dividimos as 
"funcionalidades" em diferentes pastas, como: login, user e etc.
- _Sevices_: semelhante a features, mas não faz parte de um caso de uso do ponto de vista do negócio. Por exemplo: Enviar e-mail

## Decisões de Design

Documente as principais decisões de design tomadas durante o desenvolvimento do código. 
Explique o raciocínio por trás dessas decisões e como elas impactam o 
desempenho, a escalabilidade e a manutenção do código.

## Funcionalidades Implementadas

* Recebimento de Dados via HTTP POST;
* Armazenamento no Azure Cosmos DB;
* Envio de Mensagens para a Fila do Azure Service Bus;
* Consumo da Fila pelo Segundo Microsserviço;
* Contrato de entre os microsserviços com gRPC;
 

Nesse techo do código é passado um objeto para gRPC cliente que envia para servidor, em seguida o gRPC 
servidor confirma se o objeto foi enviado ou não. Caso o servidor do gRPC confirmar retorna a mensagem "Objeto enviado com sucesso." é 
inserido o objeto userModel à base de dados NoSQL e envia uma mensagem do objeto userModel para a fila. Caso de não comunicação do gRPC 
cliente para gRPC servidor é retornando uma mensagem "Objeto não enviado.".
```csharp
    public async Task<string> SubmitUser(UserModel userModel)
    {
        var userResponse = await this.contractWebAPIClient.MessageGrpc(userModel);
        if (userResponse.Message == "Objeto enviado com sucesso.")
        {
            // Adiciona o objeto userModel à base de dados NoSQL
            await noSQLDataBase.Add(container, userModel, userModel.Id.ToString());
            // Envia uma mensagem do objeto userModel para a fila
            await this.queueService.SendMessageQueue(userModel);
            return userResponse.Message;
         }
         return messageNotSend;
    }
``` 

Nesse techo do código é processado o objeto que foi enviado para a fila, 
no qual o userViewModel recebe esse objeto e armazena em outra tabela na base de dados NoSQL apenas com o Id e Message que foi enviado no objeto.
```csharp
    private async Task ProcessMessagesAsync(Message message, CancellationToken token)
    {
         // Processing Message - Queue
         UserViewModel userViewModel = JsonSerializer.Deserialize<UserViewModel>(message.Body);
         await this.noSQLDataBase.Add(container, userViewModel, userViewModel.Id.ToString());

         await queueClient.CompleteAsync(message.SystemProperties.LockToken);
    }

```


## Boas Práticas e Padrões de Codificação

* Nomenclatura: Utilize nomes descritivos para os arquivos do código, classes, métodos, variáveis
* Estrutura do código: Utilizei como base o DDD que é amplamente utilizada em projetos. Baseado em **Design a DDD-oriented microservice**
(https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice)

## Testes Unitários

* Realizei testes unitário do SubmitUserUseCase e UserRepositoriesTest;
* Cobertura dos testes foram de sucesso e falha;

## Tratamento de Erros e Exceções

* Para tratamento de erros e exceções foi feito um ExceptionFilter. Ele ficou declarado de forma global;

## Considerações de Segurança

* Para segurança foi utilizado funcionalidade Autorização por meio do AuthorizationFilter.
O AuthorizationFilter é um filtro que pode ser aplicado a nível de classe ou método;

* A API SubmitUserController precisa de Autorização por meio do AuthorizationFilter

Obs: Foi apenas validação mockada do token. Esse é o token que tem que passar no Header: 986ghgrgtru989ASdsaerew13434545435

## Instruções de executar o projeto

1. No menu "Arquivo", selecione "Abrir" > "Projeto/Solução";
2. Selecione o arquivo .csproj para importá-lo no Visual Studio;
3. No Gerencioanor de Soluções, clique com o botão direito do mouse no projeto que deseja
definir o projeto de inicialização (Definir como Projeto de Inicialização);
4. Pressione F5 ou vai em "Depurar" > "Iniciar Depuração";

Obs: Todo o projeto foi desenvolvido no Visual Studio.

O projeto possui apenas a API SubmitUserController do metodo POST.

Rota: /api/SubmitUser

Exemplo de JSON que pode ser enviado.
Obs: O id tem que ser colocado manualmente, não foi utilizado Guid.
```json
{
  "id": "100",
  "name": "Luks",
  "email": "luks@test.com",
  "message": "luks"
}
```