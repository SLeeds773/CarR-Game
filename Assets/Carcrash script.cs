using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carcrashscript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        MeshFilter mf = collision.transform.GetComponent<MeshFilter>();
        Vector3[] vertices = mf.mesh.vertices;

        Vector3 hitPoint = transform.InverseTransformPoint(collision.contacts[0].point);
        Vector3 hitRadius = relativeVelocity.magnitude;
        Vector3 hitDir = transform.InverseTransformDirection(-collision.contacts[0].normal);

        int i = 0;
        while (i < vertices.Length)
        {
            float distance = Vector3.Distance(vertices[i], hitPoint);
            //if(distance > hitRadius) return;

            Vector3 dir = (distance - hitPoint);
            if (dir.magnitude < radius)
            {
                float amount = 1 - dir.magnitude / radius;
                float vertMove = hitDir * amount;
                vertices[i] += vertMove;
                i++;
            }
            mesh.vertices = vertices;
            mesh.RecalculateBounds();
            mf.mesh = mesh;
        }
    }
}
