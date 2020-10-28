using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class PlayerMovement : MonoBehaviour
{
    float boundrylimitx = 10.0f;
    float boundrylimitz = 10.0f;
    float movementspeed = 5.0f;
    float gModifier = 2.5f;
    bool OnGround = true;
    Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        Physics.gravity *= gModifier;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * movementspeed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * movementspeed);

        if (transform.position.z < -boundrylimitz)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -boundrylimitz);
        }
        else if (transform.position.z > boundrylimitz)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, boundrylimitz);
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * verticalInput);
        }

        if (transform.position.x < -boundrylimitx)
        {
            transform.position = new Vector3(-boundrylimitx, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > boundrylimitx)
        {
            transform.position = new Vector3(boundrylimitx, transform.position.y, transform.position.z);
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * horizontalInput);
        }

        if (Input.GetKeyDown(KeyCode.Space) && OnGround)
        {
            OnGround = false;

            playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }

        if (transform.position.z >= 10)
        {
            if (transform.position.x >= -5 && transform.position.x <= 5)
            {
                boundrylimitz = 20.0f;
                boundrylimitx = 5.0f;
            }
        }
        else if (transform.position.z <= 10)
        {
            boundrylimitx = 10.0f;
            boundrylimitz = 10.0f;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlaneA") || collision.gameObject.CompareTag("PlaneB"))
        {
            OnGround = true;
        }

        if (collision.gameObject.CompareTag("PlaneA"))
        {
            Debug.Log("PlaneA");
        }
        if (collision.gameObject.CompareTag("PlaneB"))
        {
            Debug.Log("PlaneB");
        }


    }
}
