using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, IMovable
{
    /// <summary>
    /// �Ђ炪�Ȃ�����̈ʒu�ɓ�����
    /// </summary>
    /// <param name="pos"></param>
    public void Move(Vector3 pos)
    {
        this.transform.localPosition = pos;
    }

    /// <summary>
    /// �폜
    /// </summary>
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
