using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class JumpJoyButtonScript : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    [HideInInspector]
    public bool pressed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
        print("pressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }
}
