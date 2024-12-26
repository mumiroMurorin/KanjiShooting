using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class CSVLoader : MonoBehaviour
{
    private string excelUrl = "https://drive.google.com/uc?id=1EqKwp-tzQ_hmmpyS2R3V7rK8jlfFeNvY&export=download";

    void Start()
    {
        StartCoroutine(DownloadExcelFile());
    }

    private IEnumerator DownloadExcelFile()
    {
        UnityWebRequest request = UnityWebRequest.Get(excelUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            byte[] data = request.downloadHandler.data;

            // �_�E�����[�h�����t�@�C�����ꎞ�ۑ�
            string filePath = Path.Combine(Application.persistentDataPath, "downloaded.csv");
            File.WriteAllBytes(filePath, data);

            Debug.Log("Excel file downloaded and saved at: " + filePath);

            // �K�v�ɉ�����Excel�����
            // NPOI�Ȃǂ��g��
        }
        else
        {
            Debug.LogError("Failed to download Excel file: " + request.error);
        }
    }
}
