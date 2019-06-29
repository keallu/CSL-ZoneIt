using ColossalFramework.UI;
using System;
using UnityEngine;

namespace ZoneIt
{
    public class ModManager : MonoBehaviour
    {
        private bool _initialized;

        private UIButton _esc;

        private UIPanel _zonePanel;
        private UIDragHandle _zoneDragHandle;
        private UIPanel _zoneInnerPanel;
        private UIButton[] _zoneButtons;
        private UISlider _zoneRowsSlider;
        private UILabel _zoneRowsLabel;
        private UICheckBox _zoneAnarchyCheckBox;

        public void Awake()
        {
            try
            {
                if (_esc == null)
                {
                    _esc = GameObject.Find("Esc").GetComponent<UIButton>();
                    ModProperties.Instance.PanelDefaultPositionX = _esc.absolutePosition.x - 550f;
                    ModProperties.Instance.PanelDefaultPositionY = _esc.absolutePosition.y;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Zone It!] ModManager:Awake -> Exception: " + e.Message);
            }
        }

        public void Start()
        {
            try
            {
                if (ModConfig.Instance.PanelPositionX == 0.0f)
                {
                    ModConfig.Instance.PanelPositionX = ModProperties.Instance.PanelDefaultPositionX;
                }
                if (ModConfig.Instance.PanelPositionY == 0.0f)
                {
                    ModConfig.Instance.PanelPositionY = ModProperties.Instance.PanelDefaultPositionY;
                }

                _zoneButtons = new UIButton[5];

                CreateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Zone It!] ModManager:Start -> Exception: " + e.Message);
            }
        }

        public void OnDestroy()
        {
            try
            {
                if (_zoneAnarchyCheckBox != null)
                {
                    Destroy(_zoneAnarchyCheckBox);
                }
                if (_zoneRowsLabel != null)
                {
                    Destroy(_zoneRowsLabel);
                }
                if (_zoneRowsSlider != null)
                {
                    Destroy(_zoneRowsSlider);
                }
                foreach (UIButton button in _zoneButtons)
                {
                    Destroy(button);
                }
                if (_zoneInnerPanel != null)
                {
                    Destroy(_zoneInnerPanel);
                }
                if (_zoneDragHandle != null)
                {
                    Destroy(_zoneDragHandle);
                }
                if (_zonePanel != null)
                {
                    Destroy(_zonePanel);
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Zone It!] ModManager:OnDestroy -> Exception: " + e.Message);
            }
        }

        public void Update()
        {
            try
            {
                if (!_initialized || ModConfig.Instance.ConfigUpdated)
                {
                    UpdateUI();

                    _initialized = true;
                    ModConfig.Instance.ConfigUpdated = false;
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Zone It!] ModManager:Update -> Exception: " + e.Message);
            }
        }

