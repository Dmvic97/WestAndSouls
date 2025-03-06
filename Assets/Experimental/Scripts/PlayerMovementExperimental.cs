using UnityEngine;

public class PlayerMovementExperimental : MonoBehaviour
{
    [Header("References")]
    public PlayerMovementStats PlayerMovementStats;
    [SerializeField] private Collider2D _feetColl;
    [SerializeField] private Collider2D _bodyColl;

    private Rigidbody _rb;

    //movement vars
    private Vector2 _moveVelocity;
    private bool _isFacingRight;

    //collision check var
    private RaycastHit2D _groundHit;
    private RaycastHit2D _headHit;
    private bool _isGrounded;
    private bool _bumpedHead;

    //at the start
    private void Awake()
    {
        _isFacingRight = true; //we start looking right

        _rb = GetComponent<Rigidbody>(); //we get player rb "propierties" (as the script is used as a component at the player)
    }

    private void FixedUpdate() //why fixed
    {
        
        CollisionChecks();

        if (_isGrounded)
        {
            Move(PlayerMovementStats.GroundAcceleration, PlayerMovementStats.GroundDeceleration, InputManager.Movement);
        }
        else
        {
            Move(PlayerMovementStats.AirAccelaration, PlayerMovementStats.AirDecelaration, InputManager.Movement);
        }
    }


    //Podemos agrupar partes de código con #region y #endregion
    #region Movement
    //movement method which uses few var (acce, etc) and math functions
    private void Move(float acceleration, float deceleration, Vector2 moveInput)
    {
         if (moveInput != Vector2.zero) //if chara its moving
         {
            TurnCheck(moveInput);

            Vector2 targetVelocity = Vector2.zero;//why is this needed?
            if (InputManager.RunIsHeld) //diff move values if running or not
            {
                targetVelocity = new Vector2(moveInput.x, 0f) * PlayerMovementStats.MaxRunSpeed;
            }
            else
            {
                targetVelocity = new Vector2(moveInput.x, 0f) * PlayerMovementStats.MaxWalkSpeed;
            }

            _moveVelocity = Vector2.Lerp(_moveVelocity, targetVelocity, acceleration * Time.fixedDeltaTime); //Lerp is a math function which i have 0 idea does, we mult by DeltaTime so it doesnt work on frames
            _rb.linearVelocity = new Vector2(_moveVelocity.x, _rb.linearVelocity.y); //so we calculate the current velocity of our chara and add to the rb
         }

         else if (moveInput == Vector2.zero)
         {
            _moveVelocity = Vector2.Lerp(_moveVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);//if we stop the input chara start to decelerate
            _rb.linearVelocity = new Vector2(_moveVelocity.x, _rb.linearVelocity.y);//add the calculated velocity to the rb
         }
    }

    //check if we need to turn sprite
    private void TurnCheck(Vector2 moveInput)
    {
        if (_isFacingRight && moveInput.x < 0)
        {
            Turn(false);
        }
        else if (!_isFacingRight && moveInput.x > 0)
        {
            Turn(true);
        }
    }
    //Turn the Sprite
    private void Turn (bool turnRight)
    {
        if (turnRight)
        {
            {
                _isFacingRight = true;
                transform.Rotate(0f, 100f, 0f);
            }
        }
        else
        {
            _isFacingRight = false;
            transform.Rotate(0f, -100f, 0f);
        }
    }
    #endregion

    #region Collision Checks

    private void IsGrounded()
    {
        Vector2 boxCastOrigin = new Vector2(_feetColl.bounds.center.x, _feetColl.bounds.center.y);
        Vector2 boxCastSize = new Vector2(_feetColl.bounds.size.x, PlayerMovementStats.GroundLayer);

        _groundHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.down, PlayerMovementStats.GroundDetectionRayLength, PlayerMovementStats.GroundLayer);

        if (_groundHit.collider == null)
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }
    private void CollisionChecks()
    {
        IsGrounded();
    }
    #endregion
}
