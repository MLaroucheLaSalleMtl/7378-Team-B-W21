using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public GameObject loadingScreen;
    private AsyncOperation async; 
    [SerializeField] private Slider progressbar;
    public int nextSceneIndex;
    public int thisSceneIndex;
    


    bool ISPause;
    bool isStop = true;
    private bool InGame;
    public Transform Pause;

    // Start is called before the first frame update
    void Start()
    {
        progressbar.value = 0;
    }



    // Update is called once per frame
    void Update()
    {

        if (isStop == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                isStop = false;
                Pause.gameObject.SetActive(true);
                AudioListener.volume = 0;

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                isStop = true;
                Pause.gameObject.SetActive(false);
                AudioListener.volume = 1;

            }
        }

    }
    public IEnumerator LoadLevel(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);//load target scene
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressbar.value = progress;
            yield return null;
        }
    }



    public void NextScene()
    {
        StartCoroutine(LoadLevel(nextSceneIndex));
        AudioListener.volume = 1;

    }
    public void RetryScene()
    {
        StartCoroutine(LoadLevel(thisSceneIndex));
        AudioListener.volume = 1;
    }



    public void Quitgame()
    {

        //Application.Quit();
#if UNITY_EDITOR
#else
     Application.Quit();
#endif
        
     }


}
