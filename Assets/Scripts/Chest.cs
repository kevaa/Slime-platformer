using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
public class Chest : MonoBehaviour
{
    BoxCollider2D col;
    Animator anim;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var collector = other.GetComponent<ICollector>();
        if (collector != null)
        {
            anim.SetTrigger("Collected");
            col.enabled = false;
            collector.Collect();
        }
    }
}