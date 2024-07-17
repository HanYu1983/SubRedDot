using UnityEngine;
using System.Collections;

public class PrefabLoader : MonoBehaviour
{
    public GameObject prefabToLoad; // 要加載的預製件
    public Transform parentTransform; // 預製件將被實例化到的父物件

    private GameObject currentInstance; // 當前實例化的預製件

    public void LoadPrefab()
    {
        // 如果已經有一個實例，先銷毀它
        if (currentInstance != null)
        {
            Destroy(currentInstance);
        }

        // 實例化新的預製件
        if (prefabToLoad != null)
        {
            currentInstance = Instantiate(prefabToLoad, parentTransform);
        }
        else
        {
            Debug.LogError("沒有指定預製件!");
        }
    }

    public IEnumerator DestroyPrefab()
    {
        if (currentInstance != null)
        {
            Destroy(currentInstance);
        }
        yield return null;
    }
}