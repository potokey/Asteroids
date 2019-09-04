using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private DateTime bulletTime;
    void Start()
    {
        bulletTime = DateTime.Now;
    }
    void Update()
    {
        if (DateTime.Now - bulletTime > new TimeSpan(0, 0, 5))
            Destroy(this.gameObject);        
    }
    private void OnCollisionEnter(Collision collision)
    {
        var collider = collision.collider.gameObject;
        Destroy(this.gameObject);
        if (collider.layer == 9)
        {
            var type = collider.GetComponent<AsteroidController>().AsteroidType;
            var position = collider.transform.position;
            Destroy(collider);          

            switch (type)
            {
                case AsteroidTypeEnum.Small:
                    Aggregator.Publish("AddPoints", 1);
                    break;
                case AsteroidTypeEnum.Medium:
                    Aggregator.Publish("AddPoints", 2);

                    for (int i = 0; i < 2; i++)                                           
                        CreateAsteroid(PrefabEnum.AsteroidSmall, position);
                    
                    break;
                case AsteroidTypeEnum.Big:
                    Aggregator.Publish("AddPoints", 3);

                    for (int i = 0; i < 2; i++)                    
                        PrefabsHelper.CreatePrefab(PrefabEnum.AsteroidMedium, position);                    
                    break;
                default:
                    break;
            }
        }
    }

    private void CreateAsteroid(PrefabEnum pref, Vector3 position)
    {
        var newObj = PrefabsHelper.CreatePrefab(pref, position);       
    }
}
