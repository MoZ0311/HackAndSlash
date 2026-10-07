using UnityEngine;

/// <summary>
/// プレイヤーの移動を制御するクラス
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float moveSpeed;

    [Header("Components")]
    [SerializeField] Rigidbody playerRigidbody;

    Vector3 playerVelocity;

    public void HandleMovement(Vector2 inputAxis)
    {
        Vector3 right = Vector3.right * inputAxis.x;
        Vector3 forward = Vector3.forward * inputAxis.y;
        Vector3 direction = right + forward;

        direction.y = 0.0f;

        playerVelocity = direction.normalized * moveSpeed;
    }

    public void ApplyMovement()
    {
        Vector3 newVelocity = new(playerVelocity.x, playerRigidbody.linearVelocity.y, playerVelocity.z);
        playerRigidbody.linearVelocity = newVelocity;
    }
}
