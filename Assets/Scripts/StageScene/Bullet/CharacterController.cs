using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, IMovable
{
    [Header("通常弾")]
    [SerializeField] GameObject generalObject;
    [Header("スペシャル弾")]
    [SerializeField] GameObject specialObject;
    [Header("チャージ中のエフェクト")]
    [SerializeField] ParticleSystem particleOnCharge;

    private void Start()
    {
        generalObject.SetActive(true);
        specialObject.SetActive(false);
    }

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

    public void ChargeStart()
    {
        if(particleOnCharge == null) { Debug.LogWarning("【Bullet】チャージ中のエフェクトがないよ！"); return; }
        particleOnCharge.gameObject.SetActive(true);
        particleOnCharge.Play();
    }

    public void ChargeCancell()
    {
        if (particleOnCharge == null) { return; }
        particleOnCharge.Stop();
        particleOnCharge.gameObject.SetActive(false);
    }

    public void ChargeComplete()
    {
        generalObject.SetActive(false);
        specialObject.SetActive(true);
    }
}
