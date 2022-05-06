using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerInput : MonoBehaviour
{
    public float Horizontal { get; protected set; }
    public float Vertical { get; protected set; }

    public Vector2? DragVect { get; protected set; }
    public bool PressingBreakBtn { get; protected set; }
    [SerializeField] protected float maxStretchLength;

}
