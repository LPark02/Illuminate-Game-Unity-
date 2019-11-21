using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    private Transform playerPos;
    private Rigidbody rgbdPlayer;

    public float jumpForce = 178f;     

    private Quaternion startingRotation;
    public float speed = 10;

    private GameObject endRotation;


    public float groundCheckDistance = 5f;
    public bool isGrounded = false;

    public CollisionSensor objFrente;
    public CollisionSensor objAtras;
    public CollisionSensor objDentro;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").transform;
        rgbdPlayer = playerPos.GetComponent<Rigidbody>();
        startingRotation = this.transform.rotation;
        endRotation = new GameObject();

    }

    // Update is called once per frame
    void Update()
    {

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, endRotation.transform.rotation, Time.deltaTime * speed);
    }

    public void andar()
    {
        if (objFrente.hasObjInside)
        {
            if (objFrente.objectInside.tag != "Wall")
            {
                rgbdPlayer.AddRelativeForce(new Vector3(0, jumpForce, jumpForce));
            }
            else
            {
                rgbdPlayer.AddRelativeForce(new Vector3(0, jumpForce, 0));
            }
        }
        else
        {
            rgbdPlayer.AddRelativeForce(new Vector3(0, jumpForce, jumpForce));
        }        
    }

    public void voltar()
    {
        if (objAtras.hasObjInside)
        {
            if (objAtras.objectInside.tag != "Wall")
            {
                rgbdPlayer.AddRelativeForce(new Vector3(0, jumpForce, -jumpForce));
            }
            else
            {
                rgbdPlayer.AddRelativeForce(new Vector3(0, jumpForce, 0));
            }
        }
        else
        {
            rgbdPlayer.AddRelativeForce(new Vector3(0, jumpForce, -jumpForce));
        }        
    }
    public void viraEsquerda()
    {
        endRotation.transform.Rotate(Vector3.up, -90, Space.World);
    }
    public void viraDireita()
    {
        endRotation.transform.Rotate(Vector3.up, 90, Space.World);
    }

}
