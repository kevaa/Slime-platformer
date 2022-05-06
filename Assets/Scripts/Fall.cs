using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Kill();
            return;
        }

        var prop = other.GetComponent<Prop>();
        if (prop != null)
        {
            Destroy(prop.gameObject);
        }

    }
}
