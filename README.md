## C# IDeskBand2 Sample - a Media control Deskband for Windows 7+
	
![Screenshot](https://raw.githubusercontent.com/navhaxs/media-control-deskband/master/screenshot.png)

Original source from Pavel Zolnikov's article ["Extending Explorer with Band Objects using .NET and Windows Forms"](http://www.codeproject.com/Articles/2219/Extending-Explorer-with-Band-Objects-using-NET-and), from way back in 2002. See for instructions.

I then added the IDeskBand2 interface, and a couple of buttons hooked up to SendInput using [InputSimulator](https://inputsimulator.codeplex.com/)

This builds with .NET 4.6 on Windows 10 x64. Clearly most of this project lends itself to win32, but this will do for now. Did not achieve native theming (such as button hover styles consistent to msstyles), which may or may not be even possible on Windows 10.

#### Installation steps

Start an elevated command prompt, then run `env.bat` (may need to adjust)

`cd .\SampleBars\bin\Release\

gacutil.exe /if SampleBars.dll

gacutil.exe /if BandObjectLib.dll

gacutil.exe /if WindowsInput.dll

regasm.exe SampleBars.dll`

The dll must be (re)installed into the GAC each and every time it is changed.

### Notes

`Interop.SHDocVw.dll` is generated using tlbimp.exe with the snk - on the actual SHDocVw.dll.

Launching explorer.exe in an elevated prompt means that explorer.exe will be elevated too. For some reason the SendInput from an evelated explorer to a non-evelated Spotify doesn't work?

#### Also see

* https://github.com/dwmkerr/sharpshell/
* https://msdn.microsoft.com/en-us/library/bb762064(VS.85).aspx
