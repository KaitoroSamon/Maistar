using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    //[SerializeField] GameObject prefab;
    private float time;
    float charge=0;

    //[SerializeField]
    //public Transform shotPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        time += Time.deltaTime;
        if(Input.GetMouseButton(0))
        {
            if(time > 1.0f)
            {
                charge=1;
            }
            
        }

        if(Input.GetMouseButtonUp(0) && charge == 1)
        {
            Debug.Log("yeah");
        }
    }
}
