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
    #endregion

    #region //Private fields
    #endregion

    #region //Properies
    #endregion


    #region //Overrides
    #endregion


    #region //Public
    #endregion


    #region //Private
    #endregion

    #region //Editor part
    #endregion
}
