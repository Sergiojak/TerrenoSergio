using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    //num de balas que vamos a crear
    [SerializeField]
    int maximoElementos = 10;
    //el elemento que queremos crear
    [SerializeField]
    GameObject balaPrefab;

    //lugar para guardarlos (la pool)
    private Stack<GameObject> pool;


    //Static es que solo se puede poner en 1 sitio, si pongo el script en 2 sitios diferentes da error
    public static BulletPool Instance;

    private void Awake()
    {
        //Singletone para usarlo desde el playerInput;
        if (BulletPool.Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        //que al iniciar el juego se haga el Setup de la Pool
        SetupPool();
    }

    //Creando objetos dentro de la pool
    void SetupPool()
    {
        //esta es la pila, siempre cogemos el último que hemos puesto en el stack, el de más arriba de la pila
        pool = new Stack<GameObject>();
        GameObject balaCreada = null;


        //Crear un for hasta el max de elementos
        for (int i = 0; i < maximoElementos; i++)
        {
            //En balaCreada instanciamos nuestro prefab (instantiate es meter dentro del juego)
            balaCreada = Instantiate(balaPrefab);
            //balaCreada lo desactivo
            balaCreada.SetActive(false);
            //balaCreada lo meto en la pool, con pool.Push(gameObject)
            pool.Push(balaCreada);
        }

    }

    //ya tenemos la Pool hecha, ahora creamos función de obtener el objeto
    public GameObject ObtenerObjeto()
    {
        GameObject bullet = null;

        //si no quedan elementos en mi pool...
        if (pool.Count == 0)
        {
            //pues creas uno
            bullet = Instantiate(balaPrefab);
        }
        else
        {
            //pop para cogerla y la activamos
            bullet = pool.Pop();
            bullet.SetActive(true);
        }
        return bullet;

    }

    //Creamos función de devolver el objeto, una vez usado volverá a la pool
    public void DevolverObjeto(GameObject SphereDevuelta)
    {
        //Vuelve a guardar el objeto en la pool, con pool.Push(GameObject)
        pool.Push(SphereDevuelta);
        //Se desactiva de la escena
        SphereDevuelta.SetActive(false);
    }
    //Push (presionar) para meterla en la pool
    //Pop como las pringles (?) para sacarla
}
