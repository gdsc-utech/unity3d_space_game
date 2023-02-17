using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    public GameObject asteroid;
    public int amount;
    public int radius;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < amount; i++)
        {
            // generate a random position in a sperical area
            var spawnPosition = Random.insideUnitSphere * radius;

            GameObject obj = Instantiate(asteroid, spawnPosition, Quaternion.identity);
            // assign a random size
            obj.transform.localScale = new Vector3(1, 1, 1) * Random.Range(0.05f, 0.1f);
            // add a random force to the asteroid
            obj.GetComponent<Rigidbody>().AddForce(
                Random.Range(0, 10),
                Random.Range(0, 10),
                Random.Range(0, 10),
                ForceMode.Impulse
            );
        }
    }

}
