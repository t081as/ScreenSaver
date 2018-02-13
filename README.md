![SCREENSAVER](https://gitlab.com/tobiaskoch/ScreenSaver/raw/master/Media/ScreenSaver-256.png)

# SCREENSAVER

[![pipeline status](https://gitlab.com/tobiaskoch/ScreenSaver/badges/master/pipeline.svg)](https://gitlab.com/tobiaskoch/ScreenSaver/commits/master)
[![maintained: yes](https://tobiaskoch.gitlab.io/badges/maintained-yes.svg)](https://gitlab.com/tobiaskoch/ScreenSaver/commits/master)
[![donate: paypal](https://tobiaskoch.gitlab.io/badges/donate-paypal.svg)](https://www.tk-software.de/donate)

ScreenSaver is a .NET library for creating screen savers for the Microsoft Windows operating system.

## Installation

### Option 1: NuGet
NuGet packages will be available later.

### Option 1: Binary
Stable versions can be downloaded [here](https://gitlab.com/tobiaskoch/ScreenSaver/pipelines?scope=tags).

### Option 2: Source
#### Requirements
The following applications must be available and included in you *PATH* environment variable:

* [Git](https://git-scm.com/)
* [Nuget.exe](https://www.nuget.org/)
* MSBuild (.NET Framework / Mono; [Visual Studio](https://www.visualstudio.com) recommended for development)

#### Source code
Get the source code using the following command:

    > git clone https://gitlab.com/tobiaskoch/ScreenSaver.git

#### Build
    > .\Build.cmd

The library will be located in the directory *.\Build\Release* if the build succeeds.

## Usage
The solution contains an example that demonstrates the usage of this library.

## Contributing
see [CONTRIBUTING.md](https://gitlab.com/tobiaskoch/ScreenSaver/blob/master/CONTRIBUTING.md)

## Contributors
see [AUTHORS.txt](https://gitlab.com/tobiaskoch/ScreenSaver/blob/master/AUTHORS.txt)

## Donating
Thanks for your interest in this project. You can show your appreciation and support further development by [donating](https://www.tk-software.de/donate).

## License
**ScreenSaver** © 2017-2018  Tobias Koch. Released under the [MIT License](https://gitlab.com/tobiaskoch/ScreenSaver/blob/master/LICENSE.md).