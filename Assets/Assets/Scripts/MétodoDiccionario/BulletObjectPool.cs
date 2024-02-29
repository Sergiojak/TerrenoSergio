using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    //static para que al hacer una clase hija de otra no se copien o choquen las funciones
    //Creo una instancia de la clase para evitar que se repita
    private static BulletObjectPool instance;

    //Creo un Diccionario con la cola de objetos:  <int (que es la clave, o sea el n�de la bala), almacenamiento de balas en la cola]
    static Dictionary<int, Queue<GameObject>> pool = new Dictionary<int, Queue<GameObject>>();

    //Creo un Diccionario que ordena en el Hierachy todos los objetos de la cola: <int (la clave = Id de la bala) n�del objeto]
   //se crea una piscina en la que se meten los objetos de antes y as� se quedan ordenados dependiendo de los tipos (si tengo varios tipos de balas)
    static Dictionary<int, GameObject> parents = new Dictionary<int, GameObject>();

    //Singletone para que el script del player pueda acceder a este script 
    void Awake() 
    {
        if (instance == null) // Si la instancia de este script est� vac�a, (p.ej si no se usa el script) 
        {
            instance = this; //pues usamos este script
        }
        else // Si este script est� siendo usado en otro lado
        {
            Destroy(this); //destruimos este
        }
    }

    //Preload para precargar el n� de balas que necesitamos, creamos la bala, (en la pool) y dejarla ah� invisible.
    //(BalaPrimigenia, cantidad de balas a crear) 
    // Con esto precargamos los objetos en la escena, necesitamos el prefab y la cantidad de objetos a crear
    public static void PreLoad(GameObject bulletToPool, int amount) 
    {
        //la key o id de la bala para identificar en qu� pool est�, es decir, si est� en la piscina de (Balas, misiles, powerups,...) 
        //obtenemos esta key en cada funci�n porque si lo hacemos en el Start solo pillar�a la de la balaPrimigenia
        //GetInstanceID sirve para devolver el id del objeto, viendo que es �nico y almacenamos en id el orden en el que se cre� el objeto
        int bulletId = bulletToPool.GetInstanceID();

        //con esto creamos la piscina (est� vac�o) // Esto es lo de antes, el objeto del pool que almacenar� despu�s todas las balas
        GameObject parent = new GameObject();

        //le damos un nombre, para que la primera vez que aparezca la bala se cree una "carpeta" empty con el nombre de string
        parent.name = bulletToPool.name + "Pool";  // Lo nombramos bien

        //Metemos al GameObject dentro del diccionario parents
        parents.Add(bulletId, parent); // A�adir al diccionario, el de la jerarqu�a, para luego poder meterle las cositas dentro y estar ordenadas

        //Creamos el diccionario de la pool, que a su vez esta dentro del del parents (y la queue)
        pool.Add(bulletId, new Queue<GameObject>()); // Este sirve ahora para crear una nueva cola (en vez de stackearlas) donde almacenaremos las balas P.EJ 

        //ahora rellenamos la queue

        for (int i = 0; i < amount; i++)
        {
            CreateObject(bulletToPool);
        }

    }

    //El parametro que se le pasa es el BalaPrimigenia, ya que clonaremos el resto de objetos de ah� 
    static void CreateObject(GameObject bulletToPool) // Pasamos el prefab, porque vamos a coger ese prefab para clonarlo
    {
        // la key o id de la bala para identificar en qu� pool est�, es decir, si est� en la piscina de (Balas, misiles, powerups,...)
        int bulletId = bulletToPool.GetInstanceID(); // Instancio el ID, al coger el identificador del prefab elijo en qu� piscina lo meto

        //Intantiante: crea en el game el Clon desde la Primigenia
        GameObject bulletGo = Instantiate(bulletToPool) as GameObject;  // Hacemos copia del objeto que le pas�, del prefab

        //que al crear el Clon se haga hijo de la Primigenia y obtenga su posici�n
        bulletGo.transform.SetParent(GetParent(bulletId).transform); // Aqu� le decimos qu� piscina es, hacemos padre el ParentPool y hacemos hijo al objeto que acabamos de copiar

        //Los desactivo para que no aparezcan todos de golpe en la pantalla
        bulletGo.SetActive(false); // No queremos que se vean todos en la escena, los desactivamos

        //lo a�ado al diccionario pool que es el que uso para operar con las balas
        pool[bulletId].Enqueue(bulletGo); // Lo a�adimos en la cola, es como el push
    }

    //Funci�n que devuelve el ID del padre (se le pasa como parametro la clave del diccionario)
    //Coge el ID del padre y se lo pone al hijo, todas las balas tendr�n el ID del padre
    static GameObject GetParent(int parentID)
    {
        // Devuelve el identificador del padre y se lo pasa como un par�metro a la clave del diccionario
        //Se crea un GameObject en el que se almacena el ID del Padre
        GameObject parent; // Esto es para crear un sitio donde almacenar el ID del padre, en la jerarqu�a se ve como un empty
        //trata de coger el valorID y si no hay nada, no ocurre nada
        parents.TryGetValue(parentID, out parent); // Intenta obtener el valor asociado a la clave, si no hay, no pasa nada
        return parent;  // Le pasamos al diccionario el identificador
    }

    //Funci�n para utilizar la bala (Se le pasa como par�metro la balaPrimigenia)
    public static GameObject GetObject(GameObject bulletToPool) // Para coger los objetos y sacarlos
    {
        // la key o id de la bala para identificar en qu� pool est�, es decir, si est� en la piscina de (Balas, misiles, powerups,...)
        int bulletId = bulletToPool.GetInstanceID(); // Almacenamos el identificador del objeto para saber en qu� piscina est�

        //si el pool est� vacio== 0 -> Crea una bala.
        //Si el pool No est� vac�o -> Lo saca de la cola.
        if (pool[bulletId].Count == 0) // Si la pool est� vac�a crea un objeto, despu�s (o si no est� vac�a) lo saca de la cola
        {
            CreateObject(bulletToPool);
        }

        //Con esto sacamos el primero de la cola(el que est� preparado)
        GameObject bulletGo = pool[bulletId].Dequeue(); //Similar al pop del stack // Sacamos el primer objeto de la cola

        bulletGo.SetActive(true);

        return bulletGo; //al no ser void tiene que devolver algo, por eso el return

    }

    //Funci�n que devuelve la bala activada a la queue (balaPrimigenia (para sacar el id), Objeto que se quiere poner en la cola)
    public static void RecicleObject(GameObject bulletToPool, GameObject bulletToRecicle) // Metemos el objeto activado a la cola, necesitamos el prefab para ver qu� ID tiene y a qu� pool meterlo; y el objeto que queremos meter en la cola
    {
        // la key o id de la bala para identificar en qu� pool est�, es decir, si est� en la piscina de (Balas, misiles, powerups,...)
        int bulletId = bulletToPool.GetInstanceID(); // Miramos el ID para saber en qu� piscina ponerlo
        //Se mete el objeto que se quiere reutilizar en la cola y se desactiva
        pool[bulletId].Enqueue(bulletToRecicle);  // Lo metemos en la cola, como pushearlo

        bulletToRecicle.SetActive(false);
    }

    //Limpio la pool
    public static void ClearPool()
    {
        //este foreach, para cada var del 1er  var de variable (podemos tb poner i, x o como sea) en el 1er diccionario.value de la piscina
        //nos sirve para que si a�adimos una bala extra nos la pille, sin necesidad de nosotros poner en el for el num maximo
        foreach (var dictionary1 in pool)
        {
            Queue<GameObject> queue = dictionary1.Value;
            foreach (GameObject m_Obj in queue)
            {
                Destroy(m_Obj);
            }
            queue.Clear();
        }

        pool.Clear();

        foreach (var dictionary2 in parents)
        {
            GameObject parent = dictionary2.Value;
            Destroy(parent);
        }
        parents.Clear();
    }
}