        private void CreateUI()
        {
            try
            {
                _zonePanel = UIUtils.CreatePanel("ZoneItZonePanel");
                _zonePanel.zOrder = 0;
                _zonePanel.backgroundSprite = "GenericPanelLight";
                _zonePanel.color = new Color32(96, 96, 96, 255);
                _zonePanel.size = new Vector2(206f, 106f);
                _zonePanel.isVisible = false;

                _zoneDragHandle = UIUtils.CreateDragHandle(_zonePanel, "ZoneDragHandle");
                _zoneDragHandle.size = new Vector2(_zoneDragHandle.parent.width, _zoneDragHandle.parent.height);
                _zoneDragHandle.relativePosition = new Vector3(0f, 0f);
                _zoneDragHandle.eventMouseUp += (component, eventParam) =>
                {
                    ModConfig.Instance.PanelPositionX = _zonePanel.absolutePosition.x;
                    ModConfig.Instance.PanelPositionY = _zonePanel.absolutePosition.y;
                    ModConfig.Instance.Save();
                };

                _zoneInnerPanel = UIUtils.CreatePanel(_zonePanel, "ZoneInnerPanel");
                _zoneInnerPanel.backgroundSprite = "GenericPanelLight";
                _zoneInnerPanel.color = new Color32(206, 206, 206, 255);
                _zoneInnerPanel.size = new Vector2(_zoneInnerPanel.parent.width - 16f, 66f);
                _zoneInnerPanel.relativePosition = new Vector3(8f, 8f);

                for (int i = 0; i < 5; i++)
                {
                    UIButton button = UIUtils.CreateButton(_zoneInnerPanel, "Button" + (i), (i).ToString());
                    button.objectUserData = i;
                    button.tooltip = $"Zone radius: {i}";
                    button.relativePosition = new Vector3(5f + i * 36f, 5f);
                    button.eventClick += (component, eventParam) =>
                    {
                        if (!eventParam.used)
                        {
                            ModConfig.Instance.Cells = (int)button.objectUserData;
                            ModConfig.Instance.Save();

                            UpdateButtons(ModConfig.Instance.Cells);

                            eventParam.Use();
                        }
                    };

                    _zoneButtons[i] = button;
                }

                _zoneRowsSlider = UIUtils.CreateSlider(_zoneInnerPanel, "RowsSlider", -1, 8, ModConfig.Instance.Rows);
                _zoneRowsSlider.tooltip = "Force number of rows in zone blocks";
                _zoneRowsSlider.size = new Vector2(130f, 8f);
                _zoneRowsSlider.relativePosition = new Vector3(15f, 48f);
                _zoneRowsSlider.eventValueChanged += (component, value) =>
                {
                    if (_zoneRowsLabel != null)
                    {
                        ModConfig.Instance.Rows = (int)value;
                        ModConfig.Instance.Save();

                        _zoneRowsLabel.text = value == -1 ? "Off" : value.ToString();
                    }
                };

                _zoneRowsLabel = UIUtils.CreateLabel(_zoneInnerPanel, "RowsLabel", ModConfig.Instance.Rows == -1 ? "Off" : ModConfig.Instance.Rows.ToString());
                _zoneRowsLabel.textAlignment = UIHorizontalAlignment.Right;
                _zoneRowsLabel.verticalAlignment = UIVerticalAlignment.Top;
                _zoneRowsLabel.textColor = new Color32(185, 221, 254, 255);
                _zoneRowsLabel.textScale = 0.7058824f;
                _zoneRowsLabel.autoSize = false;
                _zoneRowsLabel.size = new Vector2(30f, 16f);
                _zoneRowsLabel.relativePosition = new Vector3(150f, 48f);

                _zoneAnarchyCheckBox = UIUtils.CreateCheckBox(_zonePanel, "AnarchyCheckBox", "Zone Anarchy", ModConfig.Instance.Anarchy);
                _zoneAnarchyCheckBox.tooltip = "Enable Zone Anarchy to avoid any update of zone blocks";
                _zoneAnarchyCheckBox.size = new Vector2(_zoneAnarchyCheckBox.parent.width - 16f, 16f);
                _zoneAnarchyCheckBox.relativePosition = new Vector3(8f, 82f);
                _zoneAnarchyCheckBox.eventCheckChanged += (component, value) =>
                {
                    ModConfig.Instance.Anarchy = value;
                    ModConfig.Instance.Save();
                };

                UpdateUI();
            }
            catch (Exception e)
            {
                Debug.Log("[Zone It!] ModManager:CreateUI -> Exception: " + e.Message);
            }
        }

        private void UpdateUI()
        {
            try
            {
                _zonePanel.absolutePosition = new Vector3(ModConfig.Instance.PanelPositionX, ModConfig.Instance.PanelPositionY);
                _zonePanel.isVisible = ModConfig.Instance.ShowZonePanel;

                UpdateButtons(ModConfig.Instance.Cells);
            }
            catch (Exception e)
            {
                Debug.Log("[Zone It!] ModManager:UpdateUI -> Exception: " + e.Message);
            }
        }

        private void UpdateButtons(int selected)
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    if ((i) == selected)
                    {
                        _zoneButtons[i].normalBgSprite = "OptionBaseFocused";
                    }
                    else
                    {
                        _zoneButtons[i].normalBgSprite = "OptionBase";
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log("[Zone It!] ModManager:UpdateButtons -> Exception: " + e.Message);
            }
        }
    }
}
