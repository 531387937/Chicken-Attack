using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loading :MonoBehaviour
{
    AsyncOperation async=null;
    public string ScenceName;
    private float progressValue;
    // Start is called before the first frame update
    void Start()
    {
        ScenceName = FeedToBattle.NextScene;
        StartCoroutine(LoadScenes());
    }

    IEnumerator LoadScenes()
    {
        async = SceneManager.LoadSceneAsync(ScenceName);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            if (async.progress < 0.9f)
                progressValue = async.progress;
            else
                progressValue = 1.0f;


            if (progressValue >= 0.9)
            {
                //if (Input.anyKeyDown)
                //{
                //async.allowSceneActivation = true;
                //}
                Invoke("Change", 1f);
            }
            yield return null;
        }
    }

    void Change()
    {
        async.allowSceneActivation = true;
    }
}
