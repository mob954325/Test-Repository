using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
{
    /// <summary>
    /// ���� State�� ���� �� �� ����Ǵ� ����
    /// </summary>
    /// <returns></returns>
    public abstract StateBase EnterCurrentState();

    /// <summary>
    /// ���� State�� ���� �� �� ����Ǵ� ����
    /// </summary>
    /// <returns></returns>
    public abstract StateBase ExitCurrentState();

    /// <summary>
    /// ���� State���� ����Ǵ� ���� (Update)
    /// </summary>
    /// <returns></returns>
    public abstract StateBase RunCurrentState();
}