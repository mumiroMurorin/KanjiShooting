using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxChanger : MonoBehaviour
{
    [Header("変更前のSkyBoxMaterial")]
    [SerializeField] Material previousSky;
    [Header("変更後のSkyBoxMaterial")]
    [SerializeField] Material nextSky;

    [Header("完全遷移までの時間")]
    [SerializeField] float changeDuration;
    [Header("マテリアルが変わる瞬間の時間"), Range(0f, 1f)]
    [SerializeField] float changeMaterialTime;
    [Header("回転スピード")]
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
    /// SkyBox(Material)を回転させる(必ずUpdateで呼んで！)
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
        // 初期化
        changeCount = 0;
        rotationRepeatValue = 0;
        isChangedMaterial = false;

        isChanging = true;
    }
}
