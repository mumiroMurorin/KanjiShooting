using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, IMovable
{
    /// <summary>
    /// ‚Ğ‚ç‚ª‚È‚ğŠ’è‚ÌˆÊ’u‚É“®‚©‚·
    /// </summary>
    /// <param name="pos"></param>
    public void Move(Vector3 pos)
    {
        this.transform.localPosition = pos;
    }

    /// <summary>
    /// íœ
    /// </summary>
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
