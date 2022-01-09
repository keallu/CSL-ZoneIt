using CitiesHarmony.API;
using ICities;

namespace ZoneIt
{
    public class ModInfo : IUserMod
    {
        public string Name => "Zone It!";
        public string Description => "Allows to change zone block layout and also disable zone block updates.";

        public void OnEnabled()
        {
            HarmonyHelper.DoOnHarmonyReady(() => Patcher.PatchAll());
        }

        public void OnDisabled()
        {
            if (HarmonyHelper.IsHarmonyInstalled)
            {
                Patcher.UnpatchAll();
            }
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            UIHelperBase group;
            bool selected;

            group = helper.AddGroup(Name);

            selected = ModConfig.Instance.ShowZonePanel;
            group.AddCheckbox("Show Zone Panel", selected, sel =>
            {
                ModConfig.Instance.ShowZonePanel = sel;
                ModConfig.Instance.Save();
            });

            group.AddSpace(10);

            group.AddButton("Reset Positioning of Zone Panel", () =>
            {
                ModProperties.Instance.ResetPanelPosition();
            });
        }
    }
}