using Unity.VisualScripting;
using UnityEngine;

public class BeeType : MonoBehaviour
{
    private StateMachine _stateMachine;
    private Rigidbody2D rb;
    public EnemyData Data;
   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _stateMachine = new StateMachine();
        rb = GetComponent<Rigidbody2D>();
        Data.rb = GetComponent<Rigidbody2D>();
        Data.anim = GetComponent<Animator>();
        Data.transform = GetComponent<Transform>();
        Data.spawnPoint = transform.position;
        Data.target = GameObject.FindGameObjectWithTag("Player");
        

        _stateMachine.AddState(new FlyingState(Data, _stateMachine));
        _stateMachine.AddState(new PFlyingState(Data, _stateMachine));
        _stateMachine.AddState(new FShootState(Data, _stateMachine));

        _stateMachine.ChangeState<PFlyingState>();
    }

    // Update is called once per frame
    void Update()
    {
        
        SelectState();


        Debug.Log(_stateMachine.CurrentState);


        _stateMachine.Update();
    }
    void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }   

    void SelectState()
    {
        float distance = Vector2.Distance(Data.target.transform.position, transform.position);
        if (_stateMachine.CurrentState is null)
        {
            _stateMachine.ChangeState<PFlyingState>();
        }

        if(distance <= Data.attackRange)
        {
            _stateMachine.ChangeState<FShootState>();
        }
        else if ( distance <= Data.maxDistance)
        {
            _stateMachine.ChangeState<FlyingState>();
        }
        else
        {
            _stateMachine.ChangeState<PFlyingState>();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Data.maxDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Data.attackRange);
    }

   
}
