using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class OptionSetter : MonoBehaviour
{
    private OptionHolder optionHolder;

    /// <summary>
    /// íçì¸
    /// </summary>
    /// <param name="holder"></param>
    [Inject]
    public void Construct(OptionHolder holder)
    {
        optionHolder = holder;
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