using UnityEngine;
using UnityEngine.UIElements;

public class CrosshairMovement : MonoBehaviour
{
    [SerializeField] UIDocument gameUI;
    [SerializeField] PlayerAim playerAim;
    VisualElement crosshair;

    void Awake()
    {
        var root = gameUI.rootVisualElement;
        crosshair = root.Q<VisualElement>();
    }

    public void UpdateMovement()
    {
        // UIの位置を更新
        crosshair.style.left = playerAim.CurrentPosition.x;
        crosshair.style.bottom = playerAim.CurrentPosition.y;
    }
}
