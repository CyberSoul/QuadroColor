using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonTemplate<UIManager>
{
    #region //SerializeFields
    [SerializeField] GameObject WinPopup;
    [SerializeField] Text PlayerName;
    [SerializeField] Button EndTurnBtn;
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
        GameEvents.Instance.OnGameComplete += OnGameComplete;
        GameManager.Instance.OnPlayerPhaseChanged.AddListener(OnPlayerPhaseChanged);
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region //Public

    public void ResetGame()
    {
        WinPopup.active = false;
        if (GameEvents.Instance.OnResetGame != null)
        {
            GameEvents.Instance.OnResetGame(GameManager.Instance.CurrentDeskSize);
            GameManager.Instance.OnPlayerPhaseChanged.AddListener(OnPlayerPhaseChanged);
            GameManager.Instance.OnFigureSelected.AddListener(OnFigureSlected);
        }
    }

    public void Close()
    {
        Application.Quit();
    }

    public void EndTurn()
    {
        GameEvents.Instance.OnTurnEnd();
    }

    #endregion

    #region //Event callbacks

    public void OnGameComplete()
    {
        WinPopup.active = true;
    }

    private void OnPlayerPhaseChanged(string a_playerName, PlayerStepPhase a_phase)
    {
        PlayerName.text = a_playerName + "\n";
        switch (a_phase)
        {
            case PlayerStepPhase.Place:
                PlayerName.text += "Please place selected figure";
                EndTurnBtn.interactable = false;
                break;

            case PlayerStepPhase.Select:
                PlayerName.text += "Please select figure for opponent";
                EndTurnBtn.interactable = false;
                break;
        }
    }
    private void OnFigureSlected(Figure a_figure)
    {
        int i = 0;
        ++i;
        EndTurnBtn.interactable = true;
    }    
    
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
