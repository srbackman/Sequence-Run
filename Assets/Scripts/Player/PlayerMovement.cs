using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationType
{
    idle,
    run,
    jump
}

public class PlayerMovement : MonoBehaviour
{
    private PlayerAnimationCollection animationCollection;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [Space]
    [SerializeField] private float _groundingRaycatsLenght = 1f;
    [SerializeField] private LayerMask _validSurfaces;
    [Space]
    [SerializeField] private float _personalGravity = 3f;
    [SerializeField] private float _movementMaxSpeed = 3f;
    [SerializeField] private float _movementTimeToMaxSpeed = 0.3f;
    private float _maxSpeedTimer;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _slowdownForce = 0.3f;
    [SerializeField] private int _airDiff = 2;
    [SerializeField] private float _jumpCooldown = 0.2f;
    [Space]
    [SerializeField] private float _runningSpriteDuration = 0.3f;
    [Space]
    [SerializeField] private float _collisionSafeGuardSideDistance;
    [SerializeField] private float _collisionSafeGuardUpDistance;
    [SerializeField] private float _collisionSafeGuardDownDistance;
    [SerializeField] private float _collisionSafeGuardRadius;

    private float _jumpTimer;
    private float _runningSpriteTimer;
    public Vector2 _playerVelocity = Vector2.zero;
    private Vector2 _bounceForce = Vector2.zero;
    private bool _leftLook = true;
    private CapsuleCollider2D _collider2D;

