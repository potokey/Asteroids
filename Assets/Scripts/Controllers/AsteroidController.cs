using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public AsteroidTypeEnum AsteroidType;

    void Start()
    {
        var rig = this.GetComponent<Rigidbody>();
        this.transform.localRotation = Random.rotation;
        var ra = Random.Range(0.1f, 1);
        rig.AddTorque(new Vector3(Random.Range(0.1f, 1), Random.Range(0.1f, 1), Random.Range(0.1f, 1)) * Random.Range(1000, 100000));
    }

    void Update()
    {

    }

}
