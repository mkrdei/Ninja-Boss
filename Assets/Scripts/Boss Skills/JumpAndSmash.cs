using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpAndSmash : MonoBehaviour
{
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if (other.gameObject.tag == "Player" && ps.isPlaying)
        {
            SceneManager.LoadScene("LostScene");
        }
    }
}
