using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _charaterController;
    private UI_Manager _UI_Manager;

    private float _speed = 5.5f;
    private float _gravityConstant = 15f;
    private float _jumpSpeed = 6f;
    private float _verticalVelocity;
    private bool _isInJump = false;

    private int _starCount;

    void Start()
    {
        _UI_Manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _charaterController = GetComponent<CharacterController>();

        if(_UI_Manager == null)
        {
            Debug.LogError("Player could not find UI Manager.");
        }
        if (_charaterController == null)
        {
            Debug.LogError("Player could not locate its Character Controller.");
        }
    }

    void Update()
    {
        if (_charaterController.isGrounded)
        {
            if(_isInJump)
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
            _verticalVelocity -= _gravityConstant/2 * Time.deltaTime;            
        }

        _charaterController.Move(new Vector3(Input.GetAxis("Horizontal") * _speed * Time.deltaTime, _verticalVelocity * Time.deltaTime));
    }

    public void AddStar()
    {
        _starCount++;
        _UI_Manager.UpdateScore(_starCount);
    }
}
