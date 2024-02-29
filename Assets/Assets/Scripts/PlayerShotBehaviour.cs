using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotBehaviour : MonoBehaviour
{
    //Llama al script del pool (singletone)
    BulletPool bulletPool;

    //le metemos el GameObject de la torreta del player
    public GameObject playerTurret;

    //Velocidad de la bala
    public float bulletSpeed = 60f;

    //Audio disparo
    public AudioSource audioShot;

    void Start()
    {
        bulletPool = BulletPool.Instance;
    }

    void Update()
    {
        //Si le damos al LMB
        if (Input.GetButtonUp("Fire1"))
        {
            //Creamos de la pool el objeto (usando la función que saca la bala de la pila,  script BulletPool)
            GameObject bullet = bulletPool.ObtenerObjeto();

            //que su posición sea la misma que el cañón/torreta del jugador
            bullet.transform.position = playerTurret.transform.position;
            bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //le añadimos un impulso para que salga disparado hacia delante de la torreta
            bullet.GetComponent<Rigidbody>().AddForce(playerTurret.transform.forward * bulletSpeed, ForceMode.Impulse);
            //para que la bala siempre esté rotada hacia delante
            transform.rotation = Quaternion.RotateTowards(transform.rotation, playerTurret.transform.rotation, Time.deltaTime);

           
            audioShot.Play();
          

        }
        //Hace falta tener el Rigidbody en el prefab, no te olvides de ponerlo
    }
}
