using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using System.Collections.Generic;

namespace OyasumiStackMod
{
    [BepInPlugin("com.oyasumi.infinitestack", "Oyasumi Infinite Stack", "1.3.0")]
    public class Plugin : BaseUnityPlugin
    {
        // Config entries for the mod settings
        public static ConfigEntry<bool> ModEnabled;
        public static ConfigEntry<int> MaxStackSize;

        // Stores the original stack sizes so we can restore them when the mod is disabled
        private static Dictionary<string, int> originalStacks = new Dictionary<string, int>();

        private void Awake()
        {
            // Register settings in the Configuration Manager (F1 menu)
            ModEnabled = Config.Bind("1. General", "Enable Stack Mod", true, "Enable or disable the stack mod");
            MaxStackSize = Config.Bind("2. Settings", "Max Stack Size", 999,
                new ConfigDescription("Set the maximum stack size for all stackable items", new AcceptableValueRange<int>(1, 9999)));

            // Automatically apply changes whenever settings are changed in the config menu
            ModEnabled.SettingChanged += (sender, args) => ApplyStackSize();
            MaxStackSize.SettingChanged += (sender, args) => ApplyStackSize();

            var harmony = new Harmony("com.oyasumi.infinitestack");
            harmony.PatchAll();

            Debug.Log(">>> [Oyasumi] Infinite Stack v1.3.0 LOADED! <<<");
        }

        public static void ApplyStackSize()
        {
            // Safety check: make sure game data is loaded before applying
            if (GameBalance.me?.items_data == null) return;

            int count = 0;
            foreach (var item in GameBalance.me.items_data)
            {
                if (item == null || string.IsNullOrEmpty(item.id)) continue;

                // Save the original stack size the first time we see this item
                if (!originalStacks.ContainsKey(item.id))
                    originalStacks[item.id] = item.stack_count;

                // Only modify items that are naturally stackable (stack > 1)
                if (originalStacks[item.id] > 1)
                {
                    // If mod is enabled, apply custom stack size. Otherwise restore original value.
                    item.stack_count = ModEnabled.Value ? MaxStackSize.Value : originalStacks[item.id];
                    count++;
                }
            }
            Debug.Log($">>> [Oyasumi] STACK UPDATE! Enabled: {ModEnabled.Value} | Size: {MaxStackSize.Value} | Items: {count} <<<");
        }
    }

    // Patch MainGame to apply stack sizes right after the game finishes loading a save
    [HarmonyPatch(typeof(MainGame), "OnGameStartedPlaying")]
    public static class MainGamePatch
    {
        [HarmonyPostfix]
        static void Postfix()
        {
            Plugin.ApplyStackSize();
        }
    }
}
