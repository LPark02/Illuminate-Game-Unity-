using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public Lamps lampScript;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
            lampScript.lamps_quantity = lampScript.lamps_quantity + 1;
            Destroy(gameObject);
        }
        
    }

}
