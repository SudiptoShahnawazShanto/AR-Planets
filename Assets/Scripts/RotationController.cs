using Unity.VisualScripting;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    public GameObject planetObject;
    public Vector3 rotationVector;

    private Quaternion initialRotation;

    private Touch touch;
    public float PassiveRotateSpeed = 1f;
    public float AassiveRotateSpeed = 2.5f;
    private float timeAfterTouch = 0.2f;

    private void Start()
    {
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                float rotationX = touch.deltaPosition.y * AassiveRotateSpeed * Time.deltaTime;
                float rotationY = -touch.deltaPosition.x * AassiveRotateSpeed * Time.deltaTime;

                planetObject.transform.Rotate(rotationX, rotationY, 0f, Space.World);
            }
            timeAfterTouch = 0;   
        }
        else
        {
            timeAfterTouch += Time.deltaTime;

            if (timeAfterTouch >= 0.2)
            {
                planetObject.transform.Rotate(rotationVector * PassiveRotateSpeed * Time.deltaTime);
            }
        }
    }
}
