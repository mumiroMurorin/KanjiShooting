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

        if (playableDirectors == null) { Debug.LogError("ySystemzPlayableDirectorClass‚ªƒZƒbƒg‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ"); return null; }

        foreach (PlayableDirectorClass p in playableDirectors)
        {
            if (name != p.SceneName) { continue; }
            if (director != null) { Debug.LogWarning($"ySystemzPlayableDirector:{name}‚Í•¡”‚ ‚è‚Ü‚·Bæ‚ğ“n‚µ‚Ü‚·"); continue; }
            director = p.Director;
        }

        if (director == null) { Debug.LogWarning($"ySystemzPlayableDirector:{name}‚Í‘¶İ‚µ‚Ü‚¹‚ñ"); }
        return director;
    }
}
