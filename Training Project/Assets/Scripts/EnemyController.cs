
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _direction = Vector2.right;
    [SerializeField] private float patrolDelay = 1.5f;
    private WaypointPath _waypointPath;
    private Vector2 _patrolTargetPosition;
    [SerializeField] private float patrolSpeed= 1f;


  
    void Awake()
    {

        _rigidbody = GetComponent<Rigidbody2D>();
        _waypointPath = GetComponentInChildren<WaypointPath>();

        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody2D component is missing from this GameObject.");
        }
    }

    private void FixedUpdate()
    {
        if (!_waypointPath) return;

        //set our direction toward that waypoint:
        //subtracting our position from target position
        //gives us the slope line between the two
        //We can get direction by normalizing it
        //We can get distance by magnitude
        var dir = _patrolTargetPosition - (Vector2)transform.position;

        //if we are close enough to the target,
        //time to get the next waypoint
        if (dir.magnitude <= 0.1)
        {
            //get next waypoint
            _patrolTargetPosition = _waypointPath.GetNextWaypointPosition();

            //change direction
            dir = _patrolTargetPosition - (Vector2)transform.position;
        }

        //this if/else is not in the video (it was made in the GameManager videos)
        //Be sure to update the line in the if clause to match the change in the
        //video instead of adding it above
        if (GameManager.Instance.State == GameState.Playing)
        {
            //UPDATE: how velocity is set
            //normalized reduces dir magnitude to 1, so we can
            //keep at the speed we want by multiplying
            _rigidbody.velocity = dir.normalized * patrolSpeed; 
        }
        else 
        {
            _rigidbody.velocity = Vector2.zero;
        }

        //keep resetting the velocity to the
        //direction * speed even if nudged
        //_rigidbody.velocity = _direction * 2;
        // if (GameManager.Instance.State == GameState.Playing)
        // {
        //     _rigidbody.velocity = _direction * 2;
        // }
        // else
        // {
        //     _rigidbody.velocity = Vector2.zero;
        // }
    }

    private void Start()
    {
        
        if (_waypointPath)
        {
            _patrolTargetPosition = _waypointPath.GetNextWaypointPosition();
        }
        else
        {
            StartCoroutine(PatrolCoroutine());
        }
    }
    IEnumerator PatrolCoroutine()
    {
        //change the direction every second
        while (true) {
            _direction = new Vector2(1, -1);
            yield return new WaitForSeconds(patrolDelay);
            _direction = new Vector2(-1, 1);
            yield return new WaitForSeconds(patrolDelay);
        }
    }

    private void OnEnable()
    { 
        GameManager.OnAfterStateChanged += HandleGameStateChange;
    }

    private void OnDisable()
    {
        GameManager.OnAfterStateChanged -= HandleGameStateChange;
    }

    private void HandleGameStateChange(GameState state)
    {
        
        if (state == GameState.Starting)
        {
            GetComponent<SpriteRenderer>().color = Color.grey;
        }

        if (state == GameState.Playing)
        {
            GetComponent<SpriteRenderer>().color = Color.magenta;
        }
    }

    public void AcceptDefeat()
    {
        GameEventDispatcher.TriggerEnemyDefeated();
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.transform.GetComponent<HealthSystem>()?.Damage(3);
            Vector2 awayDirection = (Vector2)(other.transform.position - transform.position);
            other.transform.GetComponent<PlayerController>()?.Recoil(awayDirection * 3f);
        }
    }
}
