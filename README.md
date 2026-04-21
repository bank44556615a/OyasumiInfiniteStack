# Oyasumi Infinite Stack 📦

A simple, lightweight, and dynamic stack size modifier for **Graveyard Keeper**. 
Built with BepInEx and HarmonyLib.

## ✨ Features

* **Dynamic Configuration:** Change the stack size in real-time via the BepInEx Configuration Manager (F1 menu). No need to restart the game!
* **Safe Restore:** If you disable the mod in the settings, all items will safely revert to their original vanilla stack sizes.
* **Smart Filtering:** Only modifies items that are naturally stackable (stack > 1). It completely ignores unstackable items (like tools, weapons, and armor) to prevent inventory bugs.
* **Custom Limits:** Set your max stack size anywhere from 1 to 9999 (Default: 999).

## 📋 Requirements
* [Graveyard Keeper](https://store.steampowered.com/app/599140/Graveyard_Keeper/)
* [BepInEx 5.x](https://github.com/BepInEx/BepInEx)

## 📥 Installation

1. Make sure you have **BepInEx** installed in your Graveyard Keeper game folder.
2. Download the latest `OyasumiInfiniteStack.dll` from the [Releases](../../releases) page.
3. Drop the `.dll` file into your `Graveyard Keeper/BepInEx/plugins` folder.
4. Launch the game!

## ⚙️ Configuration

Once you run the game with the mod installed, a config file will be generated at `BepInEx/config/com.oyasumi.infinitestack.cfg`. 

You can edit this file directly or use the **BepInEx Configuration Manager** in-game (usually by pressing `F1`) to tweak the settings:
* **Enable Stack Mod:** (True/False) Toggle the mod on or off.
* **Max Stack Size:** (1 - 9999) Define your desired maximum stack limit.

## 🛠️ Building from Source

If you want to compile this mod yourself:
1. Clone this repository.
2. Open the solution in Visual Studio.
3. Add the required references from your game's `Graveyard Keeper_Data/Managed` folder:
   * `Assembly-CSharp.dll`
   * `UnityEngine.dll`
   * `UnityEngine.CoreModule.dll`
4. Add the BepInEx and Harmony references from your `BepInEx/core` folder.
5. Build the project (Release mode recommended).

## 📄 License

This project is open-source and free to use. Feel free to fork, modify, and improve!
