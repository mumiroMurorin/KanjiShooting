using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerTextView : MonoBehaviour
{
    [Header("TimerText")]
    [SerializeField] TextMeshProUGUI tmp;

    /// <summary>
    /// �L�������ς�����Ƃ��̃��\�b�h
    /// </summary>
    /// <param name="time"></param>
    public void OnChangeTimeCount(float time)
    {
        tmp.text = FormatTime(time);
    }

    /// <summary>
    /// float���uhh:mm:ss�v�ɃR���o�[�g
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    string FormatTime(float seconds)
    {
        int totalSeconds = Mathf.FloorToInt(seconds); // �����_�ȉ���؂�̂�
        int hours = totalSeconds / 3600;              // ����
        int minutes = (totalSeconds % 3600) / 60;     // ��
        int secs = totalSeconds % 60;                 // �b

        // "00:00:00" �t�H�[�}�b�g�ŕԂ�
        return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, secs);
    }
}
