using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

    Text _blockName;
    public Text blockName
    {
        get
        {
            if (_blockName == null)
            {
                GameObject obj = GameObject.Find("blockName_txt");
                _blockName = obj.GetComponent<Text>();
            }
            return _blockName;
        }
    }

    public bool showBlockName;

    void Update()
    {
        if (showBlockName)
        {
            if (blockName != null)
            {
                var block = MultiSaveManager.Instance.GetExecutingBlock();
                blockName.text = block == null ?"null" 
                    : block.BlockName;
            }
        }
        else
        {
            if (blockName != null)
                blockName.text = string.Empty;
        }
    }
}
