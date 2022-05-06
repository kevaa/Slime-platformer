using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FallingProps : MonoBehaviour
{
    [SerializeField] GameObject[] prefabs;
    float timer = 0f;
    [SerializeField] float secPerDrop = 4f;
    [SerializeField] Transform spawnTransform;
    System.Random rand;
    [SerializeField] Transform parent;
    private void Start()
    {
        rand = new System.Random();
    }
    private void Update()
    {
        if (timer >= secPerDrop)
        {
            var randInd = rand.Next(0, prefabs.Length);
            Instantiate(prefabs[randInd], spawnTransform.position, Quaternion.identity, parent);
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

}
