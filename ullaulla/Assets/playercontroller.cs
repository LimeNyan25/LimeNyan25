using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.LowLevelPhysics2D.PhysicsLayers;

using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    public string PalyerName = "Player";
    public int hp = 100;
    public Transform visual;
    bool isAlive = true;
    Vector3 startPosition;

    public float jumpPower = 8f;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    public float fallLimit = -10f;
    public int lives = 3;
    private bool isGameOver = false;
    public Vector2 airScale = new Vector2(0.8f, 1.2f);
    private float facing = 1f;
    public TMP_Text livesText;
    public GameObject gameOverPanel;
    public string nextSceneName = "Stage2";

    void Start()



    {
        Debug.Log(PalyerName + " 시작. 체력 " + hp);
        Debug.Log("피격 후 체력 " + (hp - 30));
        Debug.Log("달리기 속도 " + moveSpeed * 2);
        Debug.Log("10f/ 4f= " + 10f / 4f);
        
        if (hp >= 70)
        {
            Debug.Log("건강");
        }
        else if (hp >= 30)
        {
            Debug.Log("주의");
        }
        else
        {
            Debug.Log("위험");
        }

        rb = GetComponent<Rigidbody2D>();
        transform.position = startPosition;
        livesText.text = "LIFE " + lives;
    }
    void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded && !isGameOver)
        {
            Debug.Log("점프!");
            rb.linearVelocity = new Vector2(
            rb.linearVelocity.x, jumpPower);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
           // Debug.Log("착지");

        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
           // Debug.Log("공중");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else if (other.CompareTag("Trap"))
        {
            LoseLife();
            //루즈라이프에 낙사는 신경쓰지말고 if 목숨0일때 게임오버가 적용되는것
        }
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (moveInput.x > 0)
        {
            visual.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput.x < 0)
        {
            visual.localScale = new Vector3(-1, 1, 1);
        }
        
        if (moveInput.x > 0) { facing = 1f; }
        else if (moveInput.x < 0) { facing = -1f; }

    }

    void Update()
    {
        if (!isGameOver)
        {
            transform.Translate(Vector3.right * moveInput.x
            * moveSpeed * Time.deltaTime);
        }

        if (transform.position.y < fallLimit)
        {
            LoseLife();
            //loselife로 따로 저장해둠
        }
        
        if (isGrounded)
        {
            visual.localScale = new Vector3(facing, 1f, 1f);
        }
        else
        {
            visual.localScale = new Vector3(
            facing * airScale.x, airScale.y, 1f);
        }

    }
    void LoseLife()
    {
        lives -= 1;
        transform.position = startPosition;
        rb.linearVelocity = Vector2.zero;
        livesText.text = "LIFE " + lives;

        if (lives <= 0)
        {
            isGameOver = true;
            gameOverPanel.SetActive(true);
        }
    }

}