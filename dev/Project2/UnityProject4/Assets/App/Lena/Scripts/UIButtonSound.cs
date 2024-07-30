//Easy Script for button sound event: Hover & Click sound by JS 2019
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using Assets.App.Han;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler
{

    public AudioClip ClickedSound;
    public AudioClip HoverSound;

    //get button component
    private Button button { get { return GetComponent<Button>(); } }
    // get audiosource
    private AudioSource source { 
        get {
            var gameState = FindObjectOfType<GameState>();
            if(gameState == null)
            {
                throw new UnityException("gameState not found");
            }
            return gameState.soundEffectAudioSource;
        } 
    }


    void Start()

    {
        //set default sound
        source.clip = HoverSound;

        source.playOnAwake = false;


        button.onClick.AddListener(() => PlayClickSoud());


    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData == null)
        {
            throw new System.ArgumentNullException(nameof(eventData));
        }

        source.clip = HoverSound;
        source.PlayOneShot(HoverSound);
    }

    void PlayClickSoud()

    {

        source.clip = ClickedSound;
        source.PlayOneShot(ClickedSound);

    }
}