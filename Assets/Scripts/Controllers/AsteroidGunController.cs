using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGunController : MonoBehaviour
{
    private const float timeToNextAsteroid = 2f;
    // Use this for initialization
    void Start()
    {

    }
    float timeLeft = 2f;
    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            GenerateAsteroid();
            timeLeft = timeToNextAsteroid;
        }
    }
    private void GenerateAsteroid()
    {
        var bullet = PrefabsHelper.CreatePrefab(PrefabEnum.AsteroidBig, this.transform.position);
        var rig = bullet.GetComponent<Rigidbody>();
        rig.velocity =  Vector3.left * 100;
    }
}
