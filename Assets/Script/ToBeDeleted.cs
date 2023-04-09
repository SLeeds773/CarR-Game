using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToBeDeleted : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Destroyer"))
        {
            print("should delete");
            Destroy(this.gameObject);
        }
        else
        {
            print("wrong tag????");
            print(other.tag);
            print(other.gameObject.tag);
        }
    }
}
