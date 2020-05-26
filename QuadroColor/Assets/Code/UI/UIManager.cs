using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonTemplate<UIManager>
{
    #region //SerializeFields
    #endregion

    #region //Events
    #endregion

    #region //Private fields
    #endregion

    #region //Properies
    #endregion

    #region //Overrides
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region //Public

    public void ResetGame()
    {
        if (GameEvents.Instance.OnResetGame != null)
        {
            GameEvents.Instance.OnResetGame(GameManager.Instance.deskSize);
        }
    }

    public void Close()
    {
        Application.Quit();
    }

    #endregion

    #region //Event callbacks
    
    //private void On
    
    #endregion

    #region //Private
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
