using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable][CreateAssetMenu(fileName = "NewSettings", menuName = "Quadro/Settings", order = 1)]
public class GameSettings : ScriptableObject
{
    #region // SerializeFields
    [SerializeField] Material[] possibleMaterials = new Material[14]; //For current max value of 7
    [SerializeField] Figure[] figures = new Figure[5];
    #endregion

    #region //Private fields
    #endregion

    #region //Properies
    #endregion

    #region //Public
    public Material[] GetMaterialsByDeskSize(int a_deskSize)
    {
        return GetMaterialsByCount(a_deskSize * 2);
    }

    public Material[] GetMaterialsByCount(int a_requiredAmount)
    {
        Material[] result = new Material[a_requiredAmount];

        for (int i = 0; i < a_requiredAmount; ++i)
        {
            result[i] = possibleMaterials[i];
        }

        return result;
    }

    public Figure GetFigure(int deskSize)
    {
        return figures[deskSize - 3]; //start from desk with size 3.
    }
    #endregion

    #region //Private
    #endregion
}
