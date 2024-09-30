![Fishbowl](https://raw.githubusercontent.com/Kirkpajl/FishbowlInventory.LegacyApi/master/fishbowl-logo.png "Fishbowl")
# Fishbowl Inventory Legacy API implementation in C#.NET

#### IMPORTANT NOTE:
* I am not actively maintaining this repo project.
* If you find issues - please reach out with a PR that would solve it. I would try to push NuGET with updates to help as soon as I can.
* If anyone is interested to take over as maintainer - please contact me, thanks!

## Latest NuGet Release:
This library can be used from NuGet channel:

* [Fishbowl Inventory Legacy API Package](https://www.nuget.org/packages/FishbowlInventory.LegacyApi/) - Version 1.0.6: `Install-Package FishbowlInventory.LegacyApi`

**Please note:** This source and nuget are a work-in-progress.  Not all API methods/calls have been included/tested.

## Example Usage

### Get user permissions

```C#
// Initialize the Fishbowl Inventory Legacy API client
using var client = new FishbowlInventoryApiClient("localhost", 28192, "Legacy API Test Client", "Tests the legacy API endpoints", 1234, "admin", "admin");

// Authenticate with the Fishbowl Inventory server
var userInfo = await client.LoginAsync();

if (userInfo == null) return false;

// Output User details
Console.WriteLine($"User Name:  {userInfo.FullName}");
Console.WriteLine($"Allowed Modules ({userInfo.AllowedModules.Length}):");
foreach (var module in userInfo.AllowedModules) Console.WriteLine($"  * {module}");
Console.WriteLine($"Server Version:  {userInfo.ServerVersion}");

// Terminate the Fishbowl Inventory user session
await client.LogoutAsync();
```

## Documentation:
For further details on how to use/integrate the FishbowlInventory.LegacyApi package, please refer to the repository wiki page.

[Fishbowl Inventory LEGACY API .NET SDK WIKI](https://github.com/Kirkpajl/FishbowlInventory.LegacyApi/wiki)

## Issues / Bugs:
If you have a query, issues or bugs, it means that you have shown interest in this project, and I thank you for that.
Feel free to ask, suggest, report issue or post a bug [here](https://github.com/Kirkpajl/FishbowlInventory.LegacyApi/issues) in context of this library use.

**Please note:** If your query/issue/bug is related to Fishbowl Inventory LEGACY API, I recommend posting it to the official [Fishbowl Support](https://help.fishbowlinventory.com/s/) forum.

You can find all of the methods to connect with me at my [blog](https://joshuakirkpatrick.com/contact) (ref. footer)

## References:

* [Fishbowl Inventory LEGACY API](https://help.fishbowlinventory.com/s/article/Fishbowl-API) - Official documentation
* [My Blog](https://joshuakirkpatrick.com/) - My personal blog

## Credits / Disclaimer:

* Fishbowl Advanced logo used in this readme file is owned by and copyright of Fishbowl.
* I am not affiliated with Fishbowl, this work is solely undertaken by me.
* This library is not or part of the official set of libraries from Fishbowl and hence can be referred as Third party library for Fishbowl using .NET.

## License

This work is [licensed](https://github.com/Kirkpajl/FishbowlInventory.LegacyApi/blob/master/LICENSE) under:

The MIT License (MIT)
Copyright (c) 2024 Josh Kirkpatrick