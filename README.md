# 🎵 Pedro Pedro Speaker (Thunderstore Mod)

**Pedro Pedro Speaker** is a BepInEx plugin for Unity games that replaces the in-game boombox music with the legendary **Pedro Pedro** song — all secretly embedded inside the `.dll` file!

---

## 🎯 Features

- 🔊 Replaces the default boombox sound with a custom embedded `.ogg` audio clip.
- 🧼 No visible `.mp3` or `.ogg` files — the audio is hidden inside the `.dll`.
- 🧩 Compatible with BepInEx and Unity games using `ValuableBoombox` (tested with REPO).

---

## 💡 How It Works

1. The `.ogg` file is embedded directly into the plugin DLL as a resource.
2. When the plugin loads, it extracts the sound into a temporary file and loads it via UnityWebRequest.
3. On game start, the plugin patches the `ValuableBoombox.Start()` method using Harmony.
4. The default sound is replaced with the new Pedro Pedro AudioClip!

---

## 🛠️ Requirements

- [.NET Framework-compatible IDE (e.g., Visual Studio)](https://visualstudio.microsoft.com/)
- [BepInEx 5.x](https://github.com/BepInEx/BepInEx)
- [Harmony](https://github.com/pardeike/Harmony)
- Repo

---

## 🧪 Installation

1. Drop the compiled `PedroPedroSpeaker.dll` into your game's `BepInEx/plugins` folder.
2. Launch the game.
3. Spawn a boombox... and let Pedro Pedro do the talking.

---

---

## ⚠️ Legal Note

This mod embeds copyrighted audio for **personal/fun** use and should not be uploaded publicly to mod stores like Thunderstore **without permission** from the original artist. Use responsibly.

---
