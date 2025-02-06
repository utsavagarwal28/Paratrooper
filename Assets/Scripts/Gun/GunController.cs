using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform gunBarrelPivot;
    public float rotationSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        gunBarrelPivot.Rotate(0, 0, -rotation);
    }
}
