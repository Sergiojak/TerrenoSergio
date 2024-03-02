using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class EndGameScreen : MonoBehaviour
{
    //alternativa: con GameObject[] enemies;

    //gameobject que contiene a los enemigos como hijos
    [SerializeField]
    GameObject enemies;

    //texto en pantalla de cantidad de enemigos
    [SerializeField]
    TextMeshProUGUI enemyCountText;

    
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject playerTurret;

    [SerializeField]
    AudioSource backgroundInGameMusic;

    //hay que hacer el canvas con GameObject para poder usar el .activeself y ponerlo en el if, si no pues el audio no funcionará
    public GameObject  winBackgroundImageCanvas;

    //como el Lose se hace desde el OnCollisionEnter del jugador podemos usar el aplha (tb se puede usar el GameObject active y tal)
    public CanvasGroup loseBackgroundImageCanvas;

    public AudioSource winMusic;
    public AudioSource youWinSound;


    public AudioSource loseMusic;
    public bool needLoseMusic = false;

    public AudioSource youLoseSound;
    public bool needYouLoseSound = false;

    void Update()
    {
        //alternativa que quizás pesa menos al tener menos GameObjects y Emptys:
        //enemies = GameObject.FindGameObjectsWithTag("Enemy")
        //en el if poniendo if(enemies == 0 && winBackgroundImageCanvas.activeSelf == false)
        //luego para que se muestre sería   enemyCountText.text = "Enemies: " + enemies.Length.ToString();

        enemyCountText.text = "Enemies: " + enemies.transform.childCount; //transform.childcount es la cantidad de hijos que tiene el gameobject (en este caso es el enemy)

        if (enemies.transform.childCount == 0 && winBackgroundImageCanvas.activeSelf == false)
        {
            Cursor.lockState = CursorLockMode.None;
            winMusic.Play();
            backgroundInGameMusic.Stop();
            player.SetActive(false);
            winBackgroundImageCanvas.SetActive(true);
            youWinSound.Play();
        }
    }

    public void LoseGame()
    {
        loseBackgroundImageCanvas.alpha = 1f;
        Cursor.lockState = CursorLockMode.None;
        playerTurret.SetActive(false);
        backgroundInGameMusic.Stop();

        needLoseMusic = true;
        if(needLoseMusic == true)
        {
            loseMusic.Play();
        }
        needYouLoseSound = true;
        if (needYouLoseSound == true)
        {
            youLoseSound.Play();
        }
    }
}
