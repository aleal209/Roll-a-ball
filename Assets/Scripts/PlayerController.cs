using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float jump = 0;
    public int doublejump = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private Rigidbody rb;
    private int count;
    private bool isGrounded;
    private float movementX;
    private float movementY;
    private float movementUp;
    private int changedoublejump;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        changedoublejump = doublejump;
        winTextObject.SetActive(false);
    }

    // Checks if a movement value is being pressed
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump(InputValue jumpValue)
    {
        // Debug.Log(jumpValue);
        if (isGrounded == true)
        {
            Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
            rb.AddForce(up * jump, ForceMode.Impulse);
        }
        else if (changedoublejump > 0)
        {
            Vector3 up = new Vector3(0.0f, 1.0f, 0.0f);
            rb.AddForce(up * jump, ForceMode.Impulse);
            changedoublejump -= 1;
        }
    }

    // Checks if player has collided with another object
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    // Sets the count text
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 13)
        {
            winTextObject.SetActive(true);
        }
    }
    // Checks at a specific time interval
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, movementUp, movementY);
        rb.AddForce(movement * speed);
        if(transform.position.y == 0.5f)
        {
            isGrounded = true;
            changedoublejump = doublejump;
        }
        else
        {
            isGrounded = false;
        }
    }
}
