using System.Collections;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    float x, y, z;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Normal camera y = 7 rotation = 40 z = -17, top position camera y = 14 rotation = 65 z = -7

        if(target.transform.position.z >= 8)
        {
            x = target.transform.position.x;
            y = target.transform.position.y + 16;
            z = 10;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(90, 0, 0), Time.deltaTime);
        }

        else if (target.transform.position.z >= 2)
        {
            x = target.transform.position.x;
            y = target.transform.position.y + 16;
            z = -3;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(65, 0, 0), Time.deltaTime);
        }

        else if (target.transform.position.z >= -2)
        {
            x = target.transform.position.x;
            y = target.transform.position.y + 16;
            z = -9;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(65, 0, 0), Time.deltaTime);
        }

        else if (target.transform.position.z >= -7)
        {
            x = target.transform.position.x;
            y = target.transform.position.y + 16;
            z = -9;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(65, 0, 0), Time.deltaTime);
        }
        else if (target.transform.position.z < -7)
        {
            x = target.transform.position.x;
            y = target.transform.position.y + 7;
            z = -20;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(40, 0, 0), Time.deltaTime);
        }
        StartCoroutine(MoveOverSeconds(gameObject, new Vector3(x, y, z), 1f));
    }
    
  public IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed)
    {
        // speed should be 1 unit per second
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;
    }

}
