using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class PrefabsHelper
{
    public static Dictionary<PrefabEnum, GameObject> PrefabDict { get; private set; }
    public static GameObject dynamicObjects;
    public static void InitializePrefabs()
    {
        PrefabDict = new Dictionary<PrefabEnum, GameObject>()
                    {
                         {PrefabEnum.AsteroidBig,Resources.Load("Prefabs/asteroidBig") as GameObject },
                         {PrefabEnum.AsteroidMedium,Resources.Load("Prefabs/asteroidMedium") as GameObject },
                         {PrefabEnum.AsteroidSmall,Resources.Load("Prefabs/asteroidSmall") as GameObject },

                         {PrefabEnum.Bullet,Resources.Load("Prefabs/Bullet") as GameObject },
                         {PrefabEnum.Ship,Resources.Load("Prefabs/SG_radogost") as GameObject }
                    };
        dynamicObjects = GameObject.Find("DynamicObjects");
    }
    public static GameObject CreatePrefab(PrefabEnum type, Vector3 position, Quaternion? rotation = null)
    {
        var pref = PrefabDict[type];
        var newObject = GameObject.Instantiate(pref, dynamicObjects.transform);
        newObject.transform.localPosition = position;
        if (rotation != null)
            newObject.transform.localRotation = (Quaternion)rotation;

        return newObject;
    }

}
