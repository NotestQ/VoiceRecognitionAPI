# VoiceRecognitionAPI
Loads Windows Speech recognition into Content Warning and provides a dev-friendly way of using it.

## Attributions
This is a VoiceRecognitionAPI port for Content Warning made with written permission from the original developer.  
The original VoiceRecognitionAPI is created by [LoafOrc](https://github.com/LoafOrc/), you may get the original library [here](https://github.com/LoafOrc/VoiceRecognitionAPI/).  

The port modifies one patch from Lethal Company start-up method to a Content Warning start-up method. 

## Additions
This is a simple API that, by itself, adds nothing. This is intended for mod developers to build their own features from. It is recommended to look at their feature list instead.  

## Issues
If the mod is throwing an error use [the github issue page](https://github.com/NotestQ/VoiceRecognitionAPI/issues) and copy-paste the error in there.

If it is loading correctly but isn't detecting your voice:
 - Make sure you have set your [default microphone](https://www.howtogeek.com/700440/how-to-choose-your-default-microphone-on-windows-10/) correctly.
 - Change BepInEx's `LogLevels` in its config found at `(ContentWarningDir)/BepInEx/config/BepInEx.cfg`. to include `, Debug` or change it to `All`. If it is getting wrong results try [improving your speech recognition](https://support.microsoft.com/en-us/windows/use-voice-recognition-in-windows-83ff75bd-63eb-0b6c-18d4-6fae94050571#:~:text=In%20Control%20Panel%2C%20select%20Ease,to%20set%20up%20speech%20recognition.)

## Mod Integration
Download the [latest dll](https://github.com/NotestQ/VoiceRecognitionAPI/releases/latest) from the releases tab and add it as a reference to the project. After adding it as a reference you can add it as a dependency:
```cs
[BepInDependency(VoiceRecognitionAPI.Plugin.modGUID)]
public class YourMod : BaseUnityPlugin { // ...
```
(Make sure to import it)  

### Usage
All VoiceRecognitionAPI methods are under the `Voice` class. It also good to note that they can NOT be defined at runtime. You should register your voice actions when your mod's `Awake` function is called to make sure they are registered in time. This is how to register a simple voice phrase:
```cs
Voice.ListenForPhrase("my cool voice phrase", (message) => {
     logger.LogInfo("You said my cool voice phrase!");
 });
```
If you'd like to have multiple different phrases to activate the same stuff you can do:
```cs
Voice.ListenForPhrases(new string[] { "my cool voice phrase", "my epic voice phrase" }, (message) => {
    logger.LogInfo("Both of those phrase activate this log statement.");
});
```  

Read the original [developer docs](https://github.com/LoafOrc/VoiceRecognitionAPI/wiki) for further information  

## Contributing
Uhhh so this is my first time really trying to do a public Github repo so bare with me if I screw something up.

If you'd like to build the mod for yourself:
 - Clone the repo
 - Create a .csproj.user file in the root containing:
```xml
<Project>
  <PropertyGroup>
    <!--This should be the path to the folder that contains `Content Warning.exe` usually people have it on the C: drive but incase not, change it here-->
    <ContentWarningPath>C:\Program Files (x86)\Steam\steamapps\common\Content Warning</ContentWarningPath> 
  </PropertyGroup>
</Project>
```
 - Then contribute I guess.
 - Build like you would a normal mod.
 - literally just changed Lethal Company to Content Warning, this feels wrong. This must be wrong.
