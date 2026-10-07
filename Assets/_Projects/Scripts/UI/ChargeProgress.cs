using UnityEngine;
using UnityEngine.UIElements;
using CustomUI;

public class ChargeProgress : MonoBehaviour
{
    [SerializeField] UIDocument gameUI;
    [SerializeField] PlayerWeapon playerWeapon;
    RadialProgressIndicator radialProgress;
    bool hasCharged = false;

    void Awake()
    {
        var root = gameUI.rootVisualElement;
        radialProgress = root.Q<RadialProgressIndicator>();
    }

    public void UpdateProgress()
    {
        if (playerWeapon.WeaponContext.crosshair == null ||
            !playerWeapon.WeaponContext.hasCharge)
        {
            return;
        }

        radialProgress.Progress = playerWeapon.ChargeRatio;

        if (radialProgress.Progress >= 1)
        {
            radialProgress.GaugeColor = Color.cyan;
            if (!hasCharged)
            {
                AudioPlayer.Instance.PlaySE("Fullcharge");
            }
            hasCharged = true;
        }
        else if (playerWeapon.FireState != FireState.Firing)
        {
            radialProgress.GaugeColor = Color.white;
            hasCharged = false;
        }
    }
}
