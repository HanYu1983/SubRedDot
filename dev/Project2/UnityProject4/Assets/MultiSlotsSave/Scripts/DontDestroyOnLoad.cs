using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

    static List<string> instances = new List<string>();

    void Awake()
    {
        if (instances.Contains(gameObject.name))
        {
            Destroy(gameObject);
            return;
        }
        instances.Add(gameObject.name);
        DontDestroyOnLoad(this);
    }
}
