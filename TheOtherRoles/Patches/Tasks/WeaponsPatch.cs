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
    [HarmonyPatch(typeof(WeaponsMinigame), nameof(WeaponsMinigame.Begin))]
    class WeaponsMinigameBeginPatch
    {
        public static void Prefix(WeaponsMinigame __instance)
        {
        }
    }

    [HarmonyPatch(typeof(Asteroid))]
    class AsteroidPatch
    {
        [HarmonyPatch(nameof(Asteroid.Reset))]
        [HarmonyPostfix]
        private static void AwakePostfix(Asteroid __instance)
        {
            if (CursedTasker.cursedTasker.FindAll(x => x.PlayerId == CachedPlayer.LocalPlayer.PlayerId).Count() > 0)
            {
                if (__instance.gameObject.GetComponent<Rigidbody2D>()) return;
                Rigidbody2D rigidbody2D = __instance.gameObject.AddComponent<Rigidbody2D>();
                rigidbody2D.gravityScale = 0f;
                FloatRange asteroidMooveSpeed = __instance.MoveSpeed;
                asteroidMooveSpeed.min = asteroidMooveSpeed.max * 2;
                asteroidMooveSpeed.max = asteroidMooveSpeed.max * 3;
                __instance.MoveSpeed = asteroidMooveSpeed;
            }

            if (EasyTasker.easyTasker.FindAll(x => x.PlayerId == CachedPlayer.LocalPlayer.PlayerId).Count() > 0)
            {
                if (__instance.gameObject.GetComponent<Rigidbody2D>()) return;
                Rigidbody2D rigidbody2D = __instance.gameObject.AddComponent<Rigidbody2D>();
                rigidbody2D.gravityScale = 0f;
                FloatRange asteroidMooveSpeed = __instance.MoveSpeed;
                asteroidMooveSpeed.max = asteroidMooveSpeed.min;
                __instance.MoveSpeed = asteroidMooveSpeed;
            }
        }
    }

}
