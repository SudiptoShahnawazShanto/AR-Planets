using Unity.VisualScripting;
using UnityEngine;

public class RotationHomeScreen : MonoBehaviour
{
    public GameObject planetObject;
    public Vector3 rotationVector;
    [SerializeField] private float RotateSpeed = 1f;

    private void Update()
    {
        planetObject.transform.Rotate(rotationVector * RotateSpeed * Time.deltaTime);
    }
}
