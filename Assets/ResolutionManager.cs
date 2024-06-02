using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionManager : MonoBehaviour
{
    public TMP_Dropdown ResolutionDropdown;
    public Toggle FullscreenToggle;

    Resolution[] AllResolutions;
    int SelectedResolution;
    bool IsFullscreen;

    List<Resolution> SelectedResoltionList = new List<Resolution>();

    void Start()
    {
        IsFullscreen = true;
        AllResolutions = Screen.resolutions;    

        List<string> resolutionStringList = new List<string>();
        string newRes;
        foreach (Resolution res in AllResolutions)
        {
            newRes = res.width.ToString() + "x" + res.height.ToString();
            if (!resolutionStringList.Contains(newRes))
            {
                resolutionStringList.Add(newRes);
                SelectedResoltionList.Add(res);
                
            }
        }
        
        ResolutionDropdown.AddOptions(resolutionStringList);
    }

    public void ChangeResolution()
    {
        SelectedResolution = ResolutionDropdown.value;
        Screen.SetResolution(SelectedResoltionList[SelectedResolution].width, SelectedResoltionList[SelectedResolution].height, IsFullscreen);
    }

    public void ChangeFullscreen()
    {
        IsFullscreen = FullscreenToggle.isOn;
        Screen.SetResolution(SelectedResoltionList[SelectedResolution].width, SelectedResoltionList[SelectedResolution].height, IsFullscreen);
    }
}
