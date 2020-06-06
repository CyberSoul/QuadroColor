using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //For func and actions.
using UnityEngine.Events;

public class GameEvents : SingletonTemplate<GameEvents>
{

    #region //SerializeFields
    #endregion

    #region //Events

    [System.Serializable] public class EventGameScene : UnityEvent<SceneList, SceneList> { }
    [System.Serializable] public class EventFadeComplete : UnityEvent<bool> { }

    [System.Serializable] public class EventPlayerPhaseChanged : UnityEvent<string, PlayerStepPhase> { }
    [System.Serializable] public class EventPlayerFigureSelected : UnityEvent<Figure> { }

    public Action<Figure> OnFigureTap;
    public Action<DeskEllement> OnDeskEllementTap;
    public Action<DeskEllement, Figure> OnAttachFigure;
    public Action OnCollectLine;

    public Action OnGameComplete;
    public Action<int> OnResetGame;
    public Action OnTurnEnd;
    #endregion

    #region //Private fields
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
    #endregion

    #region //Editor part
    #endregion
}
