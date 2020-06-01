using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> points;
    [SerializeField] private GameObject guyPrefab;

    private float batchCooldown = 2f;
    private int quantity = 6;

    private float cooldown = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            return;
        }

        cooldown = batchCooldown;

        for (int i = 0; i < quantity; i++)
        {
            if (GameManager.instance.GetGuysCount() >= 25) return;

            int randomStart = Random.Range(0, points.Count - 1);
            int randomEnd = Random.Range(0, points.Count - 1);

            GameObject guyCreated = Instantiate(guyPrefab, points[randomStart].position, Quaternion.identity);
            Guy guy = guyCreated.GetComponent<Guy>();
            guy.SetDestination(points[randomEnd].position);

        }
        


    }
}
