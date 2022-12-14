using Nethereum.Hex.HexTypes;
using Web3CS;
using Web3CS.Console;
using Web3CS.Enums;
using Web3CS.Protocol.EVM.RPCObjects;

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

/*
 * 
 METIS Nodes Do Not Support This RPC Method
Console.WriteLine($"\t= L2 Specific:");

OPL2RollupInfo rollupInfo = await web3Client.L2_GetRollupInfoAsync();

Console.WriteLine($"\t\t+ Operation Mode: '{rollupInfo.mode}'");
Console.WriteLine($"\t\t+ Syncing: '{rollupInfo.syncing}'");
*/


Console.WriteLine($"");
Console.WriteLine($"General/Utility Information:");
Console.WriteLine($"\t+ Encode Keccak Data: '{await web3Client.GenerateKeccakStringAsync("0x68656c6c6f20776f726c64")}'");

/* METIS Nodes Do Not Support This RPC Method
 * TODO: Investgate further.
 * 
string signedTX = await web3Client.SignTransactionAsync("0xb60e8dd61c5d32be8058bb8eb970870f07233155", "0xd46e8dd67c5d32be8058bb8eb970870f07244567", new HexBigInteger("0x76c0"), new HexBigInteger("0x9184e72a000"), new HexBigInteger("0x9184e72a"), new HexBigInteger("0x76c0"), "0xd46e8dd67c5d32be8d46e8dd67c5d32be8058bb8eb970870f072445675058bb8eb970870f072445675");
Console.WriteLine($"Sign Transaction: '{signedTX}'");       
*/

//https://bytemeta.vip/repo/ethereum-optimism/optimism/issues/1218
//testing this on Metis which is an Optimism L2 fork
//Console.WriteLine($"\t+ Sign Message: '{await web3Client.SignMessageAsync(onChainAddressToUse, "Moo Said The Fish To The Farmer!")}'");



Console.WriteLine($"");
Console.WriteLine($"Network Information:");
Console.WriteLine($"\t+ Network ID: '{await web3Client.GetNetworkIDAsync()}'");
Console.WriteLine($"\t+ Gas Price: '{await web3Client.GetGasPriceAsync()}' Wei");

/*
 * 
 METIS Nodes Do Not Support This RPC Method
Console.WriteLine($"\t= L2 Specific:");

OPL2RollupGASPrices rollupGASPrices = await web3Client.L2_GASPricesAsync();

Console.WriteLine($"\t\t+ L1 GAS: '{rollupGASPrices.l1GasPrice}', L2 GAS: '{rollupGASPrices.l2GasPrice}'");
*/



Console.WriteLine($"");
Console.WriteLine($"Block Information:");
Console.WriteLine($"\t+ Current Block: '{latestBlock}'");
Console.WriteLine($"\t+ TX Count: '{await web3Client.GetTransactionCountByNumberAsync(latestBlock)}'");
Console.WriteLine($"\t+ Uncle Count: '{await web3Client.GetUncleCountByBlockNumberAsync(latestBlock)}'");
Console.WriteLine($"\t= L2 Specific:");

EVMBlock<EVMBlockTransaction>[] blocks = await web3Client.L2_GetBlockRangeAsync(new HexBigInteger(latestBlock.Value - 2), latestBlock, false);

Console.WriteLine($"\t\t+ Get Block Range - Latest: {latestBlock}, Range: {latestBlock}-{new HexBigInteger(latestBlock.Value - 2)} (Last 3 Blocks)");
foreach(EVMBlock block in blocks)
{
    Console.WriteLine($"\t\t\t - Block: '{new HexBigInteger(block.number).Value}', Tx Count:'{block.transactions.Length}', GAS Used:'{block.gasUsed}' Hash: '{block.hash.Shorten()}'");
}//foreach(EVMBlock block in blocks)


Console.WriteLine($"\t= L1 Specific:");

EVMBlock<EVMBlockTransaction> latestBlockData = await web3Client.GetBlockByNumberAsync(EVMDefaultBlockParams.Latest, false);
Console.WriteLine($"\t\t - Block: '{new HexBigInteger(latestBlockData.number).Value}', Tx Count:'{latestBlockData.transactions.Length}', GAS Used:'{latestBlockData.gasUsed}' Hash: '{latestBlockData.hash.Shorten()}'");

foreach(EVMBlockTransaction tx in latestBlockData.transactions)
{
    Console.WriteLine($"\t\t\t - TX: From: '{tx.from.Shorten()}', To:'{tx.to.Shorten()}', Nonce:'{tx.nonce}', Input: '{tx.input.Shorten()}' Hash: '{tx.hash.Shorten()}'");
}//foreach(EVMBlock block in blocks)


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