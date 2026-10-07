using UnityEngine;

public class DummyMovement : MonoBehaviour
{
    [SerializeField] Transform targetTransform;
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody enemyRigidbody;
    [SerializeField] float damageAmount;
    Vector3 direction;

    void Update()
    {
        if (targetTransform == null)
        {
            direction = Vector3.zero;
            return;
        }
        direction = (targetTransform.position - transform.position).normalized;
    }

    void FixedUpdate()
    {
        enemyRigidbody.linearVelocity = direction * moveSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealth targetHealth))
        {
            targetHealth.TakeDamage(damageAmount);
        }
    }
}
