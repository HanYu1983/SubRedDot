using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompFrontUI : MonoBehaviour
{

    public void OnChangePage()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
