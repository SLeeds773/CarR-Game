using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    public GameObject[] roads;
    // Start is called before the first frame update
    void Start()
    {


    }
     public void Build()
    {
        
        int RandomRoad = Random.Range(0,5);
        Instantiate(roads[RandomRoad], this.transform.position, Quaternion.identity);
    }
   
}
