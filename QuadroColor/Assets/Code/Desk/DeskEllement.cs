using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //For func and actions.

public class DeskEllement : MonoBehaviour
{
    #region //SerializeFields
    [SerializeField] Transform attachPosition;
    [SerializeField] Material highlightMaterial;
    [SerializeField] MeshRenderer meshView;
    #endregion

    #region //Events

    public Action<DeskEllement> OnTap;

    #endregion

    #region //Private fields
    private Figure attachedFigure = null;
    private Material defaultMaterial;
    #endregion

    #region //Properies
    public bool IsOccupied { get { return attachedFigure != null; } }
    public Figure Occupier { get { return attachedFigure; } }
    #endregion

    #region //Overrides
    // Start is called before the first frame update
    void Start()
    {
        defaultMaterial = meshView.material;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseEnter()
    {
        meshView.material = highlightMaterial;
    }

    private void OnMouseExit()
    {
        meshView.material = defaultMaterial;
    }

    private void OnMouseUpAsButton()
    {
        if (GameEvents.Instance.OnDeskEllementTap != null)
        {
            GameEvents.Instance.OnDeskEllementTap(this);
        }
    }
    #endregion

    #region //Public

    public void AttachFigure( Figure a_figure)
    {
        a_figure.transform.SetParent(attachPosition);
        a_figure.transform.localPosition = new Vector3(0, 0, 0);
    }

    #endregion

    #region //Event callbacks
    
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
