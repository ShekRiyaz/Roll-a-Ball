using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    private Rigidbody rb;
    private float movementx;
    private float movementy;
    private int count;

    public TextMeshProUGUI countText;
    public GameObject winText;
    public GameObject loseText;
    public TextMeshProUGUI Sec;

    private float time;
    private bool isRun;

    private GameManager gameManager;  // 🔹 reference to GameManager

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.SetActive(false);
        loseText.SetActive(false);
        time = 0f;
        isRun = true;

        // Find GameManager in the scene
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementx = movementVector.x;
        movementy = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = count.ToString();
        if (count >= 12)
        {
            winText.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            isRun = false;

            // 🔹 Call GameManager to show Restart
            if (gameManager != null)
                gameManager.GameOver();
        }
    }

    private void Update()
    {
        if (isRun)
        {
            time += Time.deltaTime;
            Sec.text = "Time : " + time.ToString("F2");
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementx, 0.0f, movementy);
        rb.AddForce(movement * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            loseText.SetActive(true);

            // 🔹 Call GameManager to show Restart
            if (gameManager != null)
                gameManager.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }
}
