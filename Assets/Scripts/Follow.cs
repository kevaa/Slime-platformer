using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class Follow : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float smoothSpeed = .125f;
    Vector3 camOffset = Vector3.back;
    float defaultCamSize = 9f;

    float longJumpPowerupCamSize = 14f;
    float minDistToStartFollow = .35f;
    float targetDistMultiplier;

    Camera cam;
    private void Awake()
    {
        cam = GetComponent<Camera>();
        var player = followTarget.GetComponent<Player>();
        if (player != null)
        {
            player.OnPowerupCollected += OnPowerupCollected;
            player.OnPowerupTimeup += OnPowerupTimeup;
        }
        cam.orthographicSize = defaultCamSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var targetPos = followTarget.position + camOffset;
        var thisToTargetMultiplier = (targetPos - transform.position).magnitude;
        if (thisToTargetMultiplier > minDistToStartFollow)
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * thisToTargetMultiplier * Time.deltaTime);
    }

    void OnPowerupCollected(PowerupType type, float seconds)
    {
        switch (type)
        {
            case PowerupType.LongJump:
                {
                    cam.orthographicSize = longJumpPowerupCamSize;
                    break;
                }
        }
    }

    void OnPowerupTimeup(PowerupType type)
    {
        switch (type)
        {
            case PowerupType.LongJump:
                {
                    cam.orthographicSize = defaultCamSize;
                    break;
                }
        }
    }
}
