using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform gunBarrelPivot;
    public GameObject bulletPrefab;
    public float rotationSpeed = 100f;
    public float fireRate = 0.5f;
    public float bulletSpeed = 10f;

    public float nextFireTime = 0f;

    // Update is called once per frame
    void Update()
    {
        RotateGun();

        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // Set next fire time
        }
    }

    void RotateGun()
    {
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        float newRotation = gunBarrelPivot.eulerAngles.z - rotation;


        if (newRotation > 180)
            newRotation -= 360;

        newRotation = Mathf.Clamp(newRotation, -95f, 95f);

        gunBarrelPivot.rotation = Quaternion.Euler(0, 0, newRotation);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunBarrelPivot.position, gunBarrelPivot.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = gunBarrelPivot.up * bulletSpeed; // Move bullet in gun's direction
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PyramidBullet")
        {
            //
            //
            //
            //
            //Game Over
            //
            //
            //
            //
        }
    }

}
