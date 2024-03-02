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

    //Temporizador
    float bombTimer = 0.0f;
    //Tiempo máximo que dure la bala antes de desaparecer
    public float bombMaxTime = 5f;


    //queremos que el timer de la bala se active cuando aparezca el objeto -> OnEnable salta cuando se activa el objeto
    void OnEnable()
    {
        bombTimer = 0.0f;
    }
    //El OnDisable salta cuando se Desactiva el objeto 

    void Start()
    {
        needBombExplosion = false;
    }

    void Update()
    {
        //el temporizador va aumentando
        bombTimer += Time.deltaTime;

        //queremos que el timer solo empiece cuando aparezca la bala, por eso tenemos el OnEnable

        if (bombTimer >= bombMaxTime)
        {
            //no podemos usar un Destroy(this) porque elimina el prefab
            //en su lugar usamos:
            this.gameObject.SetActive(false);

            //y hacemos que lo devuelva a la pool con el singletone y la función designada
            BombPool.instance.DevolverObjeto(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        Destroy(this);

        bombExplosionVFX.Play();
        bombExplosionVFX.transform.position = transform.position;

        needBombExplosion = true;
        if (needBombExplosion == true)
        {
            audioBombExplosion.Play();
            audioBombExplosion.transform.position = transform.position;

            needBombExplosion = false;
        }
    }
}
