## C# IDeskBand2 Sample - a Media control Deskband for Windows 7+

![Screenshot](https://raw.githubusercontent.com/navhaxs/media-control-deskband/master/screenshot.png)

Original source from Pavel Zolnikov's article ["Extending Explorer with Band Objects using .NET and Windows Forms"](http://www.codeproject.com/Articles/2219/Extending-Explorer-with-Band-Objects-using-NET-and), from way back in 2002. See for instructions.

I then adapted it to the IDeskBand2 interface.

For this example project, I added some rudimentary media control buttons to the deskband. For this I used SendInput() from [InputSimulator](https://inputsimulator.codeplex.com/).
 
This VS project builds with .NET 4.6.1 on Windows 10 x64.

Clearly most of this project lends itself to Win32 and C++ rather than C#, but this will do for now. Did not achieve native theming (such as button hover styles consistent to msstyles), which may or may not be even possible on Windows 10.

#### Get started

1. Open SampleDeskband.sln in Visual Studio

2. Build `BandObjectLib`. `SampleControl` is a dependency and will also get built.

3. Follow the installation steps below

#### Installation

Follow these commands to install the deskband dll into the GAC - required for .NET assemblies to be loaded as a deskband. This includes any dependency assemblies as well (to my knowledge).

**Note: The dll must be (re)installed into the GAC each and every time the dll is modified.**

Start an elevated command prompt, then run `env.bat` which has some aliases set up (you likely will need to edit this file to match exact version numbers on your system)

    (If you built in release mode) cd .\SampleDeskband\bin\Release\
	
	(Or, if you built in debug mode) cd .\SampleDeskband\bin\Debug\

    gacutil.exe /if SampleDeskband.dll

    gacutil.exe /if BandObjectLib.dll

    gacutil.exe /if WindowsInput.dll

Next, register our main deskband assembly,

    regasm.exe SampleDeskband.dll

Lastly, restart explorer

	k

	explorer.exe

### Notes

- Launching explorer.exe in an elevated prompt means that explorer.exe will be elevated too. In the past I used to find that calling SendInput from an evelated explorer to a non-evelated Spotify didn't work... but it seems to work ok now?

- To build `Interop.SHDocVw.dll` - the interop to the "Shell Doc Object and Control Library (SHDocVw.dll)" - use the Type Library Importer tool (tlbimp.exe) with a snk file. I used the MyExampleProject.snk for this example project.

- If you find a better way to do something, a mistake, or something that could be improved, please do open an issue!

#### Also see

* https://github.com/dwmkerr/sharpshell/
* https://msdn.microsoft.com/en-us/library/bb762064(VS.85).aspx
