using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [Header("Components")]
    private CharacterController controller;
    [SerializeField] private Transform ccamera;
    [SerializeField] private LayerMask ground;
    [SerializeField] private TextUpdate coinsText;

    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private bool isGrounded;


    private float turn;
    private float height = 2.5f;
    private Vector3 moveDirection;
    private Vector3 playerVelocity;
    [SerializeField] private int coins;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, height * 0.5f + 0.3f, ground);
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }
        moveDirection = Vector3.zero;

        // Movement
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        if (direction != Vector3.zero)
        {
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + ccamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref turn, 0.01f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * -9.81f);
        }

        playerVelocity.y += -9.81f * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        controller.Move(playerVelocity * Time.deltaTime);
        controller.Move(moveDirection.normalized * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            coins++;
            coinsText.SetPlayerCoins(coins);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.tag == "Portal")
        {
            SceneManager.LoadScene("SecondLevel");
        } else if (collision.gameObject.tag == "PortalBack")
        {
            SceneManager.LoadScene("OutdoorsScene");
        }
    }
}
