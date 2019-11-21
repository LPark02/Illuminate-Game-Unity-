using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
public class Resetar : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {      
        print("RESET");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
