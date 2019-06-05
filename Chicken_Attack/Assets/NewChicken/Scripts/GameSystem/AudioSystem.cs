using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AudioSystem : Singleton<AudioSystem>
{
    public AudioSource[] audios;
    private string currentScene;
    private bool changeAudio = false;
    private int currentPlay;
    //private static bool isNoDestroyHandler = true;//是否没有DontDestroyOnLoad处理

    // Start is called before the first frame update
    void Start()
    {
        //if (isNoDestroyHandler)
        //{
           //isNoDestroyHandler = false;
            DontDestroyOnLoad(this.gameObject);
            currentScene = SceneManager.GetActiveScene().name;
       // }
        //else if (!isNoDestroyHandler)
        //{
           // Destroy(this.gameObject);
        //}
    }

    // Update is called once per frame
    void Update()
    {
       if(currentScene=="Begin"&&currentScene != SceneManager.GetActiveScene().name&& SceneManager.GetActiveScene().name!="LoadScene")
        {
            currentScene = SceneManager.GetActiveScene().name;
        }
         else if(currentScene != SceneManager.GetActiveScene().name&&currentScene!="Begin")
            {
            print(currentScene);
                currentScene = SceneManager.GetActiveScene().name;
                switch (currentScene)
                {
                    case "QiZiNewChicken":
                    //Destroy(this.gameObject);
                    currentPlay = 0;
                    changeAudio = true;
                        break;
                    case "HP_Train":
                    currentPlay = 1;
                    changeAudio = true;
                    break;
                    case "ATK_Training":
                    currentPlay = 2;
                    changeAudio = true;
                    break;
                    case "Strong_Train":
                    currentPlay = 3;
                    changeAudio = true;
                    break;
                case "BattleNew":
                    currentPlay = 4;
                    changeAudio = true;
                    break;
                    default:
                        break;
                }
            if (changeAudio)
            {
                foreach (AudioSource audio in audios)
                {
                    if(audio!=null)
                    audio.Stop();
                }
                audios[currentPlay].Play();
                changeAudio = false;
            }
        }
    }
}
