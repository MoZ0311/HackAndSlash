using UnityEngine;

public class BarrelRifle : BarrelBase
{
    protected override void Fire()
    {
        Quaternion rotation = SpreadUtility.CalculateRandomSpread(BarrelData.SpreadAngle, MuzzleTransform.rotation);
        ShootBullet(rotation);
    }
}
