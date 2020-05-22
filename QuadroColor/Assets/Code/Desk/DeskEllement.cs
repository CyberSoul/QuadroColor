﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskEllement : MonoBehaviour
{
    #region //SerializeFields
    [SerializeField] Transform attachPosition;
    #endregion

    #region //Private fields
    private Figure attachedFigure = null;
    #endregion

    #region //Properies
    public bool IsOccupied { get { return attachedFigure != null; } }
    public Figure Occupier { get { return attachedFigure; } }
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

    public void AttachFigure( Figure a_figure)
    {
        a_figure.transform.SetParent(attachPosition);
        a_figure.transform.localPosition = new Vector3(0, 0, 0);
    }

    #endregion

    #region //Private
    #endregion

    #region //Editor part
    private void OnValidate()
    {
        /*if (attachedFigure!= null)
        {
            AttachFigure(attachedFigure);
        }*/
    }
    #endregion
}
