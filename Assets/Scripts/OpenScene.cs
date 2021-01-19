using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class OpenScene : MonoBehaviour
{

    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        
    }

    void TaskOnClick()
    {
        switch (gameObject.name)
        {
            case "PlayButton":
                sceneName = "SampleScene";
                break;
            case "HowToPlayButton":
                sceneName = "HowToPlayScene";
                break;
            case "MainMenuButton":
                sceneName = "StartScene";
                break;
        }
        SceneManager.LoadScene(sceneName);
        
    }
    // Update is called once per frame
    
}
