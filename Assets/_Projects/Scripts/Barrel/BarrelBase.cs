using UnityEngine;

public abstract class BarrelBase : MonoBehaviour
{
    [field:SerializeField] public BarrelSO BarrelData { get; private set; }
    [field:SerializeField] public Transform MuzzleTransform { get; private set; }
    public FireState FireState { get; protected set; }
    WeaponContext weaponContext;
    Transform muzzle;
    float prevFiredTime = float.MinValue;   // ゲーム開始直後、prevFiredTimeは最小値に設定
    float fireInterval;

    public void UpdateContext(WeaponContext newContext, Transform muzzleTransform)
    {
        weaponContext = newContext;

        // fireRate発/秒になるように射撃の間隔を計算
        fireInterval = 1 / Mathf.Max(weaponContext.fireRate, float.Epsilon);

        muzzle = muzzleTransform;
    }

    public virtual void UpdateFire()
    {
        if (FireState == FireState.Firing)
        {
            TryFire();
        }
    }

    protected void TryFire()
    {
        // 前回の射撃からの経過時間を計算
        float elapsedTime = Time.time - prevFiredTime;

        // 経過時間が射撃間隔以上であれば、射撃
        if (elapsedTime >= fireInterval)
        {
            // 射撃処理を呼び出し、時間を更新
            Fire();
            prevFiredTime = Time.time;

            // セミオートであれば、待機状態にステート切替
            if (BarrelData.IsSemiAuto)
            {
                FireState = FireState.Idle;
            }
        }
    }

    protected void ShootBullet(Quaternion rotation)
    {
        Quaternion bulletRotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
        Bullet bullet = Instantiate(BarrelData.BulletPrefab, muzzle.position, bulletRotation);
        bullet.InitializeBullet(
            weaponContext.bulletSpeed,
            weaponContext.maxRange,
            weaponContext.damageAmount,
            weaponContext.moduleEffect
        );
    }

    public virtual void OnFirePerformed()
    {
        FireState = FireState.Firing;
    }

    public virtual void OnFireCanceled()
    {
        FireState = FireState.Idle;
    }

    public virtual float ChargeRatio => 0;
    protected abstract void Fire();
}

public enum FireState
{
    Idle,
    Firing,
    Charging
}