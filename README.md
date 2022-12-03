# .NET Core Web3 SDK
This project was born out of the frustration of wanting a simple JSON RPC client that was very quick and easy to use.

<span style="color:red;">**THIS PROJECT IS A RESULT OF A 1 DAY DEV SPRINT, DO NOT USE IN PRODUCTION.  USE AT OWN RISK**</span>

## Prior Works
There are already 2 projects (outlined below) that have done an incredible amount of work in this space.  Without these projects and their communities acting as guidance this SDK would not be as far along.

Projects (No Partiqular Order):
* Nethereum - https://nethereum.com/ / https://github.com/Nethereum/Nethereum
* Nethermind - https://nethermind.io/nethermind-client/   /  https://github.com/NethermindEth/nethermind



#### Key Differentiator
The above projects provide the complete package for running a Node/Client/Endpoint, while the code is split out into various losely coupled libraries, there is still a large amount of interdependancy occuring which isnt simple to figure out.

The Author wanted a simple library similar to etherjs and web3js without "the kitchen sink".



## Example Useage

```
Web3 web3Client = new Web3(new Uri("https://andromeda.metis.io"));

Console.WriteLine($"Gas Price: '{await web3Client.GetGasPriceAsync()}' Wei");
Console.WriteLine($"Current Block: '{await web3Client.GetLatestBlockNumberAsync()}'");
Console.WriteLine($"Endpoint Version: '{await web3Client.GetWeb3VersionAsync()}'");
```

Outputs:
```
Gas Price: '15500000000' Wei
Current Block: '4096950'
Endpoint Version: 'Geth/v1.9.10-stable/linux-amd64/go1.17.7'
```




## Key Technologies Used
The following core technolgies are used.

**Web3 Class**

This is just a front-end class that follows the facade pattern, this is where data validation and processing will take place.  The goal is to provide a single unified, strongly typed interface to users.  This class will pull all the strings underneath.


**Microsoft StreamJsonRpc**
https://github.com/microsoft/vs-streamjsonrpc

The library utilises this JSON-RPC package extensively to interpret the JSON-RPC protocol.  However, there are subtle differences within the implementaion of EVM/ETH JSON-RPC than its standard/compliant version.  As such, some protocol/parsing modifications were made:

*Message Handling*

All EVM JSON-RPC requests are sent to the root `/` path, this breaks URI's and the default behaviour of a number of things.  As such this [Message Handler Class](Web3CS/MessageHandler/EVMMessageHandler.cs) was written.  The gist is a `HttpClient` making a `POST` request to `/` within the `WriteAsync` method, the response is then queued up and the `ReadAsync` method then pops the info from the queue and parses the data.  class was heavily inspired from [HttpClientMessageHandler](https://github.com/microsoft/vs-streamjsonrpc/blob/main/doc/extensibility.md).

*EVM RPC Implementation*

The protocol uses StreamJsonRpc's strongly typed implementation method, the EVM implementation can be found [within this class](Web3CS/Protocol/EVM/IEVMProtocol.cs).


**Nethereum.Hex**
https://github.com/Nethereum/Nethereum/tree/master/src/Nethereum.Hex

I am utilising this package for `HexBigInteger` to deal with the large numbers used within the EVM.
