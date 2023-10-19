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

    [HarmonyPatch(typeof(SimonSaysGame))]
    class SimonSaysGamePatch
    {
        [HarmonyPatch(nameof(SimonSaysGame.Begin))]
        [HarmonyPostfix]
        private static void AwakePostfix(SimonSaysGame __instance)
        {
            if (CursedTasker.cursedTasker.FindAll(x => x.PlayerId == CachedPlayer.LocalPlayer.PlayerId).Count() > 0)
            {
                __instance.flashTime = 0.05f;
                __instance.userButtonFlashTime = 0.2f;
            }

            if (EasyTasker.easyTasker.FindAll(x => x.PlayerId == CachedPlayer.LocalPlayer.PlayerId).Count() > 0)
            {
                __instance.flashTime = 0.2f;
                __instance.userButtonFlashTime = 0.05f;
            }
        }
    }

}
