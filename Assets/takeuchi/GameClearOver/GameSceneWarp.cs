using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneWarp : MonoBehaviour
{
    #region �{��
    public void SceneWarp()
    {
        SceneManager.LoadScene("Title");
    }
    #endregion
}
