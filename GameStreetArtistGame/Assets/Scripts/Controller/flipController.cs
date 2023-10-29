using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flipController : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //CharacterController.Flip();
            Destroy(this.gameObject);
        }
    }
}

