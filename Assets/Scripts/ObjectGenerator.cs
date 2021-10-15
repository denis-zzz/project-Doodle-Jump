using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    //Hypothèse : brown platform est toujours le dernier élement
    public GameObject[] platforms;
    public GameObject[] obstacles;
    public GameManager gm;
    float current_y = -3;
    public float spawnDelay;
    bool previous_is_brown = false;
    bool previous_is_obs = false;
    private GameObject[] to_destroy;
    public GameObject spring;
    private GameObject created_platform;
    private GameObject created_spring;

    void Start()
    {
        Generate(20);
    }

    void Update()
    {
        if (!gm.isPaused)
        {
            to_destroy = GameObject.FindGameObjectsWithTag("destroy");
            if (to_destroy.Length < 20)
                Generate(1);
        }
    }

    public void Generate(int number)
    {

        for (int i = 0; i < number; i++)
        {

            float x = Random.Range(-3f, 3f);
            float y = Random.Range(0.5f, 1.5f);

            Vector3 pos = new Vector3(x, current_y, 0);

            // 1/8 de chance de générer un obstacle si le jeu est en mode difficile
            if ((gm.hard == true) && (Random.Range(1, 9) == 1) && !(previous_is_obs))
            {
                GameObject obstacle = obstacles[Random.Range(0, obstacles.Length)];
                Instantiate(obstacle, pos, Quaternion.identity);
                previous_is_brown = false;
                previous_is_obs = true;
            }
            else
            {
                // 1/8 de chance de générer un brown platform
                if (Random.Range(1, 9) == 1 && !(previous_is_brown))
                {
                    Instantiate(platforms[platforms.Length - 1], pos, Quaternion.identity);
                    previous_is_brown = true;
                    previous_is_obs = false;
                }
                else
                {
                    GameObject platform = platforms[Random.Range(0, platforms.Length - 1)];
                    created_platform = Instantiate(platform, pos, Quaternion.identity);
                    previous_is_brown = false;
                    previous_is_obs = false;

                    // 1/20 de chance de générer un ressort
                    if (Random.Range(1, 21) == 1)
                    {
                        Vector3 spring_pos = new Vector3(pos.x + Random.Range(-0.5f, 0.5f),
                        pos.y + 0.1f, 0);
                        created_spring = Instantiate(spring, spring_pos, Quaternion.identity);
                        created_spring.transform.parent = created_platform.transform;
                    }
                }
            }

            current_y += y;

        }
    }

}
