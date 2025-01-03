using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxChanger : MonoBehaviour
{
    [Header("�ύX�O��SkyBoxMaterial")]
    [SerializeField] Material previousSky;
    [Header("�ύX���SkyBoxMaterial")]
    [SerializeField] Material nextSky;

    [Header("���S�J�ڂ܂ł̎���")]
    [SerializeField] float changeDuration;
    [Header("�}�e���A�����ς��u�Ԃ̎���"), Range(0f, 1f)]
    [SerializeField] float changeMaterialTime;
    [Header("��]�X�s�[�h")]
    [SerializeField] AnimationCurve rotateSpeed;

    float rotationRepeatValue;
    float changeCount;
    float ChangingRatio => changeCount / changeDuration;

    bool isChanging;
    bool isChangedMaterial;

    void Update()
    {
        if (isChanging) 
        {
            RotateSkyBox();

            changeCount += Time.deltaTime;

            if (!isChangedMaterial && ChangingRatio >= changeMaterialTime) { RenderSettings.skybox = nextSky; }
            if(ChangingRatio >= 1f) { isChanging = false; }
        }
    }

    /// <summary>
    /// SkyBox(Material)����]������(�K��Update�ŌĂ�ŁI)
    /// </summary>
    private void RotateSkyBox()
    {
        if (!previousSky) { return; }
        if (!nextSky) { return; }

        rotationRepeatValue = Mathf.Repeat(previousSky.GetFloat("_Rotation") + rotateSpeed.Evaluate(ChangingRatio) * Time.deltaTime, 360f);

        previousSky.SetFloat("_Rotation", rotationRepeatValue);
        nextSky.SetFloat("_Rotation", rotationRepeatValue);
    }

    public void ChangeSkyBoxTrigger()
    {
        // ������
        changeCount = 0;
        rotationRepeatValue = 0;
        isChangedMaterial = false;

        isChanging = true;
    }
}
