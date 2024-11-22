using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ScoreChanger : MonoBehaviour
{
    private ScoreHolderRoot scoreHolder;

    // ScoreLifetimeScope‚É‚æ‚è’“ü‚³‚ê‚é‘z’èB
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
