using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public Transform prefabGrid;
    public int size = 1;
    public float distanceTile;
    public float height;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j< size; j++)
            {
                Instantiate(prefabGrid, new Vector3(i * distanceTile + transform.position.x, transform.position.y, j * distanceTile + transform.position.z), Quaternion.identity, transform.parent);
            }
            
        }
        
    }

}
