using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] //allows the value of the private var to be edited in Unity
    private float _speed = 3.5f ; // best to keep it private
    [SerializeField]
    private float _speedMultiplier = 2.0f;
    private bool _isSpeedActive = false ;

    private bool _isTripleShotActive = false ;
    [SerializeField]
    private GameObject _laserPrefab ;
    [SerializeField]
    private GameObject _tripleShotPrefab ;

    private bool _isShieldActive = false;     
    [SerializeField]
    private GameObject _shieldVisual;
    //variable ref to shield visualizer

    [SerializeField]
    private float _fireRate = 0.5f ;
    private float _canFire  = -1.0f ;


    [SerializeField]
    private int _lives = 3 ;


    private SpawnManager _spawnManager ;









    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3 ( 0 , 0 , 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The spawn manager is NULL.");
        }
    }

    // Update is called once per frame
    void Update() 
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space)  && Time.time > _canFire)
        {
            FireLaser();
        }

    } //end update

//BASIC ACTIONS ===========================================================================
    void CalculateMovement () 
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //player inputed movement
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
     
        transform.Translate(direction * _speed * Time.deltaTime); 

        //lock X&Y
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 11.3f )
        {
            transform.position = new Vector3 (-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3F) 
        {
            transform.position = new Vector3(11.3F, transform.position.y, 0); 
        }

    } // end calculatemovement
    void FireLaser () 
    {
        _canFire = Time.time + _fireRate ; 

        if (_isTripleShotActive == true )
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else 
        {
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y+1.05f,0), Quaternion.identity ) ;
        }
    }

    public void Damage() //has to be accessible by other gameobjects
    {
        if (_isShieldActive == true)
        {
            // Debug.Log("Shields deactivated");
            _isShieldActive = false ;
            _shieldVisual.SetActive(false);
            return;
        }

        _lives--;
        
        if (_lives < 1)
        {
            _spawnManager.onPlayerDeath();
            Destroy(this.gameObject);
        }
    }


//POWERUPS=================================================================================
    public void TripleShotActive()
    {
        _isTripleShotActive = true ;
        StartCoroutine(TripleShotPowerDownRoutine() );
    }

    IEnumerator TripleShotPowerDownRoutine() 
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false ;
    }

    public void SpeedActive()
    {
        _isSpeedActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedPowerDownRoutine() );
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedActive = false; 
        _speed = _speed /_speedMultiplier;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisual.SetActive(true);
    }
    
} //end Class




//NOTES ==========================================================================================================
// Debug.Log("Space key pressed"); //console log!

        //public or private reference
        //data types (int, float, bool, string)
        //every var has a name
        //optional = value assigned

        // public float speed ; //default value of zero
        // public float speed = 3.5f ; //the f is required!
        // [SerializeField] //allows the value of the private var to be edited in Unity
        // private float _speed = 3.5f ; // best to keep it private

        //float because horizontal axes input returns a float value
        // public float horizontalInput ;  // gets moved to Update 

        // Start is called before the first frame update



    // //ADDING BOUNDARIES =========================================================================
        // float horizontalInput = Input.GetAxis("Horizontal");
        // float verticalInput = Input.GetAxis("Vertical");

        // Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        // transform.Translate(direction * _speed * Time.deltaTime);

        // //boundary if/else statements
        // //lock verticals, no clamp
        // if (transform.position.y >= 0) 
        // {
        //     transform.position = new Vector3(transform.position.x, 0 , 0);
        // } 
        // else if (transform.position.y <= -3.8f)
        // {
        //     transform.position = new Vector3(transform.position.x, -3.8f, 0);
        // }

        // //lock horizontals
        // if (transform.position.x >= 11.3f )
        // {
        //     transform.position = new Vector3 (-11.3f, transform.position.y, 0);
        // }
        // else if (transform.position.x <= -11.3F) 
        // {
        //     transform.position = new Vector3(11.3F, transform.position.y, 0);
        // }



    //ADDING PLAYER MOVEMENTS ===================================================================
        //to have user controlle movement
        //declare Inputs vars first 
        // float horizontalInput = Input.GetAxis("Horizontal");
        // float verticalInput = Input.GetAxis("Vertical");

        // transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime) ;
        // transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime) ;

        //alternative implementations
        //First, oneliner
        // transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);
        // Here, only one new vector is being created. As such, it is more optimized
        // than the two written above. 
        
        //second , oneliner cleaned to be easy to read
        // Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        // transform.Translate(direction * _speed * Time.deltaTime);

        // Below is to have uncontrolled movement
        // transform.Translate(Vector3.left * 5 * Time.deltaTime);
        // lets use the new speed variable
        // transform.Translate(Vector3.left * _speed * Time.deltaTime);

        // horizontalInput = Input.GetAxis("Horizontal");

    //offshoot and firing lasers =======================================
         //alternative for offshoot
        // Instantiate(_laserPrefab, transform.position+ new Vector3(0,0.8f,0), Quaternion.identity );

    //speed boosting
    //below works fine, but we can remove the if statement as seen above

      //IF SPEEDBOOST ACTIVE, have speed be 8x, wait 5 seconds, go back to default
        // if (_isSpeedActive == true) 
        // {
        //     transform.Translate(direction * _speedBoosted * Time.deltaTime); 
        // }
        // else 
        // {
        //     transform.Translate(direction * _speed * Time.deltaTime);
        // }