using UnityEngine;

public class ataquelimpador : MonoBehaviour
{
    public float attackCooldown = 1.5f;
    public int damage = 1;

    private float lastAttackTime = -Mathf.Infinity;

    void OnTriggerEnter(Collider other)
    {
        TryAttack(other);
    }

    void OnTriggerStay(Collider other)
    {
        TryAttack(other);
    }

    void TryAttack(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (Time.time - lastAttackTime < attackCooldown) return;

        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health == null) health = other.GetComponentInParent<PlayerHealth>();

        if (health != null)
        {
            health.TakeDamage(damage);
            lastAttackTime = Time.time;
        }
    }
}
