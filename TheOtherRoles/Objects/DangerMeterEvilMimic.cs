using UnityEngine;

namespace TheOtherRoles.Objects {
    public class DangerMeterEvilMimic
    {
        public static void UpdateProximity(Vector3 position)
        {
            if (!GameManager.Instance.GameHasStarted) return;

            if (EvilMimic.DangerMeterParent == null)
            {
                EvilMimic.DangerMeterParent = GameObject.Instantiate(GameObject.Find("ImpostorDetector"), HudManager.Instance.transform);
                EvilMimic.Meter = EvilMimic.DangerMeterParent.transform.GetChild(0).GetComponent<DangerMeter>();
                EvilMimic.DangerMeterParent.transform.localPosition = new(3.7f, -1.6f, 0);
                var backgroundrend = EvilMimic.DangerMeterParent.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
                backgroundrend.color = backgroundrend.color.SetAlpha(0.5f);
            }
            EvilMimic.DangerMeterParent.SetActive(MeetingHud.Instance == null && LobbyBehaviour.Instance == null && !EvilMimic.evilMimic.Data.IsDead);
            EvilMimic.Meter.gameObject.SetActive(MeetingHud.Instance == null && LobbyBehaviour.Instance == null && !EvilMimic.evilMimic.Data.IsDead);
            if (EvilMimic.evilMimic.Data.IsDead) return;
            if (EvilMimic.DangerMeterParent.transform.localPosition.x != 3.7f) EvilMimic.DangerMeterParent.transform.localPosition = new(3.7f, -1.6f, 0);
            float num = float.MaxValue;
            float dangerLevel1;
            float dangerLevel2;

            float sqrMagnitude = (position - EvilMimic.evilMimic.transform.position).sqrMagnitude;
            if (sqrMagnitude < (55 * GameOptionsManager.Instance.currentNormalGameOptions.PlayerSpeedMod) && num > sqrMagnitude)
            {
                num = sqrMagnitude;
            }

            dangerLevel1 = Mathf.Clamp01((55 - num) / (55 - 15 * GameOptionsManager.Instance.currentNormalGameOptions.PlayerSpeedMod));
            dangerLevel2 = Mathf.Clamp01((15 - num) / (15 * GameOptionsManager.Instance.currentNormalGameOptions.PlayerSpeedMod));

            EvilMimic.Meter.SetDangerValue(dangerLevel1, dangerLevel2);
        }

    }

}