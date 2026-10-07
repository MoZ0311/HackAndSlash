using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float dampTime;

    const string MoveX = "moveX";
    const string MoveZ = "moveZ";

    public void HandleAnimation(Vector3 localVelocity)
    {
        animator.SetFloat(MoveX, localVelocity.x, dampTime, Time.deltaTime);
        animator.SetFloat(MoveZ, localVelocity.z, dampTime, Time.deltaTime);
    }

    public void OnDead()
    {
        animator.SetTrigger("hasDead");
    }
}
