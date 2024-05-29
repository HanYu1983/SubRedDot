using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPage : APage
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBtnStartClick()
    {
        controller.JumpToScene(1);
    }
}
