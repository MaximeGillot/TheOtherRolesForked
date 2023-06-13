using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOtherRoles.Players;
using UnityEngine;

namespace TheOtherRoles.Patches.Tasks
{
   
    [HarmonyPatch(typeof(MedScanMinigame))]
    class MedScanMinigamePatch
    {
        [HarmonyPatch(nameof(MedScanMinigame.Begin))]
        [HarmonyPostfix]
        private static void AwakePostfix(MedScanMinigame __instance)
        {                        
            if (CursedTasker.cursedTasker.FindAll(x => x.PlayerId == CachedPlayer.LocalPlayer.PlayerId).Count() > 0)
            {
                __instance.ScanDuration = 15;
            }

            if (EasyTasker.easyTasker.FindAll(x => x.PlayerId == CachedPlayer.LocalPlayer.PlayerId).Count() > 0)
            {
                __instance.ScanDuration = 5;
            }
        }
    }

}
