using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerShotBehaviour : MonoBehaviour
{
    //Llama al script del pool (singletone)
    BulletPool bulletPool;

    //le metemos el GameObject de la torreta del player
    public GameObject playerTurret;

    //Velocidad de la bala
    float bulletSpeed = 200f;

    //Audio disparo
    public AudioSource audioShot;

    //Cooldown Disparo
    bool bulletUsed = false;
    float bulletTimer = 0.0f;
    public float playerBulletShotCooldown = 0.5f;

    //Recarga de munición cuando las balas estén a cero
    public int bulletAmmunitionRemaining = 15;
    float reloadingTimer = 0.0f;

    public float reloadingBulletsDuration = 2.0f;
    bool hasAmmunition = true;

    //Recarga de munición cuando las balas estén a cero
    public float manualReloadingBulletsDuration = 1.0f;
    bool needManualReload = false;

    //Mostrar cantidad de balas
    [SerializeField]
    TextMeshProUGUI ammunitionText;

    void Start()
    {
        bulletPool = BulletPool.instance;
        bulletTimer = 0.0f;

    }

    void Update()
    {

        if (bulletUsed == false && hasAmmunition == true)
        {
            //Si le damos al LMb
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
                bulletUsed = true;

                bulletAmmunitionRemaining--;
            }          
            //Hace falta tener el Rigidbody en el prefab, no te olvides de ponerlo
        }
        else
        {
            bulletTimer += Time.deltaTime;
            if (bulletTimer >= playerBulletShotCooldown)
            {
                bulletUsed = false;
                bulletTimer = 0f;
            }
        }
        if (bulletAmmunitionRemaining <= 0)
        {
            ReloadingBullets();
            hasAmmunition = false;
            ammunitionText.text = "Reloading";
        }
        else
        {
            hasAmmunition = true;
            ammunitionText.text = "Ammo:" + bulletAmmunitionRemaining + "/15";
        }

        if(Input.GetKeyDown(KeyCode.R) && bulletAmmunitionRemaining <= 14)
        {
            needManualReload = true;       
        }
        if (needManualReload == true)
        {
            ManualReloadingBullets();
            hasAmmunition = false;
            ammunitionText.text = "Reloading";
        }
    }
    public void ReloadingBullets()
    {
        reloadingTimer += Time.deltaTime;
        if(reloadingTimer >= reloadingBulletsDuration)
        {
            bulletAmmunitionRemaining = 15;
            reloadingTimer = 0.0f;
            bulletTimer = 0f;

        }
    }
    public void ManualReloadingBullets()
    {
        reloadingTimer += Time.deltaTime;
        if (reloadingTimer >= manualReloadingBulletsDuration)
        {
            bulletAmmunitionRemaining = 15;
            reloadingTimer = 0.0f;
            bulletTimer = 0f;
            needManualReload = false;

        }
    }

}
