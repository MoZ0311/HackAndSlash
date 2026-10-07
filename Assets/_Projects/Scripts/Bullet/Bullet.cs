using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] LayerMask targetLayers;
    [SerializeField][Min(0)] int maxRicochet;

    [Header("Prefabs")]
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] ParticleSystem penetrationEffect;
    [SerializeField] GameObject explosion;

    ModuleEffect moduleEffect;
    float moveSpeed;
    float maxRange;
    float damageAmount;
    float traveledDistance;
    bool hasInitialized;
    int ricochetCount;

    void Start()
    {
        if (!hasInitialized)
        {
            Debug.LogError("Bullet:初期化処理が呼ばれていません");
            Destroy(gameObject);
        }
        AudioPlayer.Instance.PlaySE("Shoot");
    }

    void Update()
    {
        // 1f当たりの飛距離を計算
        float moveDistance = moveSpeed * Time.deltaTime;

        // 累計の飛距離を更新
        traveledDistance += moveDistance;
        if (traveledDistance > maxRange)
        {
            // 射程限界で消滅
            Destroy(gameObject);
        }

        // 1f後の到達位置までRayを飛ばし、衝突を検知
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, moveDistance, targetLayers, QueryTriggerInteraction.Ignore))
        {
            OnCollidedTarget(hitInfo);
        }
        else
        {
            // ぶつからなかったので、進める
            transform.position += moveDistance * transform.forward;
        }
    }

    public void InitializeBullet(float moveSpeed, float maxRange, float damageAmount, ModuleEffect moduleEffect)
    {
        this.moveSpeed = moveSpeed;
        this.maxRange = maxRange;
        this.damageAmount = damageAmount;
        this.moduleEffect = moduleEffect;

        hasInitialized = true;
    }

    void OnCollidedTarget(RaycastHit hit)
    {
        // 対象のレイヤーにぶつかった時、位置を補正
        transform.position = hit.point;

        // TryGetComponentでHP管理スクリプト取得/アクセスを試みる
        if (hit.collider.TryGetComponent(out HealthBase targetHealth))
        {
            targetHealth.TakeDamage(damageAmount);
        }

        switch (moduleEffect)
        {
            case ModuleEffect.Penetration:
                // 貫通エフェクト
                Instantiate(penetrationEffect, transform.position, transform.rotation);
                break;

            case ModuleEffect.Ricochet:
                // 跳弾
                ricochetCount++;
                if (ricochetCount <= maxRicochet)
                {
                    traveledDistance = 0;
                    Vector3 ricochetDirection = Vector3.Reflect(transform.forward, hit.normal);
                    transform.forward = ricochetDirection;
                }
                Instantiate(hitEffect, transform.position, transform.rotation);
                break;

            case ModuleEffect.Explosion:
                // 爆破処理
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
                break;

            case ModuleEffect.None:
            default:
                // モジュール指定なしなら、着弾時に破壊
                Instantiate(hitEffect, transform.position, transform.rotation);
                Destroy(gameObject);
                break;
        }
    }
}
