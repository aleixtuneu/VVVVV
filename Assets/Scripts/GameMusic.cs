using UnityEngine;

public class GameMusic : MonoBehaviour
{
    public AudioSource _as;

    private void OnEnable()
    {
        _as = GetComponent<AudioSource>();
        //MenuScript.PauseGameEvent += StopMusic;
    }

    private void StopMusic(bool stopped)
    {
        if (stopped)
        {
            _as.Stop();
        }
        else
        {
            _as.Play();
        }
    }
}
