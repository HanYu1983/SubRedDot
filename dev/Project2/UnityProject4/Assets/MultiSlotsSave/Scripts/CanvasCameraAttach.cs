using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasCameraAttach : MonoBehaviour {

    [SerializeField]
    string cameraTag = "Main";
    [SerializeField]
    bool toAllCanvasesInScene = true;
    [SerializeField]
    ActivationPeriod activationPeriod = ActivationPeriod.OnLevelLoaded;
    void Start()
    {
        StartCoroutine(AttachDelayTimer());
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded-= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(activationPeriod== ActivationPeriod.OnLevelLoaded)
            StartCoroutine(AttachDelayTimer());
    }
    public void AttachCamToCanvases()
    {
        var goCam = GameObject.FindGameObjectWithTag(cameraTag);
        Camera cam = null;
        if (goCam != null)
            cam = goCam.GetComponent<Camera>();
        if (cam == null)
        {
            Debug.Log("camera with tag "+cameraTag+" not found");
            return;
        }
        if (toAllCanvasesInScene)
        {
            var canvases = FindObjectsOfType<Canvas>();
            //Debug.Log("canvases found: " + canvases.Length);

            foreach (var canvas in canvases)
                canvas.worldCamera = cam;
        }
        else
        {
            var canvas = transform.root.GetComponentInChildren<Canvas>();
            if (canvas != null) canvas.worldCamera = cam;
        }
        
    }
    IEnumerator AttachDelayTimer()
    {
        yield return new WaitForEndOfFrame();

        AttachCamToCanvases();

        yield break;
    }
    public enum ActivationPeriod
    {
        OnLevelLoaded,
        OnEnable,
    }
}
