using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class MenuButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{    

    public TextMeshProUGUI textoTMP;


    public void PlayButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void HelpButton()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textoTMP.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textoTMP.color = Color.white;  
    }
}
