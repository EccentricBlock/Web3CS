using Nethereum.Hex.HexTypes;
using Web3CS;

Console.WriteLine("Hello, World!");

Web3 web3Client = new Web3(new Uri("https://andromeda.metis.io"));
//Web3 web3Client = new Web3(new Uri("http://192.168.8.10:8545"));

string onChainAddressToUse = "0x00000398232e2064f896018496b4b44b3d62751f";



HexBigInteger latestBlock = await web3Client.GetLatestBlockNumberAsync();

Console.WriteLine($"Endpoint Information:");
Console.WriteLine($"+ Client Version: '{await web3Client.GetWeb3VersionAsync()}'");
Console.WriteLine($"+ Listening For New Connections: '{await web3Client.GetEndpointListeningStateAsync()}'");
Console.WriteLine($"+ Peer Count: '{await web3Client.GetEndpointPeerCountAsync()}'");
Console.WriteLine($"+ Protocol Version: '{await web3Client.GetEndpointProtocolVersionAsync()}'");
Console.WriteLine($"+ Syncing: '{await web3Client.GetEndpointSyncStatusAsync()}'");
Console.WriteLine($"+ Coinbase: '{await web3Client.GetEndpointCoinbaseAsync()}'");
Console.WriteLine($"+ Mining: '{await web3Client.GetEndpointMiningStateAsync()}' @ {await web3Client.GetEndpointHashRateAsync()} hps");

string[] accounts = await web3Client.GetEndpointAccountsAsync().ConfigureAwait(false);

Console.WriteLine($"+ Accounts: Count '{accounts.Length}'");
foreach(string account in accounts)
{
    Console.WriteLine($"    - {account} [Bal: {await web3Client.GetBalanceAsync(account, latestBlock)} WEI, TX Count {await web3Client.GetTransactionCountAsync(account, latestBlock)}]");
}


Console.WriteLine($"");
Console.WriteLine($"General/Utility Information:");
Console.WriteLine($"+ Encode Keccak Data: '{await web3Client.GenerateKeccakStringAsync("0x68656c6c6f20776f726c64")}'");
Console.WriteLine($"");
Console.WriteLine($"+ Network Information:");
Console.WriteLine($"+ Network ID: '{await web3Client.GetNetworkIDAsync()}'");
Console.WriteLine($"+ Gas Price: '{await web3Client.GetGasPriceAsync()}' Wei");
Console.WriteLine($"+ Current Block: '{latestBlock}'");

Console.WriteLine($"");
Console.WriteLine($"Contract/Wallet Information:");
Console.WriteLine($"TX Count: '{await web3Client.GetTransactionCountAsync(onChainAddressToUse, latestBlock)}'");

// need to work out
//Console.WriteLine($"+ Get Storage At Slot 0: '{await web3Client.GetSlotAtAsync(onChainAddressToUse, 0, EVMDefaultBlockParams.Latest)}'");





Console.ReadKey();