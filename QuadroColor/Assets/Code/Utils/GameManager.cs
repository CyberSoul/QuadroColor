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

    #region //Events
    public GameEvents.EventPlayerPhaseChanged OnPlayerPhaseChanged;
    public GameEvents.EventPlayerFigureSelected OnFigureSelected;
    #endregion

    #region //Private fields
    public LevelStartData startData;

    int activePlayerIndex = 0;
    PlayerStepPhase activePhase = PlayerStepPhase.Select;
    #endregion

    #region //Properies

    public GameSettings Settings { get { return gameSettings; } }
    public int CurrentDeskSize { get { return startData.level.deskSize; } }
    public string ActivePlayerName { get { return startData.playerNames[activePlayerIndex]; } }
    public PlayerStepPhase ActiveStepPhase
    {
        get { return activePhase; }
        set
        {
            activePhase = value;
            OnPlayerPhaseChanged.Invoke(ActivePlayerName, activePhase);
        }
    }

    #endregion

    #region //Overrides
    // Start is called before the first frame update
    void Start()
    {
        startData = GlobalDataManager.Instance.activeLevelSettings;
        if (desk != null)
        {
            desk.CreateMap(startData.level.deskSize);
            figureContainer.CreateFigures(startData.level.colorPairs, GetFigure(), GetMaterials());
        }

        ActiveStepPhase = PlayerStepPhase.Select;

        GameEvents.Instance.OnDeskEllementTap += OnDeskTap;
        GameEvents.Instance.OnCollectLine += OnLineCollected;
        GameEvents.Instance.OnResetGame += OnResetGame;
        GameEvents.Instance.OnTurnEnd += OnEndTurnCallback;
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion


    #region //Public
    public Material[] GetMaterials()
    {
        return gameSettings.GetMaterialsByDeskSize(CurrentDeskSize);
    }

    public Figure GetFigure()
    {
        return gameSettings.GetFigure(CurrentDeskSize);
    }
    #endregion

    #region //Event callbacks
    private void OnEndTurnCallback()
    {
        if (ActiveStepPhase == PlayerStepPhase.Select && figureContainer.SelectedFigure != null)
        {
            ++activePlayerIndex;
            if (activePlayerIndex >= startData.playerNames.Length)
            {
                activePlayerIndex = 0;
            }

            ActiveStepPhase = PlayerStepPhase.Place;
        }
    }

    private void OnResetGame(int deskSize)
    {

    }

    private void OnDeskTap(DeskEllement ellement)
    {
        if (GameManager.Instance.ActiveStepPhase == PlayerStepPhase.Place)
        {
            if (figureContainer.SelectedFigure != null)
            {
                var figure = figureContainer.SelectedFigure;
                ellement.AttachFigure(figure);
                figure.Attach();

                GameEvents.Instance.OnAttachFigure(ellement, figure);
                ActiveStepPhase = PlayerStepPhase.Select;
            }
        }
    }

    private void OnLineCollected()
    {
        if (GameEvents.Instance.OnGameComplete != null)
        {
            GameEvents.Instance.OnGameComplete();
        }

        Debug.Log("CONGRATULATIONS!");
    }
    #endregion

    #region //Private
    #endregion
}
