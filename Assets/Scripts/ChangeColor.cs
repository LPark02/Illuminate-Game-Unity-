using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Material thisMaterial;
    public Material materialToChange;
    public float tempoVoltar = 1f;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<Renderer>().material = materialToChange;
            GetComponent<AudioSource>().Play();
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        StartCoroutine(voltaMaterial(tempoVoltar));
    }
    private IEnumerator voltaMaterial(float tempo)
    {
        yield return new WaitForSeconds(tempo);
        GetComponent<Renderer>().material = thisMaterial;
    }
}
