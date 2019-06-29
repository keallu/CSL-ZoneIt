namespace ZoneIt
{
    [ConfigurationPath("ZoneItConfig.xml")]
    public class ModConfig
    {
        public bool ConfigUpdated { get; set; }
        public bool ShowZonePanel { get; set; } = true;
        public float PanelPositionX { get; set; }
        public float PanelPositionY { get; set; }
        public int Cells { get; set; } = 4;
        public int Rows { get; set; } = -1;
        public bool Anarchy { get; set; } = false;

        private static ModConfig instance;

        public static ModConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Configuration<ModConfig>.Load();
                }

                return instance;
            }
        }

        public void Save()
        {
            Configuration<ModConfig>.Save();
            ConfigUpdated = true;
        }
    }
}