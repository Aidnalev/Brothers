using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPlayer : MonoBehaviour
{
    public Camera cam;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Vector2 direccion;

    void Update()
    {
        direccion = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angulo));
        transform.rotation = rotation;

        // Disparar una bala si se presiona el botón izquierdo del mouse
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Crear una nueva bala en la posición del FirePoint y orientarla en la misma dirección que el arma
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.right = direccion;
    }
}
