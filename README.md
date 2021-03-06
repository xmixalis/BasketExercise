# Basket API .NET Core v2.1

<p>Basket API project is a set of complete Basket .NET Core Web API with an API Client and a User Interface application.</p>
<p>The complete code of the project can be found on <a target="_blank" href="https://github.com/xmixalis/BasketExercise">GitHub</a> and consists of the following projects. </p>
<ul>
    <li>Project for the Basket API targeting .NET Core v2.1</li>
    <li>Infrastructure project targeting .NET Standard v2.0</li>
    <li>API Client project targeting .NET Standard v2.0</li>
    <li>Shared Models project to be used by the API Client as well as the Web API</li>
    <li>Web Front-end project targeting .NET Core v2.1</li>
    <li>Test projects that are provided as seperate projects</li>
</ul>
<p>The Web API and Web UI projects are hosted as Azure services and documentation for the API can be found in the <a target="_blank" href="http://panchbasketapi-live.azurewebsites.net/index.html">Home page of the API url.</a>.
</p>
<p>You can also <a href="http://panchbasketui-live.azurewebsites.net/"> visit the Web UI prototype</a> in order to test the API operations. </p>

<p>Assumptions of the current implementation.</p>
<ul>
    <li>System uses in memory database to store data</li>
    <li>If a user is not authenticated then a unique ID is stored in a cookie and is mapped with a unique Basket ID.</li>
    <li>When a user logs in , a new Basket is created (or retrieved if exists) for the user. An improvement could be to transfer the anonymous basket to the User.</li>
    <li>The user updates the quantities or removes the items for each item separately.</li>
	<li>API Client project is configured to create a Nuget package configuration file in order to be deployed as a .NET Standard v2.0 library.</li>
	<li>Checkout library can be integrated with the current Web UI once it have been created as a different library.</li>
	<li>Base API address can be configured in the Web UI project changing the "APIBaseUrl" setting in appsettings.json.</li>
	<li>Live Web UI and API have been connected with Azure Application Insights for performance monitoring. </li>
</ul>
 <p>
 In order to integrate with the API using the API client provided yoy have to add a using statetement for the client library and access one of the services provided with the client;</p>
<p><u>Example of using the API Client</u></p>

```csharp
using BasketApi.Client;
```

```csharp
BasketApiClient client = new BasketApiClient("http://apibaseurl");
BasketModelResponse basket = await client.BasketService.GetBasketForUser(userId);
```
