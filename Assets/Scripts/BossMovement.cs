using UnityEngine;

public class BossMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
    private Rigidbody rb;
    Vector3 desiredPosition;
    public float health;
    float gravity = 10f;
    private float jumpHeight = 5;
    float rotationSpeed = 5;
    float bossSpeed = 3;
    bool jumped = false;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    float time;
    float lastTimeShot;
    CharacterController cc;
    Vector3 moveDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        health = 10;
        moveDirection = new Vector3(0, 0, 0);
        time = 0;
    }

    private void Update()
    {
        /*
        groundedPlayer = cc.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            if (jumped)
            {
                GameObject.Find("SmashArea").GetComponent<ParticleSystem>().Play();
            }
            jumped = false;
            

        }
        cc.Move(transform.forward * Time.deltaTime * bossSpeed);

        if (transform.forward != Vector3.zero)
        {
            gameObject.transform.forward = transform.forward;
        }

        playerVelocity.y += gravity * Time.deltaTime;
        cc.Move(playerVelocity * Time.deltaTime);*/
    }

    void FixedUpdate()
    {
        time++;

        Quaternion neededRotation = Quaternion.LookRotation(target.position - transform.position);
        Quaternion interpolatedRotation = Quaternion.Slerp(transform.rotation, neededRotation, rotationSpeed);
        interpolatedRotation.x = 0;
        interpolatedRotation.z = 0;
        transform.rotation = interpolatedRotation;
        groundedPlayer = cc.isGrounded;
        if (groundedPlayer)
        {
            moveDirection = transform.forward * bossSpeed;
            if (jumped)
            {
                GameObject.Find("SmashArea").GetComponent<ParticleSystem>().Play();
            }
            jumped = false;
        }
        else
        {

        }
        bossSkill(150, "jump_and_smash");
        moveDirection.y -= gravity * Time.deltaTime;


        cc.Move(moveDirection * Time.deltaTime);
        /*
        
        desiredPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        Quaternion neededRotation = Quaternion.LookRotation(target.position - transform.position);
        Quaternion interpolatedRotation = Quaternion.Slerp(transform.rotation, neededRotation, Time.deltaTime * rotationSpeed);
        interpolatedRotation.x = 0;
        interpolatedRotation.z = 0;
        transform.rotation = interpolatedRotation;
        cc.Move(transform.forward*Time.deltaTime);
        bossSkill(100, "jump_and_smash");

        /*
        
        desiredPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        Quaternion neededRotation = Quaternion.LookRotation(target.position - transform.position);
        Quaternion interpolatedRotation = Quaternion.Slerp(transform.rotation, neededRotation, Time.deltaTime * rotationSpeed);
        interpolatedRotation.x = 0;
        interpolatedRotation.z = 0;
        transform.rotation = interpolatedRotation;
        moveDirection = Vector3.zero;
        
        
        if (cc.isGrounded)
        {
            
            print("CharacterController is grounded");
            jumped = false;
            moveDirection = transform.forward;
            moveDirection *= bossSpeed;
        }
        else
        {
            moveDirection.y -= (gravity);
        }
        if(!jumped)
            cc.Move(moveDirection * Time.deltaTime);

        bossSkill(100, "jump_and_smash");

    }
    */

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "throwable")
        {
            if (Time.time - lastTimeShot > 1)
            {
                health--;
            }
            
            lastTimeShot = Time.time;
        }
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }
    private void bossSkill(float period, string name)
    {

        if (time == period)
        {
            if (name == "jump_and_smash")
            {
                time = 0;
                if (groundedPlayer)
                {
                    jumped = true;
                    print("jump");
                    moveDirection += transform.up * jumpHeight;
                }
            }
        }

    }

}
