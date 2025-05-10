using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    public void SetMusic(float music)
    {
        //audioMixer.SetFloat("Volume", Mathf.Log10(volume)*20);
        audioMixer.SetFloat("Music",music);
    }

    public void SetSfx(float sfx)
    {
        //Debug.Log("SFX");
        audioMixer.SetFloat("SFX",sfx);
    }
}
