
using UnityEngine;

public class SlimeType : MonoBehaviour
{
    private StateMachine _stateMachine;
    private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private RaycastHit2D hit;

    public EnemyData Data;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Data.rb = GetComponent<Rigidbody2D>();
        Data.anim = GetComponent<Animator>();
        Data.transform = GetComponent<Transform>();
        Data.target = GameObject.FindGameObjectWithTag("Player");
        Data.hit = hit;


        Data.spawnPoint = Data.rb.position;

        _stateMachine = new StateMachine();
        _stateMachine.AddState(new PatrolState(Data, _stateMachine));
        _stateMachine.AddState(new ChaseState(Data, _stateMachine));
        _stateMachine.ChangeState<PatrolState>();


    }

    // Update is called once per frame
    void Update()
    {
        hit = Physics2D.Raycast(wallCheck.position, Vector2.right * transform.localScale.x, 5f);


        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            if (!(_stateMachine.CurrentState is ChaseState))
            {
                _stateMachine.ChangeState<ChaseState>();

            }
        }
        else
        {
            if (!(_stateMachine.CurrentState is PatrolState))
            {
                _stateMachine.ChangeState<PatrolState>();

            }
        }


        Debug.DrawRay(wallCheck.position, Vector2.right * transform.localScale.x * 5f, Color.red);


        _stateMachine.Update();
    }

    void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
        PhysicAdapt();
    }
    void PhysicAdapt()
    {
        if (isGrounded())
        {
            rb.gravityScale = 0f;
        }
        else
        {
            rb.gravityScale = 4f;
        }
    }



    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
}
