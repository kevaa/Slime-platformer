using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictTrajectory : MonoBehaviour
{

    [SerializeField] GameObject pointPrefab;
    [SerializeField] GameObject[] points;
    [SerializeField] int numPoints;
    [SerializeField] ForceController fc;
    [SerializeField] Transform pointsParent;

    private void Awake()
    {
        fc.OnCharging += OnCharging;
        fc.OnStopCharge += OnStopCharge;
        fc.OnCharge += OnStartCharge;
    }
    void Start()
    {
        points = new GameObject[numPoints];
        for (int i = 0; i < numPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity, pointsParent);
        }
    }

    void OnCharging(Vector2 vect)
    {
        for (int i = 0; i < numPoints; i++)
        {
            points[i].transform.position = GetPointPos(i * .05f, vect);
        }
    }

    Vector2 GetPointPos(float time, Vector2 vect)
    {
        return (Vector2)transform.position + (vect.normalized * vect.magnitude * time) + .5f * Physics2D.gravity * Mathf.Pow(time, 2);
    }

    void OnStopCharge()
    {
        foreach (var point in points)
        {
            point.SetActive(false);
        }
    }

    void OnStartCharge()
    {
        foreach (var point in points)
        {
            point.SetActive(true);
        }
    }
}
