using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Throwable_Script : MonoBehaviour
{
    public GameObject particleObject;
    public GameObject boss;
    private Rigidbody rb;
    private Collider col;
    Vector3 minusVector, desiredVector;
    bool collided;
    // Start is called before the first frame update
    void Start()
    {
        collided = false;
        rb = GetComponent<Rigidbody>();
        minusVector = Vector3.zero;
        desiredVector = Vector3.zero;
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!collided&& GameObject.Find("Boss")!=null)
            StartCoroutine(MoveOverSpeed(gameObject, GameObject.Find("Boss").transform.position, 2f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Boss")
        {
            collided = true;
            rb.isKinematic = true;
            col.isTrigger = true;
            gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.transform.parent = GameObject.Find("Boss").transform;
            
            GameObject firework = Instantiate(particleObject, transform.position, Quaternion.identity);
            Destroy(firework,2f);
            
        }
        
    }
    public IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed)
    {
        // speed should be 1 unit per second
        while (!collided)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (!collided)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
    }
}
