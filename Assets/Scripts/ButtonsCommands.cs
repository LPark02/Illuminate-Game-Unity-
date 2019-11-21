using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonsCommands : MonoBehaviour, IPointerClickHandler
{
    public GameObject painel;
    public GameObject[] painelsToHide;
    bool panelIsOpen = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (panelIsOpen)
        {            
            painel.SetActive(false);
            panelIsOpen = false;
        }
        else
        {
            painel.SetActive(true);
            panelIsOpen = true;
            foreach(GameObject gameobj in painelsToHide)
            {
                gameobj.SetActive(false);
            }
        }        
    }

}
