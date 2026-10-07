using UnityEngine;
using Unity.Cinemachine;

public class PlayerHealth : HealthBase
{
    [SerializeField]CinemachineImpulseSource impulseSource;
    [SerializeField] float force;
    protected override void Die()
    {
        if (TryGetComponent(out PlayerAnimation playerAnimation))
        {
            AudioPlayer.Instance.PlaySE("Hit");
            playerAnimation.OnDead();
        }

        Invoke(nameof(ReStart), 1.5f);
    }

    protected override void OnDamaged()
    {
        impulseSource.GenerateImpulse(force);
    }

    void ReStart()
    {
        SceneChanger sceneChanger = FindAnyObjectByType<SceneChanger>();
        sceneChanger.ChangeScene(SceneType.Game);
    }
}
