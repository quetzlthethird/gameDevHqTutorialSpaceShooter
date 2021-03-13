using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _powerupSpeed = 3.0f ;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {  
       transform.Translate(Vector3.down * _powerupSpeed * Time.deltaTime)  ;

       if (transform.position.y < -5.0f )
       {
           Destroy(this.gameObject);
       }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.TripleShot() ;
            }

            Destroy(this.gameObject);
        }
    }
} // end class

//DONE - move down at speed of 3, adjustable
//DONE - do not reuse the object (when exit screen, destroyed)
//check for collision (OnTrigger Collision)
//only collectable by Player (hint: use tags)
//only collected, destory
//DONE - last, make sure to transform into prefab- done!