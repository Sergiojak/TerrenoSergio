using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    //Comportamiento de la bala, este script lo tiene el PREFAB de la Bala

    //Audio y partículas
    public AudioSource audioBombExplosion;
    bool needBombExplosion;
    public ParticleSystem bombExplosionVFX;

    public AudioSource bombFalling;


    //queremos que el timer de la bala se active cuando aparezca el objeto -> OnEnable salta cuando se activa el objeto
    void OnEnable()
    {
        bombFalling.Play();
    }
    //El OnDisable salta cuando se Desactiva el objeto 

    void Start()
    {
        needBombExplosion = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        BombPool.instance.DevolverObjeto(this.gameObject);

        bombExplosionVFX.Play();
        bombExplosionVFX.transform.position = transform.position;

        needBombExplosion = true;
        if (needBombExplosion == true)
        {
            audioBombExplosion.Play();
            audioBombExplosion.transform.position = transform.position;

            needBombExplosion = false;
        }
        bombFalling.Stop();

    }
}
