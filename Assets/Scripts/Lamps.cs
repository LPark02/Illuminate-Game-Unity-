using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lamps : MonoBehaviour
{
    private GameObject[] lampsObj;

    [System.NonSerialized]
    public int lamps_quantityMax;

    [System.NonSerialized]
    public int lamps_quantity = 0;

    public TextMeshProUGUI textoQuantMax;
    public TextMeshProUGUI textoQuant;
    // Start is called before the first frame update
    void Start()
    {
        lampsObj = GameObject.FindGameObjectsWithTag("Lamp");
        lamps_quantityMax = lampsObj.Length;
        textoQuantMax.text = lamps_quantityMax.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        textoQuant.text = lamps_quantity.ToString();
    }
}
