using UnityEngine;

public class BarrelShotgun : BarrelBase
{
    [SerializeField][Min(1)] int bulletPerShot;

    protected override void Fire()
    {
        Quaternion[] rotations = SpreadUtility.CalculateUniformSpread(
            bulletPerShot,
            BarrelData.SpreadAngle,
            MuzzleTransform.rotation
        );

        foreach (var rotation in rotations)
        {
            ShootBullet(rotation);
        }
    }
}
