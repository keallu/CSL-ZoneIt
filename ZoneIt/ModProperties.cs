using System;
using UnityEngine;

namespace ZoneIt
{
    public class ModProperties
    {
        public float PanelDefaultPositionX;
        public float PanelDefaultPositionY;

        private static ModProperties instance;

        public static ModProperties Instance
        {
            get
            {
                return instance ?? (instance = new ModProperties());
            }
        }

        public void ResetPanelPosition()
        {
            try
            {
                ModConfig.Instance.PanelPositionX = PanelDefaultPositionX;
                ModConfig.Instance.PanelPositionY = PanelDefaultPositionY;
                ModConfig.Instance.Save();
            }
            catch (Exception e)
            {
                Debug.Log("[Zone It!] ModProperties:ResetPanelPosition -> Exception: " + e.Message);
            }
        }
    }
}
