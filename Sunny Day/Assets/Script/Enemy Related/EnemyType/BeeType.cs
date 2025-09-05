using System.Collections.Generic;
using UnityEngine;

public class BeeType : MonoBehaviour
{
    private StateMachine _stateMachine;
    [SerializeField] private EnemyStats stats;
    public EnemyData data;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 
    void Start()
    {
        _stateMachine = new StateMachine();

        data.rb = GetComponent<Rigidbody2D>();
        data.anim = GetComponent<Animator>();
        data.transform = GetComponent<Transform>();
        data.spawnPoint = transform.position;
        data.target = GameObject.FindGameObjectWithTag("Player");
        ObjectPool.Instance.InstantiateObject(stats.projectilePrefab, stats.numberOfBullets, data.projectiles);


        _stateMachine.AddState(new FlyingState(data, _stateMachine, stats));
        _stateMachine.AddState(new PFlyingState(data, _stateMachine, stats));
        _stateMachine.AddState(new FShootState(data, _stateMachine, stats));

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
        float distance = Vector2.Distance(data.target.transform.position, transform.position);
        if (_stateMachine.CurrentState is null)
        {
            _stateMachine.ChangeState<PFlyingState>();
        }

        if(distance <= stats.attackRange)
        {
            _stateMachine.ChangeState<FShootState>();
        }
        else if ( distance <= stats.maxDistance)
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
        Gizmos.DrawWireSphere(transform.position, stats.maxDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stats.attackRange);
    }

   
}
