using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollisionSensor : MonoBehaviour
{

    public TextMeshProUGUI texto;

    [System.NonSerialized]
    public GameObject objectInside;

    [System.NonSerialized]
    public bool hasObjInside = false;

    public int objsCounter = 0;

    private void Start()
    {
        texto.text = "";
    }

    private void Update()
    {
        if(objectInside == null)
        {            
            texto.text = "";
            hasObjInside = false;
            objsCounter = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        objectInside = other.gameObject;
        texto.text = other.gameObject.name;
        hasObjInside = true;
        objsCounter++;
    }
    private void OnTriggerExit(Collider other)
    {
        objsCounter--;
        if (objsCounter == 0)
        {
            objectInside = null;
            texto.text = "";
            hasObjInside = false;            
        }
        
    }
}
