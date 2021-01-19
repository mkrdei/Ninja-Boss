using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject heart;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(heart,transform.position,transform.parent.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
