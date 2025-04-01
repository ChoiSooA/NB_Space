using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class TouchSelf : MonoBehaviour
{
    [SerializeField] private UnityEvent onClick;

    void Update() { }
    public void OnClick()
    {
        onClick.Invoke();
        Debug.Log($"{name} onclick �����");
    }

}