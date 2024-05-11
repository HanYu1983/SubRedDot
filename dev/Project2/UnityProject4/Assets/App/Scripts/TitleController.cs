using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{

    public AudioController audioController;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(audioController);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBtnStartClick()
    {
        Debug.Log("OnBtnStartClick");

        audioController.PlayAudioById(0);
    }
}
