using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


[System.Serializable]
public enum Page
{
    INIT,
    MAIN,
    GAME
}

public class CompChangeScene : MonoBehaviour
{
    Action<AsyncOperation> onLoadSceneComplete;
    public Page startPage;

    public static CompChangeScene inst;

    void Start()
    {
        inst = this;

        onLoadSceneComplete = new Action<AsyncOperation>(doLoadSceneComplete);
        DontDestroyOnLoad(this.gameObject);

        if(startPage == Page.INIT)
        {
            startPage = Page.MAIN;
        }
        ChangePage(startPage);
    }

    public void ChangePage(Page page)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync((int)page);
        ao.completed += onLoadSceneComplete;
    }

    public void JumpMainPage()
    {
        ChangePage(Page.MAIN);
    }

    public void JumpGamePage()
    {
        ChangePage(Page.GAME);
    }

    void doLoadSceneComplete(AsyncOperation ao)
    {
        ao.completed -= onLoadSceneComplete;
    }

}
