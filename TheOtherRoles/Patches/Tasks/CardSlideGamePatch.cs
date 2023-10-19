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

    [HarmonyPatch(typeof(CardSlideGame))]
    class CardSlideGamePatch
    {   
        [HarmonyPatch(nameof(CardSlideGame.Begin))]
        [HarmonyPostfix]
        
        private static void AwakePostfix(CardSlideGame __instance)
        {
            if (CursedTasker.cursedTasker.FindAll(x => x.PlayerId == CachedPlayer.LocalPlayer.PlayerId).Count() > 0)
            {
                __instance.AcceptedTime = new FloatRange(0.45f, 0.55f);
            }

            if (EasyTasker.easyTasker.FindAll(x => x.PlayerId == CachedPlayer.LocalPlayer.PlayerId).Count() > 0)
            {
                __instance.AcceptedTime = new FloatRange(0.35f, 0.65f);

            }
        }
    }

}
