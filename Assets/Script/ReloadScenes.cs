using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScenes : MonoBehaviour
{
    public void Reload()
    {
        SceneManager.UnloadSceneAsync("BlacksmithShop_3-0");
    }
}
