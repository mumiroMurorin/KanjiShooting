using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] PlayableDirector stageStartDirector;
    [SerializeField] PlayableDirector waveStartDirector;
    [SerializeField] PlayableDirector waveFinishDirector;
    [SerializeField] PlayableDirector stageFinishDirector;
    [SerializeField] PlayableDirector stageFailedStartDirector;
    [SerializeField] PlayableDirector stageFailedFinishDirector;

    public PlayableDirector GetStageStartPlayableDirector()
    {
        return stageStartDirector;
    }

    public PlayableDirector GetWaveStartPlayableDirector()
    {
        return waveStartDirector;
    }

    public PlayableDirector GetWaveFinishPlayableDirector()
    {
        return waveFinishDirector;
    }

    public PlayableDirector GetStageFinishPlayableDirector()
    {
        return stageFinishDirector;
    }

    public PlayableDirector GetStageFailedStartPlayableDirector()
    {
        return stageFailedStartDirector;
    }

    public PlayableDirector GetStageFailedFinishPlayableDirector()
    {
        return stageFailedFinishDirector;
    }
}
