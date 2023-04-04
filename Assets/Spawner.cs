using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] ToSpawn;
    public int spawnNum;
    // Start is called before the first frame update
    void Start()
    {
        int SpawnOrNo = Random.Range(0,spawnNum);

        Instantiate(ToSpawn[SpawnOrNo], this.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
