using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveGameSlot : MonoBehaviour {

    public int id = 1;
    public bool isAutoSaveSlot = false;

    public Image slot_img;
    [SerializeField]
    Text slot_txt;

    void Awake()
    {
        slot_img = GetComponentInChildren<Image>(true);
    }
    public void SetIcon(Sprite sprite)
    {
        if (slot_img != null)
            slot_img.sprite = sprite;
    }
    
    public void SetName(string slotName)
    {
        if (slot_txt != null)
            slot_txt.text = slotName;
    }
    
    public void SaveOrLoad()
    {
        UIGameMenu.Instance?.SaveOrLoad(id);
    }
}
