using UnityEngine;

/// <summary>
/// 入力に応じて、プレイヤーの装備する武器を制御するクラス
/// </summary>
public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] Transform muzzle;
    [SerializeField] Transform weaponRoot;
    [SerializeField] Transform moduleRoot;
    [SerializeField] ParticleSystem particle;
    BarrelBase currentBarrel;
    ModuleBase currentModule;

    public WeaponContext WeaponContext { get; private set; }
    public FireState FireState => currentBarrel.FireState;
    public float ChargeRatio => currentBarrel.ChargeRatio;

    void Start()
    {
        currentBarrel = weaponRoot.GetComponentInChildren<BarrelBase>();
        currentModule = moduleRoot.GetComponentInChildren<ModuleBase>();

        RebuildContext();
    }

    public void HandleFiring()
    {
        if (currentBarrel == null)
        {
            return;
        }

        currentBarrel.UpdateFire();
    }

    public void OnFirePerformed()
    {
        if (currentBarrel != null)
        {
            currentBarrel.OnFirePerformed();
        }
    }

    public void OnFireCanceled()
    {
        if (currentBarrel != null)
        {
            currentBarrel.OnFireCanceled();
        }
    }

    public void SetBarrel(BarrelBase barrelBase)
    {
        if (currentBarrel != null)
        {
            Destroy(currentBarrel.gameObject);
        }

        currentBarrel = Instantiate(barrelBase, weaponRoot);

        RebuildContext();

        particle.Play();
    }

    public void SetModule(ModuleBase moduleBase)
    {
        if (currentModule != null)
        {
            Destroy(currentModule.gameObject);
        }

        currentModule = Instantiate(moduleBase, moduleRoot);

        RebuildContext();

        particle.Play();
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
            hasCharge = true,   // 仮でtrue
            crosshair = currentBarrel.BarrelData.Crosshair,
            moduleEffect = currentModule.ModuleData.ModuleEffect
        };

        currentBarrel.UpdateContext(WeaponContext, muzzle);
    }
}

public struct WeaponContext
{
    public float aimSmoothTime;
    public float spreadAngle;
    public float fireRate;
    public float bulletSpeed;
    public float maxRange;
    public float damageAmount;
    public bool hasCharge;
    public Texture2D crosshair;
    public ModuleEffect moduleEffect;
}