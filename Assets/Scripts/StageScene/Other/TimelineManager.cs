using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineManager : MonoBehaviour
{
    [System.Serializable]
    class PlayableDirectorClass
    {
        [SerializeField] string sceneName;
        [SerializeField] PlayableDirector director;

        public string SceneName { get { return sceneName; } }
        public PlayableDirector Director { get { return director; } }
    }

    [SerializeField] PlayableDirectorClass[] playableDirectors; 

    public PlayableDirector GetPlayableDirector(string name)
    {
        PlayableDirector director = null;

        if (playableDirectors == null) { Debug.LogError("�ySystem�zPlayableDirectorClass���Z�b�g����Ă��܂���"); return null; }

        foreach (PlayableDirectorClass p in playableDirectors)
        {
            if (name != p.SceneName) { continue; }
            if (director != null) { Debug.LogWarning($"�ySystem�zPlayableDirector:{name}�͕�������܂��B���n���܂�"); continue; }
            director = p.Director;
        }

        if (director == null) { Debug.LogWarning($"�ySystem�zPlayableDirector:{name}�͑��݂��܂���"); }
        return director;
    }
}
