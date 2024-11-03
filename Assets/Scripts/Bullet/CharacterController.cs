using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, IMovable
{
    /// <summary>
    /// ひらがなを所定の位置に動かす
    /// </summary>
    /// <param name="pos"></param>
    public void Move(Vector3 pos)
    {
        this.transform.localPosition = pos;
    }

    /// <summary>
    /// 削除
    /// </summary>
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
