![SCREENSAVER](https://gitlab.com/tobiaskoch/ScreenSaver/raw/master/Media/ScreenSaver-256.png)

# SCREENSAVER

ScreenSaver is a .NET library for creating screen savers for the Microsoft Windows operating system.

## Installation

### Option 0: NuGet
NuGet packages are available [here](https://www.nuget.org/packages/ScreenSaver/).

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

Add a reference to the ScreenSaver nuget package or the ScreenSaver library

Create a UserControl that serves as screen saver

Create a screen saver configuration class implementing the `IScreenSaverConfiguration` interface:

```csharp
/// <summary>
/// The configuration of the test screen saver.
/// </summary>
internal class ScreenSaverTestConfiguration : IScreenSaverConfiguration
{
    #region Properties

    /// <summary>
    /// Gets the name of the screen saver.
    /// </summary>
    public string ScreenSaverName
    {
        get
        {
            return "Test";
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Creates and returns a new instance of a <see cref="Control"/> used as a screen saver for the given screen.
    /// </summary>
    /// <param name="screen">
    /// The <see cref="Screen"/> this screen saver control shall be used for
    /// or <c>null</c> if the control is used as a preview.
    /// </param>
    /// <returns>A <see cref="Control"/> used as a screen saver for the given screen.</returns>
    public Control CreateScreenSaverControl(Screen screen)
    {
        return new FancyScreenSaverControl();
    }

    /// <summary>
    /// Creates and returns a new instance of a <see cref="Control"/> used as a preview of the screen saver.
    /// </summary>
    /// <returns>
    /// A <see cref="Control"/> used as a preview of the screen saver.
    /// </returns>
    public Control CreatePreviewControl()
    {
        return new FancyScreenSaverControl();
    }

    /// <summary>
    /// Creates and returns a new instance of a <see cref="Control"/> used to configure the screen saver.
    /// </summary>
    /// <returns>
    /// A <see cref="Control"/> used to configure the screen saver or <c>null</c> if the screen saver
    /// can't be configured.
    /// </returns>
    public ConfigurationControl CreateConfigurationControl()
    {
        return null;
    }
}
```

Add the following code to your Main method:

```csharp
public static void Main(string[] args)
{
    ScreenSaver screenSaver = new ScreenSaver(new ScreenSaverTestConfiguration());
    screenSaver.Run(args);
}
```

That's it.

The solution contains an example that demonstrates the usage of this library and additional helper classes.

**Please keep in mind that the helper classes are implemented using the Graphics.DrawImage method; feel free to use a more performant implementation (DirectX).**

## Contributing
see [CONTRIBUTING.md](https://gitlab.com/tobiaskoch/ScreenSaver/blob/master/CONTRIBUTING.md)

## Contributors
see [AUTHORS.txt](https://gitlab.com/tobiaskoch/ScreenSaver/blob/master/AUTHORS.txt)

## Donating
Thanks for your interest in this project. You can show your appreciation and support further development by [donating](https://www.tk-software.de/donate).

## License
**ScreenSaver** © 2017-2019  [Tobias Koch](https://www.tk-software.de). Released under the [MIT License](https://gitlab.com/tobiaskoch/ScreenSaver/blob/master/LICENSE.md).