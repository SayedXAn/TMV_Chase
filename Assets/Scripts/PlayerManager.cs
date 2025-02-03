using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardSpeed = 10f; // Constant forward speed
    public float dragSpeed = 0.5f; // Speed for dragging player left and right

    private float screenWidth;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        screenWidth = Screen.width; // Cache the screen width for normalization
    }

    private void Update()
    {
        // Move the player forward at a constant speed
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, forwardSpeed);

        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                // Calculate normalized horizontal movement based on screen width
                float normalizedDeltaX = touch.deltaPosition.x / screenWidth;

                // Move the player left or right
                Vector3 newVelocity = new Vector3(normalizedDeltaX * dragSpeed * 100f, rb.linearVelocity.y, rb.linearVelocity.z);
                rb.linearVelocity = newVelocity;
            }
        }
    }
}
