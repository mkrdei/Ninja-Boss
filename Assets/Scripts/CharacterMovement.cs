using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterMovement : MonoBehaviour
{

    CharacterController cc;
    private bool rotating;
    private Vector3 moveDirection = Vector3.zero;
    private bool wallCollided = false;
    private bool jumpedWall = false;
    private float flipTime;
    public float flipSpeed = 15f;
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    Animator animator;
    string lastCollision;
    public Transform target;
    public GameObject resultText;
    Vector3 desiredTarget;
    public Rigidbody fireRb;
    public Transform fireTarget;
    private float dashSpeed = 1f;
    private bool dash;
    private Vector2 pointA, pointB;
    Vector3 desiredFirePosition;
    protected Joystick joystick;
    public GameObject boss;
    private GameObject mobileControls;
    protected JumpJoyButtonScript jumpJoyButtonScript;
    protected DashJoyButtonScript dashJoyButtonScript;
    protected FireJoyButtonScript fireJoyButtonScript;
    protected float fireTime = 0;
    private float horizontalAxis;
    private float verticalAxis;
    private bool collidedBoss = false;
    // Start is called before the first frame update
    void Start()
    {
        mobileControls = GameObject.Find("MobileControls");
        mobileControls.SetActive(Application.platform == RuntimePlatform.Android);
        joystick = FindObjectOfType<Joystick>();
        jumpJoyButtonScript = FindObjectOfType<JumpJoyButtonScript>();
        dashJoyButtonScript = FindObjectOfType<DashJoyButtonScript>();
        fireJoyButtonScript = FindObjectOfType<FireJoyButtonScript>();
        flipTime = 0;
        dash = false;
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < 0)
        {
            SceneManager.LoadScene("LostScene");
        }
        if (target != null)
            desiredTarget = new Vector3(target.position.x, transform.position.y, target.position.z);

        cc.transform.LookAt(desiredTarget);
        if (cc.isGrounded)
        {
            //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            flipTime = 0;
            jumpedWall = false;

            animator.SetBool("flippable", false);
            if (Application.platform == RuntimePlatform.Android)
            {
                horizontalAxis = joystick.Horizontal;
                verticalAxis = joystick.Vertical;
                moveDirection = new Vector3(horizontalAxis, 0.0f, verticalAxis);
                moveDirection *= speed;
                if (dashJoyButtonScript.pressed)
                    dashSpeed = 10f;
                if (jumpJoyButtonScript.pressed)
                    moveDirection.y = jumpSpeed;

            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WebGLPlayer)
            {
                horizontalAxis = Input.GetAxis("Horizontal");
                verticalAxis = Input.GetAxis("Vertical");
                moveDirection = new Vector3(horizontalAxis, 0.0f, verticalAxis);
                moveDirection *= speed;

            }

            
            if (Input.GetKey(KeyCode.LeftShift))
                dashSpeed = 10f;
            if (Input.GetKey(KeyCode.Space))
                moveDirection.y = jumpSpeed;
        }
        else
        {

            if (wallCollided)
            {
                bool mobileJump = false;
                if (Application.platform == RuntimePlatform.Android)
                    mobileJump = jumpJoyButtonScript.pressed;

                if (mobileJump || Input.GetKey(KeyCode.Space))
                {
                    moveDirection.y = jumpSpeed;
                    if (lastCollision == "Right Wall")
                        moveDirection = new Vector3(-1f, 2.0f, 0f);
                    else if (lastCollision == "Left Wall")
                        moveDirection = new Vector3(1f, 2.0f, 0f);
                    //Bu kısım silinebilir.
                    /*
                    else if(lastCollision == "Boss")
                    {
                        if(transform.position.z - GameObject.Find("Boss").transform.position.z<0)
                            moveDirection = new Vector3(0.0f, 2.0f, -1f);
                        else if(transform.position.z - GameObject.Find("Boss").transform.position.z>0)
                            moveDirection = new Vector3(0.0f, 2.0f, 1f);
                    }*/
                    moveDirection *= speed;
                    wallCollided = false;
                    jumpedWall = true;

                }

            }
            if (jumpedWall)
            {
                if (lastCollision == "Right Wall")
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, flipTime * flipSpeed);
                else if (lastCollision == "Left Wall")
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -flipTime * flipSpeed);
                //Bu kısım da silinebilir.
                /*
                else if(lastCollision == "Boss") {
                    if (transform.position.z - GameObject.Find("Boss").transform.position.z < 0)
                        transform.eulerAngles = new Vector3(-flipTime * flipSpeed, 0, 0);
                    else if (transform.position.z - GameObject.Find("Boss").transform.position.z > 0)
                        transform.eulerAngles = new Vector3(flipTime * flipSpeed, 0, 0);
                }*/
                flipTime++;
                if (flipTime == -180)
                    flipTime = 0;
                // Flippedddddddd
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        cc.Move(moveDirection * Time.deltaTime * dashSpeed);
        dashSpeed = 1;
        Fire();

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("nameeee" + collision.collider.transform.name);
        if (collision.collider.transform.name == "Left Wall" || collision.collider.transform.name == "Right Wall")
        {
            lastCollision = collision.collider.transform.name;
            wallCollided = true;
        }
        if (collision.collider.transform.tag == "Boss")
        {
            speed = 0;
            jumpSpeed = 0;
            flipSpeed = 0;
            collidedBoss = true;
            SceneManager.LoadScene("LostScene");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.transform.name == "Left Wall" || collision.collider.transform.name == "Right Wall")
        {
            wallCollided = false;
        }
    }

    private void Fire()
    {


        fireTime++;
        if (boss != null)
        {
            if (fireTime == 100 && !collidedBoss)
            {
                Rigidbody clone;
                desiredFirePosition = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
                clone = Instantiate(fireRb, desiredFirePosition, fireTarget.rotation);
                fireTime = 0;
            }
        }
        else
        {
            SceneManager.LoadScene("VictoryScene");
        }
        //float dist = Vector3.Distance(fireRb.position, fireTarget.position);
        //clone.velocity = (transform.forward * dist*dist*dist/50000 + new Vector3(0, fireRb.position.y-fireTarget.position.y, 0)) * Time.deltaTime * 200;

    }



}

