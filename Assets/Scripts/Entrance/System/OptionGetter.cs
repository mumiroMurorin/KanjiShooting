using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class OptionGetter : MonoBehaviour
{
    private ScoreHolderRoot scoreHolder;

    // ScoreHolderEnqueueParentReceiveLifetimeScope‚É‚æ‚è’“ü‚³‚ê‚é‘z’èB
    [Inject]
    public void Construct(ScoreHolderRoot holder)
    {
        scoreHolder = holder;
    }

    private void Start()
    {
        Debug.Log($"{GetScore()}");
    }

    public int GetScore()
    {
        return scoreHolder.Score;
    }
}