using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiguresContainer : MonoBehaviour
{
    #region //SerializeFields

    #endregion

    #region //Private fields

    public List<Figure> figures;
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
    void CreateFigures(int a_deckSize)
    {
        int figureAmount = a_deckSize * a_deckSize;
        if (a_deckSize % 2 == 1)
        {
            figureAmount -= 1; //Special case for odd deck size
        }

    }
    #endregion

    #region //Private
    #endregion

    #region //Editor part
    private void OnValidate()
    {


    }
    #endregion
}
