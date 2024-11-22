using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ScoreChanger : MonoBehaviour
{
    private ScoreHolderRoot scoreHolder;

    // ScoreLifetimeScopeにより注入される想定。
    [Inject]
    public void Construct(ScoreHolderRoot holder)
    {
        scoreHolder = holder;
    }

    private void Start()
    {
        ScorePlusOne();
    }

    public void ScorePlusOne()
    {
        scoreHolder.Score++;
    }
}
