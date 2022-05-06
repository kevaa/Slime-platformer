using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Prop : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float torque = 300f;
    [SerializeField] float v = 3f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.AddTorque(torque);
        rb.velocity = (Vector2.down + Vector2.left) * v;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var damageable = other.collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(1);
        }
    }
}
