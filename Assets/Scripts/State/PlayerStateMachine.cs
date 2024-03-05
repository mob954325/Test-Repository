using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    /// <summary>
    /// State�� ����Ǿ����� Ȯ���ϴ� ���� (false : ���� x , true : �����)
    /// </summary> 
    bool isEnter = false;

    /// <summary>
    /// ���¸ӽſ��� �׾����� Ȯ���ϴ� ����
    /// </summary>
    bool isDead = false;

    /// <summary>
    /// ���� ���� ��ũ��Ʈ ( Null�̸� �ൿ�� ���� )
    /// </summary>
    public StateBase currentState;

    void Start()
    {
        // init
        if (currentState != null) // currentState�� ������ ù ���๮ ����
        {
            isEnter = true;
        }
    }

    void FixedUpdate()
    {
        RunStateMachine();
        OnDieState(); // ������ ��� ���·� �̵�
    }


    /// <summary>
    /// �ش� State���� ������ ���� (���¸� �����ҷ��� �� �Լ����� �����ؾ���)
    /// </summary>
    private void RunStateMachine()
    {
        if (isEnter)
        {
            isEnter = false;
            currentState?.EnterCurrentState();
        }

        StateBase nextState = currentState?.RunCurrentState();

        if (nextState != null)
        {
            ChangeStateMachine(nextState);
        }
    }

    /// <summary>
    /// CurrentState�� nextState�� �ٲٴ� �Լ�
    /// </summary>
    /// <param name="nextState">�ٲ� state�̸�</param>
    private void ChangeStateMachine(StateBase nextState)
    {
        if (currentState != nextState) // �Ű������� ���� ���°� ���� �ʴ� -> state ��ȯ ��
        {
            isEnter = true;
            currentState?.ExitCurrentState();
        }

        currentState = nextState; // ���� state�� ����
    }

    private void OnDieState()
    {
        //if (currentState.enemy.IsDie && !isDead)
        //{
        //    isEnter = true; // ���Կ��� Ȱ��ȭ
        //    isDead = true;
        //
        //    // ���� ����
        //    currentState = currentState.enemy.SetEnemyState(EnemyBase.State.Death);
        //    currentState.enemy = GetComponent<EnemyBase>(); // state�� enemy ������Ʈ �ޱ�
        //}
    }
}
