using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Controller controller;
    public RectTransform container;
    public List<string> scenes = new List<string>();

    private GameObject currentPage;
    public GameObject loadingPage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLoad(int id)
    {
        loadingPage.SetActive(true);
        StartCoroutine(LoadAsyncScene(id));
    }
    public IEnumerator LoadAsyncScene(int id)
    {
        ResourceRequest request = Resources.LoadAsync<GameObject>(scenes[id]);

        while (!request.isDone)
        {
            yield return null; 
        }

        if(currentPage != null) { Destroy(currentPage); }

        GameObject page = Instantiate(request.asset, container) as GameObject;
        page.GetComponent<APage>().controller = controller;

        currentPage = page;

        loadingPage.GetComponent<Animation>().Play();

        yield return new WaitForSeconds(0.5f);

        loadingPage.SetActive(false);
    }
}
