using UnityEngine;
using UnityEngine.InputSystem;

public class GameUIManager : MonoBehaviour, PlayerInputActions.IPlayerActions
{
    [SerializeField] CrosshairMovement crosshairMovement;
    [SerializeField] CrosshairVisual crosshairVisual;
    [SerializeField] ChargeProgress chargeProgress;

    PlayerInputActions playerInput;
    PlayerInputActions.PlayerActions playerActions;

    void Awake()
    {
        playerInput = new();
        playerActions = playerInput.Player;
        playerActions.AddCallbacks(this);

        Cursor.visible = false;
    }

    void OnEnable()
    {
        playerActions.Enable();
    }

    void OnDisable()
    {
        playerActions.Disable();
    }

    void OnDestroy()
    {
        playerInput.Dispose();
    }

    void Update()
    {
        crosshairMovement.UpdateMovement();
        crosshairVisual.UpdateVisual();
        chargeProgress.UpdateProgress();
    }

    public void OnMove(InputAction.CallbackContext context) {}
    public void OnAim(InputAction.CallbackContext context) {}
    public void OnFire(InputAction.CallbackContext context) {}
    public void OnRolling(InputAction.CallbackContext context) {}
}
