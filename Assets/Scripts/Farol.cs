using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Farol : MonoBehaviour
{
    public Light luzFarol;
    public bool aumentaLuz = false;
    public GameObject textoFinal;
    private GameObject[] objsUI;
    public GameObject finalErrText;

    public Lamps lampsScript;

    private void Update()
    {
        if(aumentaLuz == true)
        {            
            luzFarol.range += 0.1f;
            if (luzFarol.range > 6)
            {
                aumentaLuz = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(lampsScript.lamps_quantity == lampsScript.lamps_quantityMax)
            {
                aumentaLuz = true;
                StartCoroutine(final());
            }
            else
            {
                StartCoroutine(finalErr());
            }
        }
    }

    private IEnumerator final()
    {
        objsUI = GameObject.FindGameObjectsWithTag("UI");
        foreach(GameObject uiobjs in objsUI)
        {
            uiobjs.SetActive(false);
        }
        textoFinal.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MenuScene");
    }
    
    private IEnumerator finalErr()
    {
        objsUI = GameObject.FindGameObjectsWithTag("UI");
        foreach (GameObject uiobjs in objsUI)
        {
            uiobjs.SetActive(false);
        }
        finalErrText.SetActive(true);
        yield return new WaitForSeconds(3.5f);        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
