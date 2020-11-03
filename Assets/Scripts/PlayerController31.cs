using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController31 : MonoBehaviour
{

    float speed = 15.0f;
    float maxLimit = 20f;

    bool Grounded = false;

    float gravityModifier = 2.0f;

    bool isPressed = false;

    Rigidbody playerRB;
    Renderer playerRenderer;

    public Material[] playerMaterials;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRenderer = GetComponent<Renderer>();
        Grounded = true;

        Physics.gravity *= gravityModifier;


    }

    // Update is called once per frame
    void Update()
    {
        PlayerJump();

        //User Interaction inputs
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * speed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * speed);

        if (transform.position.z < -maxLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -maxLimit);
        }
        else if (transform.position.z > maxLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxLimit);
        }

        if (transform.position.x < -maxLimit)
        {
            transform.position = new Vector3(-maxLimit, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > maxLimit)
        {
            transform.position = new Vector3(maxLimit, transform.position.y, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Grounded = true;

            playerRenderer.material.color = playerMaterials[0].color;
        }
    }

    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            playerRB.AddForce(Vector3.up * 20, ForceMode.Impulse);
            Grounded = false;

            playerRenderer.material.color = playerMaterials[1].color;
        }
    }
}
