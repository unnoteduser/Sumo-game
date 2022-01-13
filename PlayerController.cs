using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRb; 
    private GameObject focalPoint;
    public GameObject powerupIndicator;
    public float speed = 5.0f;
    float powerupStrenght = 15.0f;
    public bool hasPowerup = false;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal point");
    }
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other) // use OnTrigger when we want to know that sth is collide with each other
    {
        if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutin());// start PowerupCountdownRoutin thread below
        }
    }
    IEnumerator PowerupCountdownRoutin() //enable countdown timer outside our update loop
    {
         yield return new WaitForSeconds(7);// use keyword yield and WaitForSec method to wait before we do smt
         hasPowerup = false; // this happen when countdown over
         powerupIndicator.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision) // use OnCollision when we work with physics
    {   
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);// direction defines through subtraction enemy position from player position
            
            enemyRb.AddForce(awayFromPlayer * powerupStrenght, ForceMode.Impulse);
            Debug.Log("Collided with" + collision.gameObject.name + "with powerup set to" + hasPowerup); //concatenation
        }
    }
}
