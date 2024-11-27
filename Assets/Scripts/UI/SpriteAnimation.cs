using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] Image image; // Image �R���|�[�l���g
    [SerializeField] Sprite[] frames; // ���������X�v���C�g��z��ɓ����
    [SerializeField] float frameRate = 0.1f; // �t���[���Ԃ̊Ԋu�i�b�j

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

            image.sprite = frames[currentFrame]; // ���݂̃t���[����\��
            currentFrame = (currentFrame + 1) % frames.Length; // ���̃t���[����
            await UniTask.WaitForSeconds(frameRate, cancellationToken: token);
        }
    }

    private void OnDestroy()
    {
        cts.Cancel();
        cts.Dispose();
    }
}
