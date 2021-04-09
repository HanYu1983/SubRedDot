using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CompChangeScene : MonoBehaviour
{
    Action<AsyncOperation> onLoadSceneComplete;
    public string startPage;

    public static CompChangeScene inst;
    public CompFrontUI ui;

    void Start()
    {
        if(inst == null)
        {
            inst = this;

            onLoadSceneComplete = new Action<AsyncOperation>(doLoadSceneComplete);
        }

        ChangePage(startPage);
    }

    public void ChangePage(string page)
    {
        string[] tmp = page.Split(char.Parse("/"));
        string pageName = tmp[tmp.Length - 1];
        if(pageName != SceneManager.GetActiveScene().name)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(page);
            ao.completed += onLoadSceneComplete;
        }
    }

    void doLoadSceneComplete(AsyncOperation ao)
    {
        ao.completed -= onLoadSceneComplete;
        ui.OnChangePage();
    }

}
