using UnityEngine;
using System.Collections;

public class DoCameraShake : MonoBehaviour {
    public CameraShake cameraShake;
    public void PrintEvent(string s) {
        cameraShake.shakeDuration = 1.5f;
        Debug.Log("PrintEvent: " + s + " called at: " + Time.time);
    }
}