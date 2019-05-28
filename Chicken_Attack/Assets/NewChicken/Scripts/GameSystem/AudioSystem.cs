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
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        currentScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        
            if (currentScene != SceneManager.GetActiveScene().name)
            {            
                currentScene = SceneManager.GetActiveScene().name;
                switch (currentScene)
                {
                    case "QiZiNewChicken":
                    Destroy(this.gameObject);
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
            }
        }
    }
}
