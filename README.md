<h1 align="center">Horrible Subs Fetcher</h1>
<div align="center">

[![forthebadge](https://forthebadge.com/images/badges/made-with-c-sharp.svg)](https://forthebadge.com)
[![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://forthebadge.com)
[![forthebadge](https://forthebadge.com/images/badges/you-didnt-ask-for-this.svg)](https://forthebadge.com)

[![GitHub license](https://img.shields.io/github/license/dbelz/Horrible-Subs-Fetcher.svg?longCache=true&style=flat-square)](https://github.com/dbelz/Horrible-Subs-Fetcher/blob/master/LICENSE.md)
[![Nuget](https://img.shields.io/nuget/v/HorribleSubsFetcher.svg?style=flat-square)](https://www.nuget.org/packages/HorribleSubsFetcher/)
[![GitHub last commit](https://img.shields.io/github/last-commit/dbelz/Horrible-Subs-Fetcher.svg?longCache=true&style=flat-square)](https://github.com/dbelz/Horrible-Subs-Fetcher)
[![GitHub issues](https://img.shields.io/github/issues/dbelz/Horrible-Subs-Fetcher.svg?longCache=true&style=flat-square)](https://github.com/dbelz/Horrible-Subs-Fetcher/issues)

This is a simple library which is able to search, fetch and parse the xdcc packlist of https://xdcc.horriblesubs.info/

<sub>Built with ❤︎ by Daniel Belz</sub>
</div><br>

# How to use it?
You can search for xdcc packs like this:
```csharp
public async Task FindToradoraPacksAsync()
{
    var fetcher = new Fetcher();
    var cancellationTokenSource = new CancellationTokenSource();

    var packs = await fetcher.SearchPacklistAsync("Tokyo Ghoul", cancellationTokenSource.Token);
}
```

The `PackEntry` class overrides the `ToString()` method. It returns a string which can be copy pasted in your favorite irc/xdcc client:
```csharp
public async Task FindToradoraPacksAsync()
{
    var fetcher = new Fetcher();
    var cancellationTokenSource = new CancellationTokenSource();

    var packs = await fetcher.SearchPacklistAsync("Tokyo Ghoul", cancellationTokenSource.Token);

    var firstPack = packs.First();

    // returns: /msg $BOT$ xdcc send #$PACKNUMBER$
    Console.WriteLine(firstPack);
}
```


## Contributing

__Contributions are always welcome!__  
Just send me a pull request and I will look at it. If you have more changes please create a issue to discuss it first.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
