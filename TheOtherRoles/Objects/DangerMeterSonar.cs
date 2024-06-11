using UnityEngine;

namespace TheOtherRoles.Objects {
    public class DangerMeterSonar
    {
        public static void UpdateProximity(Vector3 position)
        {
            if (!GameManager.Instance.GameHasStarted) return;

            if (Sonar.DangerMeterParent == null)
            {
                Sonar.DangerMeterParent = GameObject.Instantiate(GameObject.Find("ImpostorDetector"), HudManager.Instance.transform);
                Sonar.Meter = Sonar.DangerMeterParent.transform.GetChild(0).GetComponent<DangerMeter>();
                Sonar.DangerMeterParent.transform.localPosition = new(3.7f, -1.6f, 0);
                var backgroundrend = Sonar.DangerMeterParent.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
                backgroundrend.color = backgroundrend.color.SetAlpha(0.5f);
            }
            Sonar.DangerMeterParent.SetActive(MeetingHud.Instance == null && LobbyBehaviour.Instance == null && !Sonar.sonar.Data.IsDead);
            Sonar.Meter.gameObject.SetActive(MeetingHud.Instance == null && LobbyBehaviour.Instance == null && !Sonar.sonar.Data.IsDead);
            if (Sonar.sonar.Data.IsDead) return;
            if (Sonar.DangerMeterParent.transform.localPosition.x != 3.7f) Sonar.DangerMeterParent.transform.localPosition = new(3.7f, -1.6f, 0);
            float num = float.MaxValue;
            float dangerLevel1;
            float dangerLevel2;

            float sqrMagnitude = (position - Sonar.sonar.transform.position).sqrMagnitude;
            if (sqrMagnitude < (55 * GameOptionsManager.Instance.currentNormalGameOptions.PlayerSpeedMod) && num > sqrMagnitude)
            {
                num = sqrMagnitude;
            }

            dangerLevel1 = Mathf.Clamp01((55 - num) / (55 - 15 * GameOptionsManager.Instance.currentNormalGameOptions.PlayerSpeedMod));
            dangerLevel2 = Mathf.Clamp01((15 - num) / (15 * GameOptionsManager.Instance.currentNormalGameOptions.PlayerSpeedMod));

            Sonar.Meter.SetDangerValue(dangerLevel1, dangerLevel2);
        }

    }

}