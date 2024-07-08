using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FungusAudio : MonoBehaviour
{
    public Slider sliderAudio;

    // Start is called before the first frame update
    void Start()
    {
        sliderAudio.onValueChanged.AddListener(SetVolumn);
    }

    public void SetVolumn(float volume)
    {
        FungusManager.Instance.MusicManager.SetAudioVolume(volume, 0, null);
    }
}
