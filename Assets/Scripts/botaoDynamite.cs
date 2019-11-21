using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botaoDynamite : MonoBehaviour
{
    public Animator rock;
    public ParticleSystem[] rockParticles;
    public GameObject[] targets;
    public int playerEntered = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerEntered++;
        }
        if (playerEntered == 3)
        {
            foreach(GameObject objetos in targets)
            {
                Destroy(objetos);
                
            }
            foreach (ParticleSystem particulas in rockParticles)
            {
                particulas.Play();
            }
            GetComponent<AudioSource>().Play();            
        }
        if(playerEntered == 1)
        {
            rock.Play("explode1");
            foreach(ParticleSystem particulas in rockParticles)
            {
                particulas.Play();
                GetComponent<AudioSource>().Play();
            }
        }
        if (playerEntered == 2)
        {
            rock.Play("explode2");
            foreach (ParticleSystem particulas in rockParticles)
            {
                particulas.Play();
                GetComponent<AudioSource>().Play();
            }
        }




    }
}
