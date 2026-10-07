public class EnemyHealth : HealthBase
{
    protected override void Die()
    {
        if (TryGetComponent(out EnemyAnimation enemyAnimation))
        {
            AudioPlayer.Instance.PlaySE("Hit");
            enemyAnimation.OnDead();
        }
        Invoke(nameof(ReStart), 1.5f);
    }

    protected override void OnDamaged()
    {
        
    }

    void ReStart()
    {
        SceneChanger sceneChanger = FindAnyObjectByType<SceneChanger>();
        sceneChanger.ChangeScene(SceneType.Title);
    }
}
