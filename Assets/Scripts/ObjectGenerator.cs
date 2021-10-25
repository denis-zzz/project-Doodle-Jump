using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject[] platforms;
    public GameObject[] obstacles;
    public GameManager gm;
    float current_y = -3;
    public float spawnDelay;
    bool previous_is_brown = false;
    bool previous_is_obs = false;
    private GameObject[] to_destroy;
    public GameObject spring;
    public GameObject jetpack;
    public GameObject haticopter;
    private GameObject created_platform;
    private GameObject created_object;
    private GameObject platform_to_create;

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
            if ((gm.hard == true) && (Random.Range(1, 9) == 1) && !(previous_is_obs) && !(previous_is_brown))
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
                    platform_to_create = platforms.Where(obj => obj.name == "Platform_brown").SingleOrDefault();
                    Instantiate(platform_to_create, pos, Quaternion.identity);
                    previous_is_brown = true;
                    previous_is_obs = false;
                }
                else
                {
                    int rand_platform = Random.Range(1, 3);
                    if (rand_platform == 1)
                    {
                        platform_to_create = platforms.Where(obj => obj.name == "Platform_green").SingleOrDefault();
                    }
                    else
                    {
                        platform_to_create = platforms.Where(obj => obj.name == "Platform_blue").SingleOrDefault();
                    }
                    created_platform = Instantiate(platform_to_create, pos, Quaternion.identity);
                    previous_is_brown = false;
                    previous_is_obs = false;

                    // 1/20 de chance de générer un ressort, jetpack ou helicochapeau
                    if (Random.Range(1, 21) == 1)
                    {
                        Vector3 _pos = new Vector3(pos.x + Random.Range(-0.5f, 0.5f),
                        pos.y + 0.1f, 0);

                        int rand_object = Random.Range(1, 4);
                        if (rand_object == 1)
                        {
                            created_object = Instantiate(spring, _pos, Quaternion.identity);
                        }
                        else if (rand_object == 2)
                        {
                            created_object = Instantiate(haticopter, _pos, Quaternion.identity);
                        }
                        else
                        {
                            created_object = Instantiate(jetpack, _pos, Quaternion.identity);
                        }
                        created_object.transform.parent = created_platform.transform;
                    }
                }
            }
            current_y += y;
        }
    }
}
