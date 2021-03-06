using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _charaterController;
    private UI_Manager _UI_Manager;

    private float _speed = 7f;
    private float _gravity = 0.5f;
    private float _jumpSpeed = 17f;
    private float _verticalVelocity, _horizontalVelocity;
    private bool _isInJump = false;
    private Vector3 _respawnPosition;

    private int _starCount;
    private int _lives = 3;

    private void Start()
    {
        FindObjects();
        CheckParameters();

        _respawnPosition = transform.position;
        _UI_Manager.UpdateLives(_lives);
        _UI_Manager.UpdateScore(_starCount);
    }

    private void FindObjects()
    {
        GameObject UI_ManagerGameObject = GameObject.Find("Canvas");
        if (UI_ManagerGameObject == null)
        {
            Debug.LogError("Player could not find Canvas.");
        }
        else
        {
            _UI_Manager = UI_ManagerGameObject.GetComponent<UI_Manager>();
            if (_UI_Manager == null)
            {
                Debug.LogError("Player could not find UI Manager.");
            }
        }

        _charaterController = GetComponent<CharacterController>();
        if (_charaterController == null)
        {
            Debug.LogError("Player could not locate its Character Controller.");
        }
    }

    private void CheckParameters()
    {
        if (_speed <= 0)
        {
            Debug.LogAssertion("Player's Speed is zero or negative.");
        }
        if (_gravity <= 0)
        {
            Debug.LogAssertion("Player's Gravity is zero or negative.");
        }
        if (_jumpSpeed <= 0)
        {
            Debug.LogAssertion("Player's Jump Speed is zero or negative.");
        }
        if (_gravity > _jumpSpeed / 5)
        {
            Debug.LogAssertion("Player's Gravity is more than 1/5 of Jump Speed, so the jump may be to short.");
        }
        if (_lives <= 0)
        {
            Debug.LogAssertion("Player's Lives count is invalid.");
        }
        if (_starCount != 0)
        {
            Debug.LogAssertion("Player starts with not zero Stars.");
        }
    }

    private void Update()
    {
        Movement();  

        if (transform.position.y < -7f)
        {
            _lives--;
            if (_lives <= 0)
            {

            }
            else
            {
                transform.position = _respawnPosition;
                _verticalVelocity = 0f;
                _horizontalVelocity = 0f;
                _UI_Manager.UpdateLives(_lives);
            }
        }
    }    

    private void Movement()
    {
        if (_charaterController.isGrounded)
        {
            if (_isInJump)
            {
                _isInJump = false;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _verticalVelocity = _jumpSpeed;
                _isInJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _isInJump)
            {
                _verticalVelocity = _jumpSpeed;
                _isInJump = false;
            }
            _verticalVelocity -= _gravity;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            _horizontalVelocity = horizontalInput * _speed;
        }
        _charaterController.Move(new Vector3(_horizontalVelocity * Time.deltaTime, _verticalVelocity * Time.deltaTime));
    }

    public void AddStar()
    {
        _starCount++;
        _UI_Manager.UpdateScore(_starCount);
    }

    public void AddVelocity(Vector3 velocity)
    {
        _horizontalVelocity = velocity.x;
        _verticalVelocity += velocity.y;
    }

    public void ClearVelocity()
    {
        _horizontalVelocity = 0f;
        _verticalVelocity = 0f;
    }
}
