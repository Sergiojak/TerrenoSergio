using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    //Comportamiento de la bala, este script lo tiene el PREFAB de la Bala

    //Audio y partículas
    public AudioSource audioBulletHit;
    bool needAudioBulletHit;
    public ParticleSystem bulletExplosionVFX;

    //Temporizador
    float bulletTimer = 0.0f;
    //Tiempo máximo que dure la bala antes de desaparecer
    public float bulletMaxTime = 2f;


    //queremos que el timer de la bala se active cuando aparezca el objeto -> OnEnable salta cuando se activa el objeto
    void OnEnable()
    {
        bulletTimer = 0.0f;
    }
    //El OnDisable salta cuando se Desactiva el objeto 

    void Start()
    {
        needAudioBulletHit = false;
    }

    void Update()
    {
        //el temporizador va aumentando
        bulletTimer += Time.deltaTime;

        //queremos que el timer solo empiece cuando aparezca la bala, por eso tenemos el OnEnable

        if (bulletTimer >= bulletMaxTime)
        {
           //no podemos usar un Destroy(this) porque elimina el prefab
           //en su lugar usamos:
           this.gameObject.SetActive(false);

            //y hacemos que lo devuelva a la pool con el singletone y la función designada
            BulletPool.Instance.DevolverObjeto(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this);

            bulletExplosionVFX.Play();
            bulletExplosionVFX.transform.position = transform.position;

            needAudioBulletHit = true;
            if (needAudioBulletHit == true) 
            {
                audioBulletHit.Play();
                needAudioBulletHit = false;
            }
        }
    }
}