using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _laserSpeed = 8.0f ;

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        if (transform.position.y > 8.0f )
        {
           DestroyGameObject();       
        }
    
    } // end update

} //end class 


// can add the function and then call it in update, or simply have it in update