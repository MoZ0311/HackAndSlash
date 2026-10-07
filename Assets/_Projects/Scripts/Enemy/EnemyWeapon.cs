using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] Transform muzzle;
    [SerializeField] Transform weaponRoot;
    [SerializeField] Transform moduleRoot;

    BarrelBase currentBarrel;
    ModuleBase currentModule;

    public WeaponContext WeaponContext { get; private set; }

    void Start()
    {
        currentBarrel = weaponRoot.GetComponentInChildren<BarrelBase>();
        currentModule = moduleRoot.GetComponentInChildren<ModuleBase>();

        RebuildContext();
    }

    void Update()
    {
        if (currentBarrel == null)
        {
            return;
        }
        currentBarrel.UpdateFire();
    }

     void RebuildContext()
    {
        BarrelSO barrelData = currentBarrel.BarrelData;
        ModuleSO moduleData = currentModule.ModuleData;

        WeaponContext = new WeaponContext{
            aimSmoothTime = barrelData.AimSmoothTime * moduleData.AimSpeedMultiplier,
            spreadAngle = barrelData.SpreadAngle * moduleData.SpreadMultiplier,
            fireRate = barrelData.FireRate * moduleData.RateMultiplier,
            bulletSpeed = barrelData.BulletSpeed * moduleData.SpeedMultiplier,
            maxRange = barrelData.MaxRange * moduleData.RangeMultiplier,
            damageAmount = barrelData.DamageAmount * moduleData.DamageMultiplier,
            hasCharge = true,
            crosshair = currentBarrel.BarrelData.Crosshair,
            moduleEffect = currentModule.ModuleData.ModuleEffect
        };

        currentBarrel.UpdateContext(WeaponContext, muzzle);
    }

    // トリガー引く
    public void PeerformFire()
    {
        if (currentBarrel != null)
        {
            currentBarrel.OnFirePerformed();
        }
    }

    // トリガー離す
    public void CancelFire()
    {
        if (currentBarrel != null)
        {
            currentBarrel.OnFireCanceled();
        }
    }
}
