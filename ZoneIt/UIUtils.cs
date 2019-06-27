using ColossalFramework.UI;
using UnityEngine;

namespace ZoneIt
{
    public class UIUtils
    {
        public static UIPanel CreatePanel(string name)
        {
            UIPanel panel = UIView.GetAView().AddUIComponent(typeof(UIPanel)) as UIPanel;
            panel.name = name;

            return panel;
        }

        public static UIPanel CreatePanel(UIComponent parent, string name)
        {
            UIPanel panel = parent.AddUIComponent<UIPanel>();
            panel.name = name;

            return panel;
        }

        public static UIDragHandle CreateDragHandle(UIComponent parent, string name)
        {
            UIDragHandle dragHandle = parent.AddUIComponent<UIDragHandle>();
            dragHandle.name = name;
            dragHandle.target = parent;

            return dragHandle;
        }

        public static UILabel CreateLabel(UIComponent parent, string name, string text)
        {
            UILabel label = parent.AddUIComponent<UILabel>();
            label.name = name;
            label.text = text;

            return label;
        }

        public static UICheckBox CreateCheckBox(UIComponent parent, string name, string text, bool state)
        {
            UICheckBox checkBox = parent.AddUIComponent<UICheckBox>();
            checkBox.name = name;

            UISprite uncheckedSprite = checkBox.AddUIComponent<UISprite>();
            uncheckedSprite.spriteName = "check-unchecked";
            uncheckedSprite.size = new Vector2(16f, 16f);
            uncheckedSprite.relativePosition = Vector3.zero;

            UISprite checkedSprite = checkBox.AddUIComponent<UISprite>();
            checkedSprite.spriteName = "check-checked";
            checkedSprite.size = new Vector2(16f, 16f);
            checkedSprite.relativePosition = Vector3.zero;
            checkBox.checkedBoxObject = checkedSprite;

            checkBox.label = checkBox.AddUIComponent<UILabel>();
            checkBox.label.text = text;
            checkBox.label.autoSize = false;
            checkBox.label.height = 20f;
            checkBox.label.verticalAlignment = UIVerticalAlignment.Middle;
            checkBox.label.textColor = new Color32(185, 221, 254, 255);
            checkBox.label.textScale = 0.8125f;
            checkBox.label.relativePosition = new Vector3(20f, 0f);

            checkBox.isChecked = state;

            return checkBox;
        }

        public static UIButton CreateButton(UIComponent parent, string name, string text)
        {
            UIButton button = parent.AddUIComponent<UIButton>();
            button.name = name;
            button.text = text;
            button.textHorizontalAlignment = UIHorizontalAlignment.Center;
            button.textVerticalAlignment = UIVerticalAlignment.Middle;
            button.relativePosition = new Vector3(0f, 0f);

            button.normalBgSprite = "OptionBase";
            button.hoveredBgSprite = "OptionBaseHovered";
            button.pressedBgSprite = "OptionBasePressed";
            button.disabledBgSprite = "OptionBaseDisabled";

            return button;
        }
    }
}
