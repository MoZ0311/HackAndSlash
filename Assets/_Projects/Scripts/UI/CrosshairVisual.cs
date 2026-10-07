using UnityEngine;
using UnityEngine.UIElements;
using CustomUI;

public class CrosshairVisual : MonoBehaviour
{
    [SerializeField] UIDocument gameUI;
    [SerializeField] PlayerWeapon playerWeapon;
    VisualElement crosshair;
    RadialProgressIndicator radialProgress;
    const string Crosshair = "Crosshair";

    void Awake()
    {
        var root = gameUI.rootVisualElement;
        crosshair = root.Q<VisualElement>(Crosshair);
        radialProgress = root.Q<RadialProgressIndicator>();
    }

    public void UpdateVisual()
    {
        if (playerWeapon.WeaponContext.crosshair == null)
        {
            return;
        }

        // クロスヘアの見た目を変更
        crosshair.style.backgroundImage = playerWeapon.WeaponContext.crosshair;

        // ゲージの表示非表示を切り替え
        if (playerWeapon.WeaponContext.hasCharge)
        {
            radialProgress.style.visibility = Visibility.Visible;
        }
        else
        {
            radialProgress.style.visibility = Visibility.Hidden;
        }
    }
}
