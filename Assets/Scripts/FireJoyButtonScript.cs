using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FireJoyButtonScript : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
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
    

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = true;
        eventData.eligibleForClick = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = false;
        
    }
}
