using HarmonyLib;
using Reactor.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TheOtherRoles.Players;
using UnityEngine;

namespace TheOtherRoles.Patches.Tasks
{
    [HarmonyPatch(typeof(UnlockManifoldsMinigame))]
    internal static class UnlockManifold
    {
        [HarmonyPatch(nameof(UnlockManifoldsMinigame.Begin))]
        [HarmonyPrefix]
        private static void BeginPrefix(UnlockManifoldsMinigame __instance)
        {
            if (CursedTasker.cursedTasker.FindAll(x => x.PlayerId == CachedPlayer.LocalPlayer.PlayerId).Count() > 0)
            {
                int index = 0;
                foreach (SpriteRenderer button in __instance.Buttons)
                {
                    if (index % 3 == 0)
                    {
                        button.sprite = SpriteHelper.CreateSpriteFromResources("UnlockManifold.png");
                    }
                    index++;
                }

            } 
        }
    }

    public static class SpriteHelper
    {
        private static DLoadImage _icallLoadImage;
        private delegate Boolean DLoadImage(IntPtr texture, IntPtr data, Boolean markNonReadable);

        public static Sprite CreateSpriteFromResources(String path)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            String streamPath = assembly.GetManifestResourceNames()
                .FirstOrDefault(x => x.StartsWith(assembly.GetName().Name) && x.EndsWith(path));

            Stream stream = assembly.GetManifestResourceStream(streamPath);
            Byte[] textureBytes = stream.ReadFully();
            Texture2D texture = new(2, 2, TextureFormat.ARGB32, false);

            _icallLoadImage = IL2CPP.ResolveICall<DLoadImage>("UnityEngine.ImageConversion::LoadImage");
            Il2CppStructArray<Byte> il2CPPArray = textureBytes;
            _ = _icallLoadImage.Invoke(texture.Pointer, il2CPPArray.Pointer, false);

            return texture.CreateSprite();
        }
    }
}