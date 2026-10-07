public class DummyHealth : HealthBase
{
    protected override void Die()
    {
        Destroy(gameObject);
    }

    protected override void OnDamaged()
    {
        
    }
}
