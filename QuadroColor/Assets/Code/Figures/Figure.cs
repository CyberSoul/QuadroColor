using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //For func and actions.

public class Figure : MonoBehaviour
{
    #region //SerializeFields
    [SerializeField] [Range(3, 7)] int size = 3;

    [Header("Visual")]
    [SerializeField] MeshRenderer[] renderers;
    [SerializeField] GameObject selectedView;
    #endregion

    #region //Events
    //public Action<Figure> OnTap;
    #endregion

    #region //Private fields
    [SerializeField] Material[] materials;

    public bool isAttached = false;
    #endregion

    #region //Properies
    public bool IsAttached { get { return isAttached; } }

    public Material[] Materials { get { return materials; } }
    #endregion

    #region //Overrides
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.Instance.OnResetGame += OnResetGame;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseUpAsButton()
    {
        if ( GameEvents.Instance.OnFigureTap != null && !isAttached)
        {
            GameEvents.Instance.OnFigureTap(this);
        }
    }
    #endregion

    #region //Public
    public void AddMaterial(Material a_material)
    {
        for (int i =0; i < materials.Length; ++i)
        {
            if (materials[i] == null)
            {
                AddMaterial(a_material, i);
                break;
            }
        }
    }

    public void AddMaterial(Material a_material, int index)
    {
        if ( index >= 0  && index < renderers.Length)
        {
            renderers[index].material = a_material;
            materials[index] = a_material;
        }
    }

    public void Select()
    {
        Debug.Log("Figure Select");
        selectedView.active = true;
    }

    public void UnSelect()
    {
        Debug.Log("Figure UnSelect");
        selectedView.active = false;
    }

    public void Attach()
    {
        UnSelect();
        isAttached = true;
    }
    #endregion

    #region //Private
    #endregion

    #region //Event callbacks

    public void OnResetGame(int deskSize)
    {
        isAttached = false;
    }

    #endregion

    #region //Editor part
    private void OnValidate()
    {
        renderers = new MeshRenderer[size];
        materials = new Material[size];

        var foundRenderers = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < foundRenderers.Length && i < size; ++i)
        {
            renderers[i] = foundRenderers[i];
        }

        if (size != foundRenderers.Length)
            Debug.LogError($"Incorrect size of renderers and required amount. Amount = {size}, but renderers found: {foundRenderers.Length}");
    }
    #endregion
}
