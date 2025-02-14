using UnityEngine;
using UnityEngine.InputSystem; //Don't miss this!

public class PlayerController : MonoBehaviour
{
    public GameObject ballPrefab;
    public float ballSpeed = 10f;
    public float speed = 5f;
    private PlayerInput _input; //field to reference Player Input component
    private Rigidbody2D _rigidbody;
    private Vector2 _facingVector = Vector2.right; //projectile mnovement

    // Start is called before the first frame update
    void Start()
    {
        //set reference to PlayerInput component on this object
        //Top Action Map, "Player" should be active by default
        _input = GetComponent<PlayerInput>();
        //You can switch Action Maps using _input.SwitchCurrentActionMap("UI");

        //set reference to Rigidbody2D component on this object
        _rigidbody = GetComponent<Rigidbody2D>();

        //transform.position = new Vector2(3, -1);
        //Invoke(nameof(AcceptDefeat), 10);
    }

    void AcceptDefeat()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //if Fire action was performed log it to the console
        if (_input.actions["Fire"].WasPressedThisFrame())
        {
            //create a new object that is a clone of the ballPrefab
            //at this object's position and default rotation
            //and use a new variable (ball) to reference the clone
            var ball = Instantiate(ballPrefab, 
                                transform.position,
                                Quaternion.identity);
            //Get the Rigidbody 2D component from the new ball 
            //and set its velocity to x:-10f, y:0, z:0
            ball.GetComponent<Rigidbody2D>().velocity = Vector2.left * ballSpeed;
            //Debug.Log("Fire activated!");

            ball.GetComponent<Rigidbody2D>().velocity = //has top do with movement projectiles
            _facingVector.normalized * 10f ; //has to do with movement projectiles

             //*CHANGE* instead of changing rigidbody velocity: 
            //call SetDirection from BallController on new ball
            ball.GetComponent<BallController>()?.SetDirection(_facingVector);
            
        }
    }

    private void FixedUpdate()
    {
        //set direction to the Move action's Vector2 value
        var dir = _input.actions["Move"].ReadValue<Vector2>();

        //change the velocity to match the Move (every physics update)
        _rigidbody.velocity = dir * speed;

         if (dir.magnitude > 0.5) // has to do with projectile movement
    {
        _facingVector = _rigidbody.velocity; // projectile movement
    }
    }
}
