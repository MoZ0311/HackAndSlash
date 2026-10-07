using UnityEngine;

/// <summary>
/// プレイヤーを入力に応じて回転させるクラス
/// </summary>
public class PlayerRotation : MonoBehaviour
{
    public void HandleRotation(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
