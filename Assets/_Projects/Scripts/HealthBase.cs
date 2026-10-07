using UnityEngine;

public abstract class HealthBase : MonoBehaviour
{
    [field:SerializeField] protected float MaxHealth { get; private set;}
    protected float currentHealth;
    public bool CanControl { get; private set; } = true;

    void Awake()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        // HPを0以上最大値以下の範囲にClamp
        currentHealth = Mathf.Clamp(currentHealth - damageAmount, 0, MaxHealth);

        OnDamaged();

        // HPが0(念のため以下)のとき、死亡
        if (currentHealth <= 0)
        {
            CanControl = false;
            Die();
        }
    }

    protected abstract void Die();
    protected abstract void OnDamaged();
}
