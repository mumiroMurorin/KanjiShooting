using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] Image image; // Image コンポーネント
    [SerializeField] Sprite[] frames; // 分割したスプライトを配列に入れる
    [SerializeField] float frameRate = 0.1f; // フレーム間の間隔（秒）

    private int currentFrame = 0;
    CancellationTokenSource cts;

    private void Start()
    {
        cts = new CancellationTokenSource();
        PlayAnimation(cts.Token).Forget();
    }

    private async UniTaskVoid PlayAnimation(CancellationToken token)
    {
        while (true)
        {
            if (token.IsCancellationRequested) { break; }

            image.sprite = frames[currentFrame]; // 現在のフレームを表示
            currentFrame = (currentFrame + 1) % frames.Length; // 次のフレームへ
            await UniTask.WaitForSeconds(frameRate, cancellationToken: token);
        }
    }

    private void OnDestroy()
    {
        cts.Cancel();
        cts.Dispose();
    }
}
