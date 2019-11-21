using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Cine : MonoBehaviour
{
    public float delayInicio = 2f;
    public Light luz;
    public TextMeshPro texto;
    public AudioSource som;

    // Start is called before the first frame update
    void Start()
    {
        luz.range = 0;
        texto.alpha = 0f;
        StartCoroutine(comeca(delayInicio));
    }

    public IEnumerator comeca(float delay)
    {
        yield return new WaitForSeconds(delay);
        luz.range = 100f;
        texto.alpha = 255f;
        som.Play();
        yield return new WaitForSeconds(delay + 2f);
        SceneManager.LoadScene("MenuScene");
    }
}
