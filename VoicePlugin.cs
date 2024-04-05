using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.IO;
using System.Reflection;
using System.Speech.Recognition;
using VoiceRecognitionAPI.Patches;

namespace VoiceRecognitionAPI {

    [BepInPlugin(modGUID, modName, modVersion)]
    public class VoicePlugin : BaseUnityPlugin {
        public const string modGUID = "me.loaforc.voicerecognitionapi";
        public const string modName = "VoiceRecognitionAPI";
        public const string modVersion = "1.2.1";

        private static readonly Harmony harmony = new Harmony(modGUID);
        internal static VoicePlugin instance;
        internal static ManualLogSource logger;

        void Awake() {
            if (instance == null) instance = this; // Signleton
            else return; // Make sure nothing else gets loaded.
            logger = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            // Make sure instead of failing we load the System.Speech Library from the embedded resources.
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) => {
                logger.LogDebug("Importing " + args.Name);

                String resourceName = modName + ".Resources." + new AssemblyName(args.Name).Name + ".dll";
                logger.LogDebug("Located at: " + resourceName);

                if (Assembly.GetExecutingAssembly().GetManifestResourceInfo(resourceName) == null) return null;

                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
                logger.LogDebug("Found file! Length: " + stream.Length);

                byte[] assemblyData = new byte[stream.Length];
                stream.Read(assemblyData, 0, assemblyData.Length);

                try {
                    Assembly loaded = Assembly.Load(assemblyData);
                    logger.LogDebug($"Loaded {loaded.FullName}");

                    return loaded;
                } catch (Exception ex) {
                    logger.LogError("Failed to load assembly: " + "\n" + ex);
                    return null;
                }
            };

            logger.LogInfo("Applying Patches");
            harmony.PatchAll(typeof(NetworkVoiceHandlerPatch));

            logger.LogInfo(modName + ":" + modVersion + " has succesfully loaded!");
        }
    }
}