    private void Awake()
    {
        _collider2D = GetComponent<CapsuleCollider2D>();
        animationCollection = GetComponent<PlayerAnimationCollection>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void MovePlayer(float horizontal, bool jump)
    {   
        //transform.position += transform.TransformDirection(new Vector3(0f, -0.2f)) * Time.deltaTime;
        bool groundedState = GroundCheck();

        PlayerAnimationSelector(horizontal, groundedState);
        CalculateHorizontalSlowdown(horizontal, groundedState);
        CalculateGravity(groundedState);
        CalculateHorizontalMovement(horizontal, groundedState);
        CalculateJump(groundedState, jump);

        /*Move player*/
        if (_bounceForce != Vector2.zero)
        {
            _playerVelocity = _bounceForce;
            _bounceForce = Vector2.zero;
        }
        
        transform.localPosition += transform.TransformDirection(_playerVelocity) * Time.deltaTime;

        /*Collision check*/
        KeepColliderClear();
    }

    /*Animations*/
    private void PlayerAnimationSelector(float horizontal, bool groundedState)
    {
        _runningSpriteTimer -= Time.deltaTime;
        AnimationType animationType = AnimationType.idle;
        if (horizontal != 0 && groundedState)
            animationType = AnimationType.run;
        if (!groundedState)
            animationType = AnimationType.jump;
        if (horizontal < 0 && !_leftLook || horizontal > 0 && _leftLook)
        {
            _leftLook = !_leftLook;
            _spriteRenderer.flipX = !_leftLook;
        }
        switch (animationType)
        {
            case AnimationType.idle:
                _spriteRenderer.sprite = animationCollection._idleSprite;
                break;
            case AnimationType.run:
                if (_runningSpriteTimer <= 0)
                {
                    _runningSpriteTimer = _runningSpriteDuration;
                    _spriteRenderer.sprite = animationCollection._runingSprites[animationCollection._currentRunSprite];
                    animationCollection._currentRunSprite++;
                    if (animationCollection._runingSprites.Length <= animationCollection._currentRunSprite)
                        animationCollection._currentRunSprite = 0;
                }
                break;
            case AnimationType.jump:
                _spriteRenderer.sprite = animationCollection._jumpSprite;
                break;
        }
        if (animationType == AnimationType.jump)
        { _collider2D.size = new Vector2(0.1f, 0.375f); }
        else
        { _collider2D.size = new Vector2(0.1f, 0.475f); }
    }

    /*Slowdown horizontal*/
    private void CalculateHorizontalSlowdown(float horizontal, bool groundedState)
    {
        if (horizontal == 0 && Mathf.Abs(_playerVelocity.x) > 0.02f)
        {
            Vector2 tempVec2;
            if (_playerVelocity.x < 0)
            {
                if (groundedState)
                    tempVec2 = new Vector2(_slowdownForce, 0); //+
                else
                    tempVec2 = new Vector2(_slowdownForce / _airDiff, 0); //+
            }
            else
            {
                if (groundedState)
                    tempVec2 = new Vector2(-_slowdownForce, 0); //-
                else
                    tempVec2 = new Vector2(-_slowdownForce / _airDiff, 0); //-
            }
            _playerVelocity += (tempVec2);
        }
        else if (horizontal == 0 && _playerVelocity.x != 0)
            _playerVelocity = new Vector2(0f, _playerVelocity.y);
    }

    /*Gravity*/
    private void CalculateGravity(bool groundedState)
    {
        if (!groundedState)
        {
            float dropSpeed = _personalGravity;
            _playerVelocity += new Vector2(0f, -dropSpeed);
        }
        else if (groundedState && _playerVelocity.y < 0)
        {
            _playerVelocity = new Vector2(_playerVelocity.x, 0f);
        }
    }

    /*Horizontal movement*/
    private void CalculateHorizontalMovement(float horizontal, bool groundedState)
    {
        _maxSpeedTimer += Time.deltaTime;
        if ((horizontal != 0 && Mathf.Abs(_playerVelocity.x) <= _movementMaxSpeed)
            || ((horizontal < 0 && _playerVelocity.x > 0) || (horizontal > 0 && _playerVelocity.x < 0)))
        {
            float tempX = Mathf.Clamp((_maxSpeedTimer / _movementTimeToMaxSpeed), 0f, _movementMaxSpeed);
            if (groundedState)
                _playerVelocity += new Vector2(horizontal * tempX, 0);
            else
                _playerVelocity += new Vector2(horizontal * (tempX / _airDiff), 0);
        }
        else
            _maxSpeedTimer = 0;
    }

    /*Calculate jump*/
    private void CalculateJump(bool groundedState, bool jump)
    {
        _jumpTimer -= Time.deltaTime;
        if (_jumpTimer <= 0 && groundedState && jump)
        {
            _jumpTimer = _jumpCooldown;
            _playerVelocity += new Vector2(0f, _jumpForce);
        }
    }

    public void AddBounceForce(Vector2 input)
    {
        _bounceForce = input;
    }

    private bool GroundCheck()
    {
        bool check = Physics2D.Raycast(transform.position, -transform.up, _groundingRaycatsLenght, _validSurfaces);
        Debug.DrawLine(transform.position, transform.position - (transform.up * _groundingRaycatsLenght), Color.white);
        return (check);
    }

    private void KeepColliderClear()
    {
        /*Raycast sides.*/
        PushAway(transform.TransformDirection(new Vector2(1, 0)), _collisionSafeGuardSideDistance);
        PushAway(transform.TransformDirection(new Vector2(-1, 0)), _collisionSafeGuardSideDistance);
        /*Raycast up and down.*/
        PushAway(transform.TransformDirection(new Vector2(0, 1)), _collisionSafeGuardUpDistance);
        PushAway(transform.TransformDirection(new Vector2(0, -1)), _collisionSafeGuardDownDistance);
    }

    private void PushAway(Vector3 direction, float maxDistance)
    {
        int i = 0;
        while (Physics2D.OverlapCircle(transform.position + (direction * maxDistance), _collisionSafeGuardRadius, _validSurfaces))
        {
            transform.position += ((direction * -1) * 0.01f);
            i++;
        }
        if (i > 2)
        {
            if (direction.x != 0) //X
            {
                _playerVelocity = new Vector3(0f, _playerVelocity.y);
            }
            if (direction.y != 0 && _playerVelocity.y <= 0) //Y
            {
                _playerVelocity = new Vector3(_playerVelocity.x, 0f);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + (transform.TransformDirection(new Vector2(1, 0)) * _collisionSafeGuardSideDistance));
        Gizmos.DrawLine(transform.position, transform.position + (transform.TransformDirection(new Vector2(-1, 0)) * _collisionSafeGuardSideDistance));
        Gizmos.DrawLine(transform.position, transform.position + (transform.TransformDirection(new Vector2(0, 1)) * _collisionSafeGuardUpDistance));
        Gizmos.DrawLine(transform.position, transform.position + (transform.TransformDirection(new Vector2(0, -1)) * _collisionSafeGuardDownDistance));
    }
}
