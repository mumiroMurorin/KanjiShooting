using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour, IMovable
{
    [Header("�ʏ�e")]
    [SerializeField] GameObject generalObject;
    [Header("�X�y�V�����e")]
    [SerializeField] GameObject specialObject;
    [Header("�`���[�W���̃G�t�F�N�g")]
    [SerializeField] ParticleSystem particleOnCharge;

    private void Start()
    {
        generalObject.SetActive(true);
        specialObject.SetActive(false);
    }

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

    public void ChargeStart()
    {
        if(particleOnCharge == null) { Debug.LogWarning("�yBullet�z�`���[�W���̃G�t�F�N�g���Ȃ���I"); return; }
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
