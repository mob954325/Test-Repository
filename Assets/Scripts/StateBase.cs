using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
{
    /// <summary>
    /// 현재 State가 시작 될 때 실행되는 내용
    /// </summary>
    /// <returns></returns>
    public abstract StateBase EnterCurrentState();

    /// <summary>
    /// 현재 State가 종료 될 때 실행되는 내용
    /// </summary>
    /// <returns></returns>
    public abstract StateBase ExitCurrentState();

    /// <summary>
    /// 현재 State에서 실행되는 내용 (Update)
    /// </summary>
    /// <returns></returns>
    public abstract StateBase RunCurrentState();
}