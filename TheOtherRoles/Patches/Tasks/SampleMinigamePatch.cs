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


    [HarmonyPatch(typeof(SampleMinigame))]
    class sdfdsf
    {
        [HarmonyPatch(nameof(SampleMinigame.Begin))]
        [HarmonyPostfix]
        private static void AwakePostfix(SampleMinigame __instance)
        {           
            if (CursedTasker.cursedTasker.FindAll(x => x.PlayerId == CachedPlayer.LocalPlayer.PlayerId).Count() > 0)
            {
                __instance.TimePerStep = 260f;
            }

            if (EasyTasker.easyTasker.FindAll(x => x.PlayerId == CachedPlayer.LocalPlayer.PlayerId).Count() > 0)
            {
                __instance.TimePerStep = 30f;
            }
        }
    }

}