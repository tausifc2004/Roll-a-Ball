using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public float speed = 0;
    public float jumpForce = 2.0f;
    public bool isGrounded;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject retryTextObject;
    public GameObject level1Wall;
    public GameObject Player;
    public GameObject PickupSound;
    [SerializeField] private GameObject gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        level1Wall.SetActive(true);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 16)
        {
            gameOverMenu.SetActive(true);
            winTextObject.SetActive(true);
            retryTextObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    void toggleExitWall()
    {
        if (count >= 4)
        {
            level1Wall.SetActive(false);
        }
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        Vector3 jump = new Vector3(movementX, 2.0f, movementY);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (Player.transform.position.y <= -45)
        {
            Player.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            PickupSound.GetComponent<AudioSource>().Play();
            speed += 1.0f;
            count = count + 1;
            SetCountText();
            toggleExitWall();
        } else if (other.gameObject.CompareTag("JumpPickUp"))
        {
            other.gameObject.SetActive(false);
            PickupSound.GetComponent<AudioSource>().Play();
            jumpForce += 0.5f;
            count = count + 1;
            SetCountText();
            toggleExitWall();
        }
    }
}
