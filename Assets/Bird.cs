using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public Vector3 userDirection = Vector3.one;
    public int movespeed;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 6f);
    }

    // Update is called once per frame
    void Update()
    {
         transform.Translate(userDirection * movespeed * Time.deltaTime);
    }
}
