using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region // SerializeFields
    [SerializeField] GameSettings gameSettings;

    [SerializeField] Desk desk;
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
        }
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

    #region //Private
    #endregion
}
