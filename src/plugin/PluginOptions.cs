using Menu.Remix.MixedUI;
using UnityEngine;

namespace SmarterCritters
{
    public class PluginOptions : OptionInterface
    {
        public static PluginOptions Instance = new();

        public static Configurable<bool> LizardPatience = Instance.config.Bind("LizardPatience", true, new ConfigurableInfo("When enabled, lizards that rely on stealth are more patient."));
        public static Configurable<bool> DropwigPitAvoidance = Instance.config.Bind("DropwigPitAvoidance", true, new ConfigurableInfo("When enabled, dropwigs avoid setting up ambushes over bottomless pits."));
        public static Configurable<bool> LizardsUnderstandSlugcatCombat = Instance.config.Bind("LizardsUnderstandSlugcatCombat", true, new ConfigurableInfo("When enabled, lizards are capable of strategizing around your weapons."));

        public override void Initialize()
        {
            base.Initialize();
            Tabs = new OpTab[1];

            Tabs[0] = new(Instance, "Options");

            CheckBoxOption(LizardPatience, 0, "Patient Lizards");
            CheckBoxOption(DropwigPitAvoidance, 1, "Dropwig Pit Avoidance");
            CheckBoxOption(LizardsUnderstandSlugcatCombat, 2, "Lizards Understand Slugcat Combat");
        }

        private void CheckBoxOption(Configurable<bool> setting, float pos, string label)
        {
            Tabs[0].AddItems(new OpCheckBox(setting, new(50, 550 - pos * 30)) { description = setting.info.description }, new OpLabel(new Vector2(90, 550 - pos * 30), new Vector2(), label, FLabelAlignment.Left));
        }
    }
}