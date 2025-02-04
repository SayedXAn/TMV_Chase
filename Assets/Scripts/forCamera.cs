using UnityEngine;

public class forCamera : MonoBehaviour
{
    public GameObject player;
    public float distance = 10f;
    public float lerpSens = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x , lerpSens), transform.position.y, player.transform.position.z + distance);
    }
}
