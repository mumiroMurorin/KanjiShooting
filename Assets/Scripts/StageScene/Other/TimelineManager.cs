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

        if (playableDirectors == null) { Debug.LogError("【System】PlayableDirectorClassがセットされていません"); return null; }

        foreach (PlayableDirectorClass p in playableDirectors)
        {
            if (name != p.SceneName) { continue; }
            if (director != null) { Debug.LogWarning($"【System】PlayableDirector:{name}は複数あります。先を渡します"); continue; }
            director = p.Director;
        }

        if (director == null) { Debug.LogWarning($"【System】PlayableDirector:{name}は存在しません"); }
        return director;
    }
}
