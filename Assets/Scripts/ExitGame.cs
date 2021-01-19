using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    Button myButton;
    // Start is called before the first frame update
    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(TaskOnClick);
    }
    void TaskOnClick()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
