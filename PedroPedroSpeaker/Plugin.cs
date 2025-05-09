using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using UnityEngine.Networking;

namespace PedroPedroSpeaker
{
    [BepInPlugin("com.masterxolos.pedrospeaker", "Pedro Pedro Speaker", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal new static ManualLogSource Logger;
        internal static AudioClip pedroClip;

        private void Awake()
        {
            Logger = base.Logger;
            Logger.LogInfo("Pedro Pedro Speaker plugin loaded!");

            // Load embedded pedro.ogg from DLL resources
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("PedroPedroSpeaker.pedro.ogg"))
            {
                if (stream != null)
                {
                    byte[] data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);
                    string tempPath = Path.Combine(Path.GetTempPath(), "pedro_temp.ogg");
                    File.WriteAllBytes(tempPath, data);

                    UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + tempPath, AudioType.OGGVORBIS);
                    var request = www.SendWebRequest();
                    while (!request.isDone) { }

                    if (www.result == UnityWebRequest.Result.Success)
                    {
                        pedroClip = DownloadHandlerAudioClip.GetContent(www);
                        Logger.LogInfo("Successfully loaded embedded pedro.ogg");

                        // Optional: Play immediately to confirm it works
                        GameObject go = new GameObject("PedroSpeakerTest");
                        AudioSource source = go.AddComponent<AudioSource>();
                        source.clip = pedroClip;
                        source.Play();
                    }
                    else
                    {
                        Logger.LogError("Failed to load AudioClip from embedded resource");
                    }
                }
                else
                {
                    Logger.LogError("Embedded resource 'pedro.ogg' not found");
                }
            }

            // Apply Harmony patches
            Harmony harmony = new Harmony("com.masterxolos.pedrospeaker");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(ValuableBoombox), "Start")]
        public class PatchBoomboxStart
        {
            static void Postfix(ValuableBoombox __instance)
            {
                if (__instance.soundBoomboxMusic != null && Plugin.pedroClip != null)
                {
                    __instance.soundBoomboxMusic.Sounds = new AudioClip[] { Plugin.pedroClip };
                    Plugin.Logger.LogInfo("Successfully replaced the boombox music with Pedro Pedro!");
                }
                else
                {
                    Plugin.Logger.LogWarning("Could not replace boombox music: Missing soundBoomboxMusic or pedroClip.");
                }
            }
        }
    }
}
