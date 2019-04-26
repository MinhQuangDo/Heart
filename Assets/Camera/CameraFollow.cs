using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 10f;
    public Vector3 offset;

    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        GameObject player2 = GameObject.Find("Player_2");

        if(player)
        {
            target = player.transform;
        }
        else if(player2)
        {
            target = player2.transform;
        }
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
