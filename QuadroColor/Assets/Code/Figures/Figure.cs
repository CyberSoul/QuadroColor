using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    #region //SerializeFields
    [SerializeField] [Range(3, 7)] int size = 3; 

    [Header("Visual")]
    [SerializeField] MeshRenderer[] renderers;
    #endregion

    #region //Private fields
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
    #endregion

    #region //Private
    #endregion

    #region //Editor part
    private void OnValidate()
    {
        renderers = new MeshRenderer[size];

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
