using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupController : MonoBehaviour
{
    void Start()
    {
        PrefabsHelper.InitializePrefabs();
    }
}
