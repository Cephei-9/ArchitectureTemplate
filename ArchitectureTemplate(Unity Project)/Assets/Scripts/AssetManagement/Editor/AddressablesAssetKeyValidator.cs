using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace ArchitectureTemplate.AssetManagement.Editor
{
    /// <summary>
    /// Editor-only validator: ensures every AssetKey has a matching Addressables address.
    /// Run manually from menu or automatically before build.
    /// </summary>
    [InitializeOnLoad]
    public static class AddressablesAssetKeyValidator
    {
        private const string MenuPath = "Tools/Addressables/Validate AssetKey addresses";

        static AddressablesAssetKeyValidator()
        {
            BuildPlayerWindow.RegisterBuildPlayerHandler(OnBuildPlayer);
        }

        [MenuItem(MenuPath)]
        public static void ValidateFromMenu()
        {
            bool ok = Validate(out string report);
            if (ok)
            {
                Debug.Log("[AddressablesAssetKeyValidator] OK\n" + report);
            }
            else
            {
                Debug.LogError("[AddressablesAssetKeyValidator] FAILED\n" + report);
            }
        }

        private static void OnBuildPlayer(BuildPlayerOptions options)
        {
            bool ok = Validate(out string report);
            
            if (!ok)
            {
                Debug.LogError("[AddressablesAssetKeyValidator] Build cancelled.\n" + report);
                return;
            }

            BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(options);
        }

        private static bool Validate(out string report)
        {
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            
            if (settings == null)
            {
                report = "AddressableAssetSettings not found (Window -> Asset Management -> Addressables -> Groups).";
                return false;
            }

            HashSet<string> addresses = new();
            
            foreach (AddressableAssetGroup group in settings.groups)
            {
                if (group == null)
                    continue;

                foreach (AddressableAssetEntry entry in group.entries)
                {
                    if (!string.IsNullOrEmpty(entry.address))
                    {
                        addresses.Add(entry.address);
                    }
                }
            }

            List<string> missing = new();
            foreach (AssetKey key in Enum.GetValues(typeof(AssetKey)))
            {
                string address = key.ToString();
                if (!addresses.Contains(address))
                {
                    missing.Add(address);
                }
            }

            if (missing.Count == 0)
            {
                report = "All AssetKey values are present as Addressables addresses.";
                return true;
            }

            report =
                "Missing Addressables entries for AssetKey:\n" +
                string.Join("\n", missing.Select(x => " - " + x)) +
                "\n\nFix: assign Addressables Address exactly equal to enum name.";
            return false;
        }
    }
}
