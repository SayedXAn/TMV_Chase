using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardSpeed = 10f; // Constant forward speed
    public float speedModifier; // Speed for dragging player left and right

    private Touch touch;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        speedModifier = 0.01f;
    }

    private void Update()
    {
        //rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, forwardSpeed);
        rb.transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);
        if(Input.touchCount > 0 )
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved && transform.position.x > -2.7f && transform.position.x < 2.7f)
            {
                transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * speedModifier,
                    transform.position.y,
                    transform.position.z /*+ touch.deltaPosition.y * speedModifier*/);
            }
            else if(touch.phase == TouchPhase.Moved && (transform.position.x <= -2.7f || transform.position.x >= 2.7f))
            {
                if (transform.position.x <= -2.7f)
                {
                    transform.position = new Vector3(-2.69f, transform.position.y, transform.position.z);
                }
                else if(transform.position.x >= 2.7f)
                {
                    transform.position = new Vector3(2.69f, transform.position.y, transform.position.z);
                }
                
            }
        }
    }

    
}