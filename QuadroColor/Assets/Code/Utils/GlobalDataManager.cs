using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataManager: SingletonTemplate<GlobalDataManager>
{
    #region //SerializeFields
    #endregion

    #region //Events
    [SerializeField] public GameEvents.EventGameScene OnGameSceneChanged;
    #endregion

    #region //Public fields
    #endregion

    #region //Private fields
    SceneList currentScene;
    #endregion

    #region //Public fields
    public LevelStartData activeLevelSettings;
    #endregion

    #region //Properies
    #endregion

    #region //Overrides
    #endregion

    #region //Public



    #endregion

    #region //Event callbacks
    #endregion

    #region //Private
    void UpdateState(SceneList a_newScene)
    {
        SceneList prevGameScene = currentScene;
        currentScene = a_newScene;

        switch (currentScene)
        {
            /*case GameState.PREGAME:
                Time.timeScale = 1;
                break;

            case GameState.RUNNING:
                Time.timeScale = 1;
                break;

            case GameState.PAUSED:
                Time.timeScale = 0;
                break;*/

            default:
                break;
        }

        OnGameSceneChanged.Invoke(prevGameScene, currentScene);
    }


    #endregion

    #region //Editor part
    private void OnValidate()
    {
    }

    private void OnDrawGizmos()
    {
    }

    private void OnDrawGizmosSelected()
    {
    }
    #endregion
}
