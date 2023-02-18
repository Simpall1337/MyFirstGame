using Unity.Mathematics;
using UnityEditor.AssetImporters;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public VectorValue pos;

    public float speed;
    private float moveInput;

    Rigidbody2D rb;
    SpriteRenderer sr;

    
    public float jumpForce;
    
    public bool onGround;
    public Transform GroundCheck;
    public float checkRadius = 0.05f;
    public LayerMask Ground;

    private int jumpCount = 0;
    public int maxJumpsValue = 2;

    public Animator animator;

    private void Start()
    {
       
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Move();
        Flip();

    }

    private void Update()
    {
        

        Jump();
        CheckingGround();
    }
    private void Move()
    {
        animator.SetFloat("HorizontalMove", Mathf.Abs(moveInput));
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }
    private void Flip()
    {
        if (moveInput < 0)
            sr.flipX = true;
        else if (moveInput > 0)
            sr.flipX = false;
    }
    private void Jump()
    {
        if (onGround == false)
            animator.SetBool("Jumping", true);
        else
            animator.SetBool("Jumping", false);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround)
                rb.AddForce(Vector2.up * jumpForce);
            else if (++jumpCount < maxJumpsValue)
                rb.velocity = new Vector2(1, 4);
        }

        if (onGround)
            jumpCount = 0;
    }

    private void CheckingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position,checkRadius,Ground);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string str = currentScene.name;
        if (other.tag == "Trap")
        {
            MoneyText.Coin = 0;
            SceneManager.LoadScene(str);

        }
    }
}
 