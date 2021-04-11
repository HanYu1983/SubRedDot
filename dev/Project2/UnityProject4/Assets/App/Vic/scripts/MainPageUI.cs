using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPageUI : MonoBehaviour
{
    public void OnMainPageStartClick(string page)
    {
        CompChangeScene.inst.ChangePage(page);
    }
}
