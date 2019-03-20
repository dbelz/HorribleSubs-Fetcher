<h1 align="center">Horrible Subs Fetcher</h1>
<div align="center">

[![forthebadge](https://forthebadge.com/images/badges/made-with-c-sharp.svg)](https://forthebadge.com)
[![forthebadge](https://forthebadge.com/images/badges/built-with-grammas-recipe.svg)](https://forthebadge.com)

[![GitHub license](https://img.shields.io/github/license/dbelz/Horrible-Subs-Fetcher.svg?longCache=true&style=flat-square)](https://github.com/dbelz/Horrible-Subs-Fetcher/blob/master/LICENSE.md)
[![Nuget](https://img.shields.io/nuget/v/HorribleSubsFetcher.svg?style=flat-square)](https://www.nuget.org/packages/HorribleSubsFetcher/)
[![GitHub last commit](https://img.shields.io/github/last-commit/dbelz/Horrible-Subs-Fetcher.svg?longCache=true&style=flat-square)](https://github.com/dbelz/Horrible-Subs-Fetcher)
[![GitHub issues](https://img.shields.io/github/issues/dbelz/Horrible-Subs-Fetcher.svg?longCache=true&style=flat-square)](https://github.com/dbelz/Horrible-Subs-Fetcher/issues)

This is a simple library which is able to search, fetch and parse the xdcc packlist of https://xdcc.horriblesubs.info/

<sub>Built with ❤︎ by Daniel Belz</sub>
</div><br>

* [Getting started](#getting-started)
    * [Fetching all bots](#fetching-all-bots)
    * [Fetching the packlist of a bot](#fetching-the-packlist-of-a-bot)
    * [Fetching all packlists](#fetching-all-packlists)
    * [Finding packs of a bot](#finding-packs-of-a-bot)
    * [Finding packs](#finding-packs)
    * [Pack ToString](#pack-tostring)

<br>

# Getting started
### Fetching all bots
```csharp
private static async Task Main()
{
	var httpClient = new HttpClient();

    var fetcher = new Fetcher(httpClient);
    var tokenSource = new CancellationTokenSource();

    var bots = await fetcher.FetchBotListAsync(tokenSource.Token);

    foreach (var bot in bots)
        Console.WriteLine(bot);

    Console.ReadLine();
}
```

[Sample Solution](https://github.com/dbelz/Horrible-Subs-Fetcher/tree/master/samples/FetchBotList)

### Fetching the packlist of a bot
```csharp
private static async Task Main()
{
	var httpClient = new HttpClient();

    var fetcher = new Fetcher(httpClient);
    var tokenSource = new CancellationTokenSource();

    const string bot = "Ginpachi-Sensei";

    var packList = await fetcher.FetchBotPackListAsync(bot, tokenSource.Token);

    foreach (var item in packList)
        Console.WriteLine(item);

    Console.ReadLine();
}
```

[Sample Solution](https://github.com/dbelz/Horrible-Subs-Fetcher/tree/master/samples/FetchBotPackList) 

### Fetching all packlists
```csharp
private static async Task Main()
{
	var httpClient = new HttpClient();

    var fetcher = new Fetcher(httpClient);
    var tokenSource = new CancellationTokenSource();

    var packList = await fetcher.FetchPackListsAsync(tokenSource.Token);

    foreach (var item in packList)
        Console.WriteLine(item);

    Console.ReadLine();
}
```

[Sample Solution](https://github.com/dbelz/Horrible-Subs-Fetcher/tree/master/samples/FetchPackList) 

### Finding packs of a bot
```csharp
private static async Task Main()
{
	var httpClient = new HttpClient();

    var fetcher = new Fetcher(httpClient);
	var tokenSource = new CancellationTokenSource();

    var bot = "Ginpachi-Sensei";

    var packList = await fetcher.FindBotPacksAsync("Toradora", bot, tokenSource.Token);

    foreach (var item in packList)
        Console.WriteLine(item);

    Console.ReadLine();
}
```

[Sample Solution](https://github.com/dbelz/Horrible-Subs-Fetcher/tree/master/samples/FindBotPacks)

### Finding packs
```csharp
private static async Task Main()
{
	var httpClient = new HttpClient();

    var fetcher = new Fetcher(httpClient);
	var tokenSource = new CancellationTokenSource();

    var packList = await fetcher.FindPacksAsync("Toradora", tokenSource.Token);

    foreach (var item in packList)
        Console.WriteLine(item);

    Console.ReadLine();
}
```

[Sample Solution](https://github.com/dbelz/Horrible-Subs-Fetcher/tree/master/samples/FindPacks) 

### Pack ToString

The `Pack` class overrides the `ToString()` method. It returns a string which can be copy pasted in your favorite irc/xdcc client:
```csharp
private static async Task Main()
{
	var httpClient = new HttpClient();

    var fetcher = new Fetcher(httpClient);
	var tokenSource = new CancellationTokenSource();

    var packList = await fetcher.FetchPackListsAsync(tokenSource.Token);

    foreach (var item in packList)
    {
        // returns: /msg $BOT$ xdcc send #$PACKNUMBER$
        Console.WriteLine(item);
    }

    Console.ReadLine();
}
```

## Contributing

__Contributions are always welcome!__  
Just send me a pull request and I will look at it. If you have more changes please create a issue to discuss it first.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
