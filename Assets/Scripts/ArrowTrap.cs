using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class ArrowTrap : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] float dist;
    [SerializeField] Vector2 offset = Vector2.down * 1.5f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position + offset, Vector2.down, dist);
        if (hit.collider != null)
        {
            var damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                rb.gravityScale = 1f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var damageable = other.collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(1);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine((Vector2)transform.position + offset, (Vector2)transform.position + offset + (Vector2.down * dist));
    }
}
