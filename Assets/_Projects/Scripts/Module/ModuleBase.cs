using UnityEngine;

public abstract class ModuleBase : MonoBehaviour
{
    [field:SerializeField] public ModuleSO ModuleData { get; private set; }
}