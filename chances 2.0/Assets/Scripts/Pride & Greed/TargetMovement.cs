using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float dashSpeed = 5f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
    private float currentSpeed;
    private bool isDashing = false;
    private float dashTime;
    private float dashCooldownTime;

    public GameObject miniGame;

    public HealthSystem miniGameLife;
    public CameraSwitch cameraSwitch;
    public GameManagerGreedPride gameManagerGreedPride;
    public GameManagerWrath gameManagerWrath;
    public SkillOption skillOption;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = moveSpeed;
    }

    void Update()
    {
        HandleMovement();
        HandleDash();
    }


    void OnEnable()
    {
        miniGameLife.health = 100;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (miniGameLife.health != 0)
        {
            if (other.CompareTag("Arrow"))
            {
                int damage = Random.Range(15, 25);
                miniGameLife.TakeDamage(damage);
            }
        }
        //lose game
        else if (miniGameLife.health <= 0)
        {
            //return camera to mainscene
            //disable minigame
            //reset mini game
            cameraSwitch.FightScene();
            miniGame.SetActive(false);
            miniGameLife.health = 100;

            //return UI's or call function
            if (gameManagerGreedPride != null)
            {
                gameManagerGreedPride.ReturnAll();
                //player takes damage
                //shield Concept
                gameManagerGreedPride.PlayerTakeDamage();

            }
            if (gameManagerWrath != null)
            {
                gameManagerWrath.ReturnAll();
                gameManagerWrath.PlayerTakeDamage();
            }
        }
    }


    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(moveX, moveY) * currentSpeed;
        if (rb.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && Time.time >= dashCooldownTime)
        {
            isDashing = true;
            currentSpeed = dashSpeed;
            dashTime = Time.time + dashDuration;
        }

        if (isDashing && Time.time >= dashTime)
        {
            isDashing = false;
            currentSpeed = moveSpeed;
            dashCooldownTime = Time.time + dashCooldown;
        }
    }
}

