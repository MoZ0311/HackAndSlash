using Unity.Cinemachine;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float damageAmount;
    [SerializeField] CinemachineImpulseSource impulseSource;
    [SerializeField] float force;
    [SerializeField] SphereCollider sphereCollider;
    [SerializeField] float duration;

    void OnEnable()
    {
        impulseSource.GenerateImpulse(force);

        Invoke(nameof(FinishExplosion), duration);
    }

    void FinishExplosion()
    {
        sphereCollider.enabled = false;
        Debug.Log("Finish Explosion");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out HealthBase targetHealth))
        {
            targetHealth.TakeDamage(damageAmount);
        }
    }
}
