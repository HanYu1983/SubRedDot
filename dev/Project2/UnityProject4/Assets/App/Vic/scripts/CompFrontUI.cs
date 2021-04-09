using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Keep))]
public class CompFrontUI : MonoBehaviour
{

    public void OnChangePage()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
