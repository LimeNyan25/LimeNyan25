using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.LowLevelPhysics2D.PhysicsLayers;

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


    void Start()
    {
        Debug.Log(PalyerName + " 시작. 체력 " + hp);
        Debug.Log("피격 후 체력 " + (hp - 30));
        Debug.Log("달리기 속도 " + moveSpeed * 2);
        Debug.Log("10f/ 4f= " + 10f / 4f);

        rb = GetComponent<Rigidbody2D>();
        transform.position = startPosition;
    }
    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("점프!");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);


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

    }

    void Update()
    {
        transform.Translate(Vector3.right * moveInput.x * moveSpeed * Time.deltaTime);
    }
}