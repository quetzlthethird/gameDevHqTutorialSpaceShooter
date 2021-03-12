using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _enemySpeed = 4.0f ;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime) ; 
        
        if (transform.position.y < -5.0f) 
        {
            float randomX = Random.Range(-11.0f, 11.0f) ; 
            transform.position = new Vector3( randomX , 10.0f , 0) ;
        }
    }//end update

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            // Debug.Log("playeredddd");

            Player player = other.transform.GetComponent<Player>(); 

            if (player != null)
            {
                player.Damage();
            }
            
            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")
        {
            // Debug.Log("laseredddd");
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

    }//end OnTriggerEnter


} //end class


//NOTES ==================================================
        // Debug.Log("Hit: " + other.transform.name);
        // other pulled from the function
        // transform grabs the object and then .name for the name key

        // if (other.gameObject == GameObject.FindWithTag("Player") )
        // if (other.tag == "Player")
        //both do the same, but second is easier to read

    //     Player player = other.transform.getComponent<Player>(); 
    //         if (player != null)
    //         {
    //             player.Damage();
    //         }
       
    //    other.transform.getComponent<Player>().Damage();
    //    One liner, but doesn't handle situations in which the component simply doesn't exist. 
    //    So you refactor the getcomponent into a variable, but the null check in, then access the Damage function