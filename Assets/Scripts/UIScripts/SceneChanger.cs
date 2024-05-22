using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public void SceneSwitch(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
