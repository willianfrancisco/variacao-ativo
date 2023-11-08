# variacao-ativo
API para calcular variação do preço de ativos nos ultimos 30 dias.

## Sobre o desafio
Este desafio consiste em consultar a variação do preço de um ativo a sua escolha nos últimos 30 pregões. Você deverá apresentar o percentual de variação de preço de um dia para o outro e o percentual desde o primeiro pregão apresentado.
- Consultar o preço do ativo na API do Yahoo Finance (este é um exemplo da consulta do ativo PETR4 https://query2.finance.yahoo.com/v8/finance/chart/PETR4.SA)
- Armazenar as informações em uma base de dados a sua escolha.
- Implementar uma API que consulte as informações na sua base de dados, retorne o valor do ativo nos últimos 30 pregões e apresente a variação do preço no período. Você deverá considerar o valor de abertura (chart.result.indicators.quote.open).

## Solucao
Para resolver o desafio eu optei por utilizat o dotnet core 6, aplicando uma arquitetura camadas juntos com os padrões arquiteturais do DDD, Arquitetura Limpar e SOLID, e utilizei o MSSQL Server como banco de dados da applicação.

### Escolha da arquitetura 
- Eu optei pela escolha de arquitetura em camadas junto ao padrão arquitetural de arquitetura limpa e SOLID  pois isso geraria uma organização simples e clara no quesito de responsabilidades facilitaria o entendimento e organização do codigo.
- Poderia utilizar uma arquitetura com 4 camadas, Domain,Application,Infra.Data e UI, porém isso geraria um forte acoplamento entre as camadas de Infra.Data e UI e seria necessario instalar pacotes que não seriam utilizados pela camada de UI, por esse motivo criei a Infra.Data.Ioc que servira como um intermediador entre as duas camadas.

### Escolha do banco de dados
- Optei pelo uso do MS-SQL SERVER por ser um banco de dados relacional que no contexto do desafio por ser uma loja ele precisa garantir ACID.
- Utilizando o Dapper para aumentar a performance na gravacao e consulta dos dados.

## Como rodar o projeto
Para rodar o docker e subir o container com o projeto basta entrar dentro da pasta src da applicação e rodar o comando docker-compose up.<br>
No navegador acessar a url http://localhost:8080/swagger/index.html.<br>
Por padrão ele ira consultar a cota da petrobras PETR4.SA, mas pode escolher qualquer cota.
