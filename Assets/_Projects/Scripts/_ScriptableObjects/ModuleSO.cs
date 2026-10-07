using UnityEngine;

[CreateAssetMenu(fileName = "ModuleSO", menuName = "Scriptable Objects/ModuleSO")]
public class ModuleSO : ScriptableObject
{
    [field:Header("ModuleMultiplier")]
    [field:SerializeField] public float AimSpeedMultiplier { get; private set; }
    [field:SerializeField] public float SpreadMultiplier { get; private set; }
    [field:SerializeField] public float RateMultiplier { get; private set; }
    [field:SerializeField] public float SpeedMultiplier { get; private set; }
    [field:SerializeField] public float RangeMultiplier { get; private set; }
    [field:SerializeField] public float DamageMultiplier { get; private set; }

    [field:SerializeField] public ModuleEffect ModuleEffect { get; private set; }
}

public enum ModuleEffect
{
    Penetration,
    Ricochet,
    Explosion,
    None
}