using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomarMunicion : MonoBehaviour
{
    public enum TipoMunicion { Flechas, Polvora }
    public TipoMunicion tipoDeMunicion;
    public int cantidadMunicion = 10;
    public AudioClip cogerMunicionSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (tipoDeMunicion)
            {
                case TipoMunicion.Flechas:
                    GameManager.instance.AddArrowAmmo(cantidadMunicion);
                    break;
                case TipoMunicion.Polvora:
                    GameManager.instance.AddGunpowderAmmo(cantidadMunicion);
                    break;
            }

            AudioManager.instance.ReproducirSonido(cogerMunicionSound);
            Destroy(this.gameObject);
        }
    }
}
