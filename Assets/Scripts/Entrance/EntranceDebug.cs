using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class EntranceDebug : MonoBehaviour
{
    CancellationTokenSource cts;
    [Header("ì«Ç›çûÇ›íÜÅc")]
    [SerializeField] GameObject obj;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && cts == null)
        {
            cts = new CancellationTokenSource();
            LoadMainScene(cts.Token).Forget();
            obj.SetActive(true);
        }
    }

    private async UniTaskVoid LoadMainScene(CancellationToken token)
    {
        if (token.IsCancellationRequested) { return; }
        await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainScene");
    }
}
