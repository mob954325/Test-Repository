using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInputAction InputAction;
    Rigidbody rigid;
    Animator anim;

    // value
    public Vector3 inputValue;
    public float speed;

    void Awake()
    {
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
        InputAction.Player.Aiming.performed += OnAimingInput;
        InputAction.Player.Aiming.canceled += OnAimingInput;        
    }

    void OnDisable()
    {
        InputAction.Player.Aiming.canceled -= OnAimingInput;
        InputAction.Player.Aiming.performed -= OnAimingInput;
        InputAction.Player.Attack.canceled -= OnAttackInput;
        InputAction.Player.Attack.performed -= OnAttackInput;
        InputAction.Player.Jump.canceled -= OnJumpInput;
        InputAction.Player.Jump.performed -= OnJumpInput;
        InputAction.Player.Move.canceled -= OnMoveInput;
        InputAction.Player.Move.performed -= OnMoveInput;
        InputAction.Disable();
    }
    private void OnAimingInput(InputAction.CallbackContext context)
    {
        if (context.performed || context.duration > 0.5f)
        {
            anim.SetTrigger("Aiming"); // 활 조준 사격 자세 시작
        }
        if (context.canceled)
        {
            StopCoroutine(RangeShot());
            StartCoroutine(RangeShot());
        }
    }

    IEnumerator RangeShot()
    {
        anim.SetBool("isShot", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isShot", false);
    }

    private void OnAttackInput(InputAction.CallbackContext context)
    {
        Debug.Log("근접 공격");
        anim.SetTrigger("Attack");
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
}
