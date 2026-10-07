using UnityEngine;
using System.Collections.Generic;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float dampTime;

    const string MoveX = "moveX";
    const string MoveZ = "moveZ";

    const string Handgun = "Handgun";
    const string Rifle = "Rifle";
    const string Shotgun = "Shotgun";
    const string Gatling = "Gatling";

    public void HandleAnimation(Vector2 inputAxis)
    {
        // 入力をプレイヤーの向きで補正
        Vector3 inputDirection = new(inputAxis.x, 0, inputAxis.y);
        Vector3 localDirection = transform.InverseTransformDirection(inputDirection).normalized;

        // さらにdampTimeで値の変動を緩やかに補正
        animator.SetFloat(MoveX, localDirection.x, dampTime, Time.deltaTime);
        animator.SetFloat(MoveZ, localDirection.z, dampTime, Time.deltaTime);
    }

    public void OnDead()
    {
        animator.SetTrigger("hasDead");
    }
}
