using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private GameObject player;

    private int score = 0;
    private float max_height = 0;
    public Text score_txt;
    public bool isActive;
    public GameObject gameover_menu;
    private AudioSource source;
    public bool hard = false;
    GameObject[] to_destroy;
    public bool isPaused;

    void Awake()
    {
        player = GameObject.Find("Doodler");
        source = GetComponent<AudioSource>();
        isActive = true;
        isPaused = false;
    }


    void FixedUpdate()
    {
        if (isActive)
        {
            if (player.transform.position.y > max_height)
            {
                max_height = player.transform.position.y;
                score = (int)max_height * 50;
                score_txt.text = score.ToString();
                if (score > 3000)
                    hard = true;
            }

            Vector3 bottom_left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            if (player.transform.position.y < bottom_left.y - 2)
            {
                StartCoroutine(fall_game_over());
            }
        }

    }

    private IEnumerator fall_game_over()
    {
        source.Play();
        isActive = false;
        yield return new WaitWhile(() => source.isPlaying);
        gameOver();
    }

    public void gameOver()
    {
        isActive = false;
        gameover_menu.SetActive(true);
        Destroy(player);
        to_destroy = GameObject.FindGameObjectsWithTag("destroy");
        foreach (GameObject go in to_destroy)
        {
            Destroy(go);
        }
    }

    public void PauseUnpauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1;
        }
    }
}
