using UnityEngine;

[CreateAssetMenu(fileName = "BarrelSO", menuName = "Scriptable Objects/BarrelSO")]
public class BarrelSO : ScriptableObject
{
    [field:Header("BarrelSettings")]
    [field:SerializeField] public float AimSmoothTime { get; private set; }
    [field:SerializeField] public float SpreadAngle { get; private set; }
    [field:SerializeField] public float FireRate { get; private set; }
    [field:SerializeField] public bool IsSemiAuto { get; private set; }
    [field:SerializeField] public Texture2D Crosshair { get; private set; }

    [field:Header("BulletSettings")]
    [field:SerializeField] public Bullet BulletPrefab { get; private set; }
    [field:SerializeField] public float BulletSpeed { get; private set; }
    [field:SerializeField] public float MaxRange { get; private set; }
    [field:SerializeField] public float DamageAmount { get; private set; }
}

public enum BarrelPattern
{
    
}
