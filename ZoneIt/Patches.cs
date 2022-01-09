using HarmonyLib;
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

    [HarmonyPatch(typeof(ZoneManager), "CreateBlock")]
    public static class ZoneManagerCreateBlockPatch
    {
        static void Prefix(ref int rows)
        {
            try
            {
                if (ModConfig.Instance.Rows != -1)
                {
                    rows = ModConfig.Instance.Rows;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Zone It!] ZoneManagerCreateBlockPatch:Prefix -> Exception: " + e.Message);
            }
        }
    }

    [HarmonyPatch(typeof(ZoneManager), "UpdateBlock")]
    public static class ZoneManagerUpdateBlockPatch
    {
        static bool Prefix()
        {
            try
            {
                return !ModConfig.Instance.Anarchy;
            }
            catch (Exception e)
            {
                Debug.Log("[Zone It!] ZoneManagerUpdateBlockPatch:Prefix -> Exception: " + e.Message);
                return true;
            }
        }
    }

    [HarmonyPatch(typeof(ZoneManager), "UpdateBlocks")]
    public static class ZoneManagerUpdateBlocksPatch
    {
        static bool Prefix()
        {
            try
            {
                return !ModConfig.Instance.Anarchy;
            }
            catch (Exception e)
            {
                Debug.Log("[Zone It!] ZoneManagerUpdateBlocksPatch:Prefix -> Exception: " + e.Message);
                return true;
            }
        }
    }
}
