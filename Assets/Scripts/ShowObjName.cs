using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowObjName : MonoBehaviour
{
    public TextMeshProUGUI objName;
    public Image background;
    
    public void OnMouseDown()
    {        
        Vector3 objPos = Camera.main.WorldToScreenPoint(this.transform.position);
        objName.transform.position = objPos;
        objName.text = transform.name;
        Color tempColor = background.color;
        tempColor.a = 0.35f;
        background.color = tempColor;

    }

    public void OnMouseExit()
    {
        objName.text = "";
        Color tempColor = background.color;
        tempColor.a = 0;
        background.color = tempColor; 
    }
}
