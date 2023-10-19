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

    [HarmonyPatch(typeof(DrillMinigame))]
    class DrillMinigamePatch
    {
        [HarmonyPatch(nameof(DrillMinigame.Begin))]
        [HarmonyPostfix]
        private static void AwakePostfix(DrillMinigame __instance)
        {
            if (CursedTasker.cursedTasker.FindAll(x => x.PlayerId == CachedPlayer.LocalPlayer.PlayerId).Count() > 0)
            {
                __instance.MaxState = 5; 
            }

            if (EasyTasker.easyTasker.FindAll(x => x.PlayerId == CachedPlayer.LocalPlayer.PlayerId).Count() > 0)
            {
                __instance.MaxState = 3;

            }
        }
    }

}
