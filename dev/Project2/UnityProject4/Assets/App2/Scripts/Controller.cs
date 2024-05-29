using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public SceneLoader loader;

    // Start is called before the first frame update
    void Start()
    {
        JumpToScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JumpToScene(int id)
    {
        loader.StartLoad(id);
    }
}
