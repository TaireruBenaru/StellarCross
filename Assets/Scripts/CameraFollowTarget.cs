using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [Range(0, 1)]
    public float followSpeed;
    public bool followPlayer;
    public bool IsInstance;
    public Transform Target;
    public Vector3 velocity = Vector3.zero;

    [Header("Bounds")]
    public bool moveOnX;
    public bool moveOnY;
    public bool moveOnZ;
    public float boundX;
    public float boundY;
    public float boundZ;


    private Vector3 newPosition;

    public static CameraFollowTarget Instance = null;

    void Awake()
    {
        if(IsInstance)
        {
            if (Instance != null) // meaning there's already an instance
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
        
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        if (Target != null)
        {
            if (moveOnX)
            {
                float dx = Target.position.x - transform.position.x;
                if (dx > boundX || dx < -boundX)
                {
                    if (transform.position.x < Target.position.x)
                    {
                        delta.x = dx - boundX;
                    }
                    else
                    {
                        delta.x = dx + boundX;
                    }
                }
            }
            if (moveOnY)
            {
                float dy = Target.position.y - transform.position.y;
                if (dy > boundY || dy < -boundY)
                {
                    if (transform.position.y < Target.position.y)
                    {
                        delta.y = dy - boundY;
                    }
                    else
                    {
                        delta.y = dy + boundY;
                    }
                }
            }
            if (moveOnZ)
            {
                float dz = Target.position.z - transform.position.z;
                if (dz > boundZ || dz < -boundZ)
                {
                    if (transform.position.z < Target.position.z)
                    {
                        delta.z = dz - boundZ;
                    }
                    else
                    {
                        delta.z = dz + boundZ;
                    }
                }
            }
        }

        // Move the camera
        newPosition = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, followSpeed);

    }
}