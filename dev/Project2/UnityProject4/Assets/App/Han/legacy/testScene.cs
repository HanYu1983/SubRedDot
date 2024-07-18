using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class testScene : MonoBehaviour
{
    public static IObservable<string> events = Observable.Merge(Events.onButtonDown);
    public string nextPage;
    private void Awake()
    {
        events.Subscribe(name =>
        {
            Debug.Log(name);
            if (name == "changePage")
            {
                CompChangeScene.inst.ChangePage(nextPage);
            }
        });
    }
}