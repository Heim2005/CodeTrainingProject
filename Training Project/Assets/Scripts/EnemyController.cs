using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _direction = Vector2.right;
    [SerializeField] private float patrolDelay = 1.5f;
  
    void Awake()
{
    _rigidbody = GetComponent<Rigidbody2D>();
    if (_rigidbody == null)
    {
        Debug.LogError("Rigidbody2D component is missing from this GameObject.");
    }
}

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PatrolCoroutine());
    }

    private void FixedUpdate()
    {
        //keep resetting the velocity to the
        //direction * speed even if nudged
        //_rigidbody.velocity = _direction * 2;
        if (GameManager.Instance.State == GameState.Playing)
        {
            _rigidbody.velocity = _direction * 2;
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
