using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonTemplate<GameManager>
{
    #region // SerializeFields
    [SerializeField] GameSettings gameSettings;

    [SerializeField] Desk desk;
    [SerializeField] FiguresContainer figureContainer;
    #endregion

    #region //Private fields

    public int deskSize;

    #endregion

    #region //Properies

    public GameSettings Settings { get { return gameSettings; } }

    #endregion

    #region //Overrides
    // Start is called before the first frame update
    void Start()
    {
        if (desk != null)
        {
            desk.CreateMap(deskSize);
            figureContainer.CreateFigures(deskSize, GetFigure(), GetMaterials());
        }
        GameEvents.Instance.OnDeskEllementTap += OnDeskTap;
        GameEvents.Instance.OnCollectLine += OnLineCollected;
        GameEvents.Instance.OnResetGame += OnResetGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion


    #region //Public
    public Material[] GetMaterials()
    {
        return gameSettings.GetMaterialsByDeskSize(deskSize);
    }

    public Figure GetFigure()
    {
        return gameSettings.GetFigure(deskSize);
    }
    #endregion

    #region //Event callbacks
    private void OnResetGame(int deskSize)
    {

    }

    private void OnDeskTap(DeskEllement ellement)
    {
        if (figureContainer.SelectedFigure != null)
        {
            var figure = figureContainer.SelectedFigure;
            ellement.AttachFigure(figure);
            figure.Attach();

            GameEvents.Instance.OnAttachFigure(ellement, figure);
        }
    }

    private void OnLineCollected()
    {
        GameEvents.Instance.OnGameComplete();
        Debug.Log("CONGRATULATIONS!");
    }
    #endregion

    #region //Private
    #endregion
}
