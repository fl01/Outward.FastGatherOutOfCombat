using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace Outward.FastGatherOutOfCombat
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class FastGatherOutOfCombat : BaseUnityPlugin
    {
        public const string GUID = "fl01.fast-gather-out-of-combat";
        public const string NAME = "Fast gather out of combat";
        public const string VERSION = "1.0.0";
        public static ManualLogSource Log;
        internal void Awake()
        {
            new Harmony(GUID).PatchAll();
        }

        [HarmonyPatch(typeof(Character), "OnInteractButtonDown", MethodType.Normal)]
        public class Character_OnInteractButtonDown
        {
            [HarmonyPrefix]
            public static bool Prefix(Character __instance)
            {
                if (!__instance.InCombat && __instance.Interactable?.ParentItem is Gatherable or CentralGatherable)
                {
                    __instance.Interactable.TryActivateHoldAction(__instance);
                    return false;
                }

                return true;
            }
        }
    }
}
