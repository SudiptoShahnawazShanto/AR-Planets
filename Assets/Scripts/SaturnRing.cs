using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaturnRing : MonoBehaviour
{
    public GameObject saturnRing;
    public Vector3 rotationVector;
    [SerializeField] private float rotateSpeed = 1f;

    void Update()
    {
        saturnRing.transform.Rotate(rotationVector * rotateSpeed * Time.deltaTime);
    }
}
