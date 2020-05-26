using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //For func and actions.

public class GameEvents : SingletonTemplate<GameEvents>
{

    #region //SerializeFields
    #endregion

    #region //Events
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
