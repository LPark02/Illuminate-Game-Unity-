using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TextHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI textoTMP;
    private Color originalColor;

    private void Start()
    {
        originalColor = textoTMP.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textoTMP.color = Color.yellow;    
    }

    public void OnPointerExit(PointerEventData eventData)
    {     
        textoTMP.color = originalColor;                    
    }
}
