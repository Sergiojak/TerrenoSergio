using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicBehaviours : MonoBehaviour
{
    [SerializeField]
    GameObject cinematicPlayer;

    public float cinematicPlayerSpeed = 20f;

    [SerializeField]
    private PlayableDirector playableDirector;

    bool cinematicSkipped = false;

    public void SkipCinematic(float time) // Reproducir timeline desde un frame
    {
        playableDirector.time = time;
        playableDirector.Play();  // Usar librería playable director
    }


    void Update()
    {
        transform.position += transform.forward * cinematicPlayerSpeed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && cinematicSkipped == false)
        {
            SkipCinematic(783f);
            cinematicSkipped = true;
        }
    }
}
