using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UpDownAnimation : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] float height;
    [SerializeField] bool isDownFirst;

    private Vector3 initialLocalPosition; // �q�I�u�W�F�N�g�̃��[�J���ʒu

    void Start()
    {
        // �q�I�u�W�F�N�g�̏������[�J���ʒu���擾
        initialLocalPosition = transform.localPosition;

        // DoTween���g�p���ď㉺�^�����i���I�ɌJ��Ԃ�
        transform.DOLocalMoveY(initialLocalPosition.y + height * (isDownFirst ? -1 : 1), duration)
            .SetRelative(false) // ���[�J���ʒu�Ɋ�Â��ē���
            .SetEase(Ease.InOutSine) // ���炩�ȓ���
            .SetLoops(-1, LoopType.Yoyo); // �i�v�ɌJ��Ԃ��i�㉺�j
    }
}
