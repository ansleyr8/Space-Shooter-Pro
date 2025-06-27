using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public or private reference
    // data type (int, float, bool, string)
    // every variable has a name
    // optional value assigned
    [SerializeField]
    private float _speed = 3.5f; //(use underscore to identify private)
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float  _fireRate = 0.5f;
    private float _canFire = -1f; // weapons cooldown

    [SerializeField]
    private int _lives = 3;

    
    private Spawn_Manager _spawnManager;


    void Start()
    {
        // take the current position and assign it as start position
        // take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {   
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

        
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); //input code
        float verticalInput = Input.GetAxis("Vertical");

        
        //new Vector3(1, 0, 0) * 0 * 3.5f * real time (distributive property of maths)
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
    
        //optimised code
        transform.Translate(new Vector3(horizontalInput,verticalInput,0) * _speed * Time.deltaTime);

        //variable for userinput

        //Vector3 direction = new Vector3(horizontalInput,verticalInput,0);
        //transform.Translate(direction * _speed * Time.deltaTime);


        //if playerposition on the y is > 0
        // y position = 0
        //else if position on the y is less than -3.8f
        //y pos = -3.8f

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if(transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x,-3.8f,0);

            //this will run
        }

        //transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0); // clamping method demo


        // if playerposition on the x is > 11
        // x position = -11
        // else if position on the x is less than -11
        // x pos = 

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
        


    }

    void FireLaser()
    {
        // if i hit the space key 
        // spawn gameObject
        //if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire) //implementation of Time.time
        //{
            _canFire = Time.time + _fireRate; // used to delay time (formula)
            Instantiate(_laserPrefab, transform.position + new Vector3(0,1.05f,0), Quaternion.identity);
            

        //}
    }
    public void Damage()
    {
        _lives -= 1;

        // check if dead
        // destroy us

        if (_lives < 1)
        {
            // Communicate with Spawn Manager
            _spawnManager.OnPlayerDeath();                     //Find the GameObject. then Get Component
            // Let them know to stop spawning
            Destroy(this.gameObject);
        }
    }

    
}
