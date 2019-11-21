using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChanges : MonoBehaviour
{
    public float lightIntensity;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Light>().intensity = lightIntensity;
    }

}
