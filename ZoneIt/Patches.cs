using Harmony;
using System;
using UnityEngine;

namespace ZoneIt
{
    [HarmonyPatch(typeof(RoadAI), "GetEffectRadius")]
    public static class RoadAIGetEffectRadiusPatch
    {
        static void Postfix(ref float radius)
        {
            try
            {
                if (radius > 0f)
                {
                    radius = radius - 32f + (32f * 0.25f * ModConfig.Instance.Cells);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Zone It!] RoadAIGetEffectRadiusPatch:Postfix -> Exception: " + e.Message);
            }
        }
    }

    [HarmonyPatch(typeof(RoadAI), "CreateZoneBlocks")]
    public static class RoadAICreateZoneBlocksPatch
    {
        static bool Prefix()
        {
            try
            {
                return ModConfig.Instance.Cells != 0;
            }
            catch (Exception e)
            {
                Debug.Log("[Zone It!] RoadAICreateZoneBlocksPatch:Prefix -> Exception: " + e.Message);
                return true;
            }
        }
    }

    [HarmonyPatch(typeof(ZoneManager), "UpdateBlock")]
    public static class ZoneBlockUpdateBlockPatch
    {
        static bool Prefix()
        {
            try
            {
                return !ModConfig.Instance.Anarchy;
            }
            catch (Exception e)
            {
                Debug.Log("[Zone It!] ZoneBlockUpdateBlockPatch:Prefix -> Exception: " + e.Message);
                return true;
            }
        }
    }

    [HarmonyPatch(typeof(ZoneManager), "UpdateBlocks")]
    public static class ZoneBlockUpdateBlocksPatch
    {
        static bool Prefix()
        {
            try
            {
                return !ModConfig.Instance.Anarchy;
            }
            catch (Exception e)
            {
                Debug.Log("[Zone It!] ZoneBlockUpdateBlocksPatch:Prefix -> Exception: " + e.Message);
                return true;
            }
        }
    }
}
