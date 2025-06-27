using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move down at 4 meters per second
        
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // if bottom of the screen 


        // respawn at top with new random x position

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX,7, 0);
        }



        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        //if other is Player
        //Damage the player first
        //Destroy us

        if (other.tag == "Player")
        {
            //damage player
            Player player = other.transform.GetComponent<Player>();
            //other.transform.GetComponent<Player>().Damage(); // snippet to call or get the method from the other component

            if (player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
        }



        // if other is laser
        //laser
        // destroy us

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
