using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _powerupSpeed = 3.0f ;

    [SerializeField] // 0= triple shot; 1=speed ; 2=shield
    private int powerupID;
    

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
                switch (powerupID)
                {
                    case 0 : 
                        player.TripleShotActive() ;
                        break;
                    case 1 : 
                        player.SpeedActive();
                        break;
                    case 2 : 
                        Debug.Log("SHIELDS TO MAX"); 
                        break;
                    default:
                        Debug.Log("Default in powerupID switch statement");
                        break;
                }//end switch
            } // end null check

            Destroy(this.gameObject);
        } // end tag check

    }
} // end class

//DONE - move down at speed of 3, adjustable
//DONE - do not reuse the object (when exit screen, destroyed)
//check for collision (OnTrigger Collision)
//only collectable by Player (hint: use tags)
//only collected, destory
//DONE - last, make sure to transform into prefab- done!