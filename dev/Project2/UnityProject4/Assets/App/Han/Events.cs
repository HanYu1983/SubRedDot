using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Events : MonoBehaviour
{
    public static Subject<string> onButtonDown = new Subject<string>();
    public static Subject<string> onButtonUp = new Subject<string>();
}
