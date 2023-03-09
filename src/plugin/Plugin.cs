using BepInEx;
using UnityEngine;

namespace SmarterStealth
{
    [BepInPlugin("com.coder23848.smarterstealth", PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
#pragma warning disable IDE0051 // Visual Studio is whiny
        private void OnEnable()
#pragma warning restore IDE0051
        {
            // Plugin startup logic
            On.LizardGraphics.Update += LizardGraphics_Update;
            On.LizardGraphics.DrawSprites += LizardGraphics_DrawSprites;
            On.Lizard.Update += Lizard_Update;
        }

        private void Lizard_Update(On.Lizard.orig_Update orig, Lizard self, bool eu)
        {
            float jawOpen = self.jawOpen;

            orig(self, eu);

            if (self.AI.behavior == LizardAI.Behavior.Lurk)
            {
                if (self.abstractCreature.personality.energy < Random.Range(0.8f, 1f)) { self.bubble = 0; } // Prevent lizards from making bubbles when they're trying to stealth
                if (self.abstractCreature.personality.energy < Random.Range(0.8f, 1f)) { self.JawOpen = jawOpen; } // Prevent lizards from moving their mouth when they're trying to stealth
            }
        }
        private void LizardGraphics_DrawSprites(On.LizardGraphics.orig_DrawSprites orig, LizardGraphics self, RoomCamera.SpriteLeaser sLeaser, RoomCamera rCam, float timeStacker, Vector2 camPos)
        {
            Lizard.Animation actualAnimation = self.lizard.animation;
            if (self.lizard.AI.behavior == LizardAI.Behavior.Lurk)
            {
                if (self.lizard.abstractCreature.personality.energy < Random.Range(0.8f, 1f)) { self.lizard.animation = Lizard.Animation.Standard; } // Lizards shake their head when they're in certain animation states. This hook fakes their animation state so that they don't.
            }

            orig(self, sLeaser, rCam, timeStacker, camPos);

            if (self.lizard.AI.behavior == LizardAI.Behavior.Lurk)
            {
                self.lizard.animation = actualAnimation; // Un-fake the lizards' animation state
            }
        }
        private void LizardGraphics_Update(On.LizardGraphics.orig_Update orig, LizardGraphics self)
        {
            Vector2 lookPos = self.lookPos;

            orig(self);

            if (self.lizard.AI.behavior == LizardAI.Behavior.Lurk)
            {
                if (self.lizard.abstractCreature.personality.energy < Random.Range(0.8f, 1f)) { self.lookPos = lookPos; } // Prevent lizards from moving their head when they're trying to stealth
            }
        }
    }
}