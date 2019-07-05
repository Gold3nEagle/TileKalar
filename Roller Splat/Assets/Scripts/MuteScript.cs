using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MuteScript : MonoBehaviour
{


    public Texture sound, mute;
    public RawImage buttonTexture;

    bool toggle = false;

    // Start is called before the first frame update
    void Start()
    {
        buttonTexture = GetComponent<RawImage>();
    }
     
    public void MuteSound()
    {
        if (toggle == false)
        {
            AudioListener.volume = 0f;
            buttonTexture.texture = mute;
            toggle = true;
        }
        else
        {
            AudioListener.volume = 1f;
            buttonTexture.texture = sound;
            toggle = false;
        }
    }

    public void AccessPrivacyPolicy()
    {
        Application.OpenURL("https://haethamalhaddad.blogspot.com/2019/07/tilekalars-privacy-policy.html");
    }

}
