using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnable_Audio : MonoBehaviour
{
    public AudioClip audioClip;

    private void OnEnable()
    {
        Audio_Manager.Instance.PlayMent(audioClip);
    }
}
