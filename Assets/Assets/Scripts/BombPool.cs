using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPool : MonoBehaviour
{
    //num de bombas que vamos a crear
    [SerializeField]
    int maximoElementos = 5;
    //el elemento que queremos crear
    [SerializeField]
    GameObject bombPrefab;

    //lugar para guardarlos (la pool)
    private Stack<GameObject> bombPool;


    //Static es que solo se puede poner en 1 sitio, si pongo el script en 2 sitios diferentes da error
    public static BombPool instance;

    private void Awake()
    {
        //Singletone para usarlo desde el playerInput;
        if (BombPool.instance == null)
        {
            instance = this;
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
        bombPool = new Stack<GameObject>();
        GameObject bombClonada = null;


        //Crear un for hasta el max de elementos
        for (int i = 0; i < maximoElementos; i++)
        {
            //En balaCreada instanciamos nuestro prefab (instantiate es meter dentro del juego)
            bombClonada = Instantiate(bombPrefab);
            //balaCreada lo desactivo
            bombClonada.SetActive(false);
            //balaCreada lo meto en la pool, con pool.Push(gameObject)
            bombPool.Push(bombClonada);
        }

    }

    //ya tenemos la Pool hecha, ahora creamos función de obtener el objeto
    public GameObject ObtenerObjeto()
    {
        GameObject bomb = null;

        //si no quedan elementos en mi pool...
        if (bombPool.Count == 0)
        {
            //pues creas uno
            bomb = Instantiate(bombPrefab);
        }
        else
        {
            //pop para cogerla y la activamos
            bomb = bombPool.Pop();
            bomb.SetActive(true);
        }
        return bomb;

    }

    //Creamos función de devolver el objeto, una vez usado volverá a la pool
    public void DevolverObjeto(GameObject bombaDevuelta)
    {
        //Vuelve a guardar el objeto en la pool, con pool.Push(GameObject)
        bombPool.Push(bombaDevuelta);
        //Se desactiva de la escena
        bombaDevuelta.SetActive(false);
    }
    //Push (presionar) para meterla en la pool
    //Pop como las pringles (?) para sacarla
}
