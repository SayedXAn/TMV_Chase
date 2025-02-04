using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("GameObjects")]
    public Rigidbody rb;
    public GameObject[] blocks;
    [Header("Variables")]
    public float forwardSpeed = 10f; // Constant forward speed
    public float speedModifier = 0.1f; // Speed for dragging player left and right
    public float maxLeftPosition = -5f;
    public float maxRightPosition = 5f;
    public float mouseSens = 1f;
    public float lerpSens = 1f;
    public Animator playerRunning;
    private Touch touch;

    [Header("Scripts")]
    public UIManagerScript uiman;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //speedModifier = 0.01f;
    }

    private void Update()
    {
        //rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, forwardSpeed);
        if(uiman.gameOn)
        {
            rb.transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
            TouchControl();
        }
        //MouseControl();


    }
    private void TouchControl()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                float temp = Mathf.Clamp(transform.position.x + touch.deltaPosition.x * (-speedModifier) * Time.deltaTime, maxLeftPosition, maxRightPosition);
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, temp, lerpSens), transform.position.y, transform.position.z);
            }
            /*if (touch.phase == TouchPhase.Moved && transform.position.x > maxLeftPosition && transform.position.x < maxRightPosition)
            {
                transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * speedModifier, transform.position.y, transform.position.z);
            }
            else if (touch.phase == TouchPhase.Moved && (transform.position.x <= maxLeftPosition || transform.position.x >= maxRightPosition))
            {
                if (transform.position.x <= maxLeftPosition)
                {
                    transform.position = new Vector3(maxLeftPosition+0.01f, transform.position.y, transform.position.z);
                }
                else if (transform.position.x >= maxRightPosition)
                {
                    transform.position = new Vector3(maxRightPosition-0.01f, transform.position.y, transform.position.z);
                }
            }*/
        }
        else
        {
            MouseControl();
        }
    }


    public void MouseControl()
    {
        float mouseAxis = Mathf.Clamp(transform.position.x + (Input.GetAxisRaw("Horizontal") * Time.deltaTime * (-mouseSens)), maxLeftPosition, maxRightPosition);
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, mouseAxis, lerpSens), transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "amadertracker")
        {
            Destroy(collision.gameObject);
            StopCoroutine(SpeedUp());
            StopCoroutine(SpeedDown());
            StartCoroutine(SpeedUp());
            //speedup
        }
        else if(collision.gameObject.tag == "onnotracker")
        {
            Destroy(collision.gameObject);
            StopCoroutine(SpeedDown());
            StopCoroutine(SpeedUp());
            if (uiman.OnnoTrackerNise())
            {                
                StartCoroutine(SpeedDown());
            }
            //speedbump
        }
        else if (collision.gameObject.tag == "chor")
        {
            uiman.GameWin();
        }
        else if (collision.gameObject.tag == "npc_car")
        {
            StopAllCoroutines();
            uiman.GameOver(false);
        }
        else if ((collision.gameObject.tag == "marker_1"))
        {
            Destroy(collision.gameObject);
            blocks[2].SetActive(true);
            Destroy(blocks[0]);            
        }
        else if ((collision.gameObject.tag == "marker_2"))
        {
            Destroy(collision.gameObject);
            Destroy(blocks[1]);
        }
    }

    IEnumerator SpeedUp()
    {
        forwardSpeed = 15f;
        playerRunning.GetComponent<Animator>().speed = 1.5f;
        yield return new WaitForSeconds(3);
        forwardSpeed = 10f;
        playerRunning.GetComponent<Animator>().speed = 1f;
    }

    IEnumerator SpeedDown()
    {
        forwardSpeed = 7f;
        playerRunning.GetComponent<Animator>().speed = 0.7f;
        yield return new WaitForSeconds(3);
        forwardSpeed = 10f;
        playerRunning.GetComponent<Animator>().speed = 1f;
    }

    
}