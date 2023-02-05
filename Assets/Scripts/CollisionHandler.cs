using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public RootController root;
    public float collideCounter = 0.0f;

    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
           case "Danger":
                Debug.Log("Danger");
                collideCounter++;

                break;
            case "Water":
                root.boost = true;
                break;
            default:
                root.boost = false;
                break;
        }
    }
}