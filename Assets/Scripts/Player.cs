using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum States
{
    Idle = 0,
    Attack
}

public class Player : MonoBehaviour
{
    [HideInInspector] public States currentState;
    [SerializeField] GameObject[] playerStates;

    PlayerInputAction InputAction;
    Rigidbody rigid;
    Animator anim;

    // value
    public Vector3 inputValue;
    public float speed;

    void Awake()
    {
        Transform child = transform.GetChild(0);
        playerStates = new GameObject[child.childCount];

        for(int i = 0; i < playerStates.Length; i++)
        {
            Transform stateChild = transform.GetChild(i);
            playerStates[i] = stateChild.gameObject;
        }

        InputAction = new PlayerInputAction();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        InputAction.Enable();
        InputAction.Player.Move.performed += OnMoveInput;
        InputAction.Player.Move.canceled += OnMoveInput;
        InputAction.Player.Jump.performed += OnJumpInput;
        InputAction.Player.Jump.canceled += OnJumpInput;
        InputAction.Player.Attack.performed += OnAttackInput;
        InputAction.Player.Attack.canceled += OnAttackInput;
    }
    void OnDisable()
    {
        InputAction.Player.Attack.canceled -= OnAttackInput;
        InputAction.Player.Attack.performed -= OnAttackInput;
        InputAction.Player.Jump.canceled -= OnJumpInput;
        InputAction.Player.Jump.performed -= OnJumpInput;
        InputAction.Player.Move.canceled -= OnMoveInput;
        InputAction.Player.Move.performed -= OnMoveInput;
        InputAction.Disable();
    }

    private void OnAttackInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            anim.SetTrigger("Attack");
        }

        if(context.duration > 0.5f)
        {
            anim.SetBool("IsAiming", true);
        }
        
        if(context.canceled)
        {
            anim.SetBool("IsAiming", false);
        }

        //if(context.canceled)
        //{
        //    anim.SetBool("IsAttack", false);
        //}
    }

    private void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            anim.SetTrigger("Jump");
        }
    }

    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        inputValue = new Vector3(input.x, 0, input.y);
    }

    void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + Time.fixedDeltaTime * inputValue * speed);
    }

    /// <summary>
    /// 상태를 받는 함수
    /// </summary>
    /// <param name="state">받을 상태 입력</param>
    public StateBase SetPlayerState(States state)
    {
        StateBase selectState = null;
        switch (state)
        {
            case States.Idle:
                selectState = playerStates[(int)States.Idle].GetComponent<Player_Idle>();
                break;
            case States.Attack:
                selectState = playerStates[(int)States.Attack].GetComponent<Player_Attack>();
                break;
        }
        return selectState;
    }
}
