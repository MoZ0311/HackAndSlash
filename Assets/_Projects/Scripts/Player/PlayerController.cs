using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーの各種挙動をまとめて管理するクラス
/// </summary>
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerAim))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerRotation))]
[RequireComponent(typeof(PlayerWeapon))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerController : MonoBehaviour, PlayerInputActions.IPlayerActions
{
    [Header("Scripts")]
    [SerializeField] PlayerAnimation playerAnimation;
    [SerializeField] PlayerAim playerAim;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerRotation playerRotation;
    [SerializeField] PlayerWeapon playerWeapon;
    [SerializeField] PlayerHealth playerHealth;

    Vector2 inputAxis;

    PlayerInputActions playerInput;
    PlayerInputActions.PlayerActions playerActions;

    void Awake()
    {
        playerInput = new();
        playerActions = playerInput.Player;
        playerActions.AddCallbacks(this);
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
        if (!playerHealth.CanControl)
        {
            return;
        }
        // 照準をもとに、狙った位置を算出
        playerAim.HandleAim(playerWeapon.WeaponContext.aimSmoothTime);

        // 狙った方向をもとに、回転を適用
        if (playerAim.AimDirection != Vector3.zero)
        {
            playerRotation.HandleRotation(playerAim.AimDirection);
        }

        // プレイヤーの射撃を制御
        playerWeapon.HandleFiring();

        // プレイヤーのアニメーションを制御
        playerAnimation.HandleAnimation(inputAxis);
    }

    void FixedUpdate()
    {
        if (!playerHealth.CanControl)
        {
            return;
        }
        // 移動速度をRigidbodyに適用
        playerMovement.ApplyMovement();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // 入力をもとにプレイヤーの速度を算出
        inputAxis = context.ReadValue<Vector2>();
        playerMovement.HandleMovement(inputAxis);
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        // 入力をもとにマウス座標をClampして渡す
        Vector2 mousePosition = context.ReadValue<Vector2>();
        mousePosition.x = Mathf.Clamp(mousePosition.x, 0, Screen.width);
        mousePosition.y = Mathf.Clamp(mousePosition.y, 0, Screen.height);
        playerAim.TargetPosition = mousePosition;
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // 入力が完了した(performed)とき
            playerWeapon.OnFirePerformed();
        }

        if (context.canceled)
        {
            // 入力を中断した(canceled)とき
            playerWeapon.OnFireCanceled();
        }
    }

    public void OnRolling(InputAction.CallbackContext context)
    {
        // throw new System.NotImplementedException();
    }
}
