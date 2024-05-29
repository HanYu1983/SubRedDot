using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story01Page : APage
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStoryFinish()
    {
        controller.JumpToScene(2);
    }
}
