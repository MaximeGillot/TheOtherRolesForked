using HarmonyLib;
using AmongUs.Data;
using AmongUs.Data.Legacy;
using TheOtherRoles.Players;

namespace TheOtherRoles.Patches {

    [HarmonyPatch(typeof(RoomTracker), nameof(RoomTracker.FixedUpdate))]
    public class RoomTrackerPatch
    {
        static bool Prefix(RoomTracker __instance) {
            PlainShipRoom[] array = null;
            if (LobbyBehaviour.Instance)
            {
                PlainShipRoom[] allRooms = LobbyBehaviour.Instance.AllRooms;
                array = allRooms;
            }
            if (ShipStatus.Instance)
            {
                array = ShipStatus.Instance.AllRooms;
            }
            if (array == null)
            {
                return false;
            }
            PlainShipRoom plainShipRoom = null;
            if (__instance.LastRoom)
            {
                int hitCount = __instance.LastRoom.roomArea.OverlapCollider(__instance.filter, __instance.buffer);
                if (RoomTracker.CheckHitsForPlayer(__instance.buffer, hitCount))
                {
                    plainShipRoom = __instance.LastRoom;
                    if( Cloner.cloner && CachedPlayer.LocalPlayer.PlayerControl == Cloner.cloner && Cloner.currentRoom != __instance.LastRoom.name)
                    {                        
                        Cloner.currentRoom = __instance.LastRoom.RoomId.ToString();
                        TheOtherRolesPlugin.Logger.LogInfo("Room tracker current room id:  " + __instance.LastRoom.RoomId.ToString());
                        TheOtherRolesPlugin.Logger.LogInfo("Room tracker current room name:  " + __instance.LastRoom.name);                        
                    }
                }
            }
            if (!plainShipRoom)
            {
                foreach (PlainShipRoom plainShipRoom2 in array)
                {
                    if (plainShipRoom2.roomArea)
                    {
                        int hitCount2 = plainShipRoom2.roomArea.OverlapCollider(__instance.filter, __instance.buffer);
                        if (RoomTracker.CheckHitsForPlayer(__instance.buffer, hitCount2))
                        {
                            plainShipRoom = plainShipRoom2;
                        }
                    }
                }
            }
            if (plainShipRoom)
            {
                if (__instance.LastRoom != plainShipRoom)
                {
                    __instance.LastRoom = plainShipRoom;
                    if (__instance.slideInRoutine != null)
                    {
                        __instance.StopCoroutine(__instance.slideInRoutine);
                    }
                    if (plainShipRoom.RoomId != SystemTypes.Hallway)
                    {
                        __instance.slideInRoutine = __instance.StartCoroutine(__instance.CoSlideIn(plainShipRoom.RoomId));
                        return false;
                    }
                    __instance.slideInRoutine = __instance.StartCoroutine(__instance.SlideOut());
                    return false;
                }
            }
            else if (__instance.LastRoom)
            {
                __instance.LastRoom = null;
                if (__instance.slideInRoutine != null)
                {
                    __instance.StopCoroutine(__instance.slideInRoutine);
                }
                __instance.slideInRoutine = __instance.StartCoroutine(__instance.SlideOut());
            }
            return false;
        }
    }
}