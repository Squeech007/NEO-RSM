using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program
    {
        // LCDs -----------------------------------------------------------------

        private void iterateLcds()
        {
            foreach (IMyTextPanel Lcd in _allLcds)
            {
                Lcd.Enabled = true;
            }
        }

        private void initLcds()
        {
            // RSM LCDs -----------------------------------------------------------------

            foreach (RsmLcd lcd in _rsmLcds)
            {


                lcd.Block.Font = "Monospace";
                lcd.Block.ContentType = ContentType.TEXT_AND_IMAGE;

                if (lcd.Block.CustomName.Contains("HUD1"))
                {
                    lcd.ShowHeader = true;
                    lcd.ShowHeaderOverlay = false;
                    lcd.ShowWarnings = false;
                    lcd.ShowPowerAndTanks = false;
                    lcd.ShowInventory = false;
                    lcd.ShowThrust = false;
                    lcd.ShowIntegrity = false;
                    lcd.ShowAdvancedThrust = false;

                    sortRsmLcd(lcd, "hudlcd:-0.55:0.99:0.7::background:range=750");
                    continue;
                }

                if (lcd.Block.CustomName.Contains("HUD2"))
                {
                    lcd.ShowHeader = false;
                    lcd.ShowHeaderOverlay = false;
                    lcd.ShowWarnings = true;
                    lcd.ShowPowerAndTanks = false;
                    lcd.ShowInventory = false;
                    lcd.ShowThrust = false;
                    lcd.ShowIntegrity = false;
                    lcd.ShowAdvancedThrust = false;

                    sortRsmLcd(lcd, "hudlcd:0.12:0.99:0.6::background:range=750");
                    continue;
                }

                if (lcd.Block.CustomName.Contains("HUD3"))
                {
                    lcd.ShowHeader = false;
                    lcd.ShowHeaderOverlay = false;
                    lcd.ShowWarnings = false;
                    lcd.ShowPowerAndTanks = true;
                    lcd.ShowInventory = false;
                    lcd.ShowThrust = false;
                    lcd.ShowIntegrity = false;
                    lcd.ShowAdvancedThrust = false;

                    sortRsmLcd(lcd, "hudlcd:0.4:0.99:0.6::background:range=750");
                    continue;
                }

                if (lcd.Block.CustomName.Contains("HUD4"))
                {
                    lcd.ShowHeader = false;
                    lcd.ShowHeaderOverlay = false;
                    lcd.ShowWarnings = false;
                    lcd.ShowPowerAndTanks = false;
                    lcd.ShowInventory = false;
                    lcd.ShowThrust = false;
                    lcd.ShowIntegrity = true;
                    lcd.ShowAdvancedThrust = false;

                    sortRsmLcd(lcd, "hudlcd:0.73:0.59:0.6::background:range=750");
                    continue;
                }

                if (lcd.Block.CustomName.Contains("HUD5"))
                {
                    lcd.ShowHeader = false;
                    lcd.ShowHeaderOverlay = false;
                    lcd.ShowWarnings = false;
                    lcd.ShowPowerAndTanks = false;
                    lcd.ShowInventory = true;
                    lcd.ShowThrust = false;
                    lcd.ShowIntegrity = false;
                    lcd.ShowAdvancedThrust = true;

                    sortRsmLcd(lcd, "hudlcd:0.73:0.2:.6::background:range=750");
                    continue;
                }

                if (lcd.Block.CustomName.Contains("HUD6"))
                {
                    lcd.ShowHeader = false;
                    lcd.ShowHeaderOverlay = true;
                    lcd.ShowWarnings = false;
                    lcd.ShowPowerAndTanks = false;
                    lcd.ShowInventory = false;
                    lcd.ShowThrust = false;
                    lcd.ShowIntegrity = false;
                    lcd.ShowAdvancedThrust = false;

                    sortRsmLcd(lcd, "hudlcd:-0.55:0.99:0.7:::range=750");
                    continue;
                }
            }


            // All other LCDs -----------------------------------------------------------------
            bool NavigationLCDFound = false;

            foreach (IMyTextPanel Lcd in _allLcds)
            {
                if (Lcd == null) continue;

                // EFC/NavOS Navigation LCDs -----------------------------------------------------------------

                if (
                    !NavigationLCDFound
                    &&
                    (Lcd.CustomName.Contains(_keywordLcdEfc)
                    ||
                    Lcd.CustomName.ToUpper().Contains(_keywordLcdNavOs))
                    )
                {
                    NavigationLCDFound = true;
                    Lcd.CustomData = "hudlcd:-0.52:-0.7:0.52";
                    continue;
                }
            }
        }
    }
}
