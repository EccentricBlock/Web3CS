using Nethereum.Hex.HexTypes;
using Web3CS;
using Web3CS.Enums;

Console.WriteLine("Hello, World!");

Web3 web3Client = new Web3(new Uri("https://andromeda.metis.io"));
//Web3 web3Client = new Web3(new Uri("http://192.168.8.10:8545"));

string onChainAddressToUse = "0x00000398232e2064f896018496b4b44b3d62751f";



HexBigInteger latestBlock = await web3Client.GetLatestBlockNumberAsync();

Console.WriteLine($"Endpoint Information:");
Console.WriteLine($"\t+ Client Version: '{await web3Client.GetWeb3VersionAsync()}'");
Console.WriteLine($"\t+ Listening For New Connections: '{await web3Client.GetEndpointListeningStateAsync()}'");
Console.WriteLine($"\t+ Peer Count: '{await web3Client.GetEndpointPeerCountAsync()}'");
Console.WriteLine($"\t+ Protocol Version: '{await web3Client.GetEndpointProtocolVersionAsync()}'");
Console.WriteLine($"\t+ Syncing: '{await web3Client.GetEndpointSyncStatusAsync()}'");
Console.WriteLine($"\t+ Coinbase: '{await web3Client.GetEndpointCoinbaseAsync()}'");
Console.WriteLine($"\t+ Mining: '{await web3Client.GetEndpointMiningStateAsync()}' @ {await web3Client.GetEndpointHashRateAsync()} hps");

string[] accounts = await web3Client.GetEndpointAccountsAsync().ConfigureAwait(false);

Console.WriteLine($"\t+ Accounts: Count '{accounts.Length}'");
foreach(string account in accounts)
{
    Console.WriteLine($"\t\t- {account} [Bal: {await web3Client.GetBalanceAsync(account, latestBlock)} WEI, TX Count {await web3Client.GetTransactionCountAsync(account, latestBlock)}]");
}


Console.WriteLine($"");
Console.WriteLine($"General/Utility Information:");
Console.WriteLine($"\t+ Encode Keccak Data: '{await web3Client.GenerateKeccakStringAsync("0x68656c6c6f20776f726c64")}'");

//https://bytemeta.vip/repo/ethereum-optimism/optimism/issues/1218
//testing this on Metis which is an Optimism L2 fork
//Console.WriteLine($"\t+ Sign Message: '{await web3Client.SignMessageAsync(onChainAddressToUse, "Moo Said The Fish To The Farmer!")}'");



Console.WriteLine($"");
Console.WriteLine($"Network Information:");
Console.WriteLine($"\t+ Network ID: '{await web3Client.GetNetworkIDAsync()}'");
Console.WriteLine($"\t+ Gas Price: '{await web3Client.GetGasPriceAsync()}' Wei");





Console.WriteLine($"");
Console.WriteLine($"Block Information:");
Console.WriteLine($"\t+ Current Block: '{latestBlock}'");
Console.WriteLine($"\t+ TX Count: '{await web3Client.GetTransactionCountByNumberAsync(latestBlock)}'");
Console.WriteLine($"\t+ Uncle Count: '{await web3Client.GetUncleCountByBlockNumberAsync(latestBlock)}'");

need to write POC and test the method below.

L2_GetBlockRangeAsync




Console.WriteLine($"");
Console.WriteLine($"Contract/Wallet Information:");
Console.WriteLine($"\t+ TX Count: '{await web3Client.GetTransactionCountAsync(onChainAddressToUse, latestBlock)}'");

//Metis: MaiaDAO Hermes Token Address
string targetContractAddress = "0xb27BbeaACA2C00d6258C3118BAB6b5B6975161c8";
string contractCode = await web3Client.GetCodeAsync(targetContractAddress, EVMDefaultBlockParams.Latest);

Console.WriteLine($"\t+ Code For Contract '{targetContractAddress}' Is '{contractCode.Length}' Bytes Long");














// need to work out
//Console.WriteLine($"+ Get Storage At Slot 0: '{await web3Client.GetSlotAtAsync(onChainAddressToUse, 0, EVMDefaultBlockParams.Latest)}'");





Console.ReadKey();