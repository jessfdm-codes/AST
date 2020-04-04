using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DogController : MonoBehaviour
{
    private bool mouseOver = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("noted...");
            StartCoroutine(LoadDogStroke());
        }
    }

    private IEnumerator LoadDogStroke() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("game");
        Debug.Log("loading...");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private void OnMouseEnter()  => mouseOver = true;
    private void onMouseExit() => mouseOver = false;
}
