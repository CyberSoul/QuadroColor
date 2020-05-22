using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiguresContainer : MonoBehaviour
{
    #region //SerializeFields
    [SerializeField][Range(0, 10)] float figureStep = 1;

    #endregion

    #region //Private fields
    public List<Figure> figures;

    Vector3 startPosition = Vector3.negativeInfinity;
    #endregion

    #region //Properies
    #endregion

    #region //Overrides
    private void Awake()
    {
        //Debug.Log(startPosition);
       // Debug.Log("setup start position: " + transform.localPosition);
        //if (startPosition == Vector3.negativeInfinity)
        {
            Debug.Log("setup start position: " + transform.localPosition);
            startPosition = transform.localPosition;
        }
    }

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
    public void CreateFigures(int a_deckSize, Figure a_figurePrefab, Material[] a_materials)
    {
        Debug.Log("Create figures");
        int figureAmount = a_deckSize * a_deckSize;
        if (a_deckSize % 2 == 1)
        {
            figureAmount -= 1; //Special case for odd deck size
        }

        // for (int i =0; i < figureAmount; ++i)
        for (int i = 0; i < a_deckSize; ++i)
            for (int j = 0; j < a_deckSize; ++j)
            {
                var figure = Instantiate(a_figurePrefab, transform);
                figure.transform.localPosition = new Vector3((i - a_deckSize / 2f) * figureStep, 0, (j - a_deckSize / 2f) * figureStep);
                figures.Add(figure);
            }
        
        transform.localPosition = new Vector3(startPosition.x + figureStep / 2, startPosition.y, startPosition.z + figureStep / 2);

        SetupColors(a_materials);
    }
    #endregion

    #region //Private

    void SetupColors(Material[] a_materials)
    {
        SetColorsRecurs(0, figures.Count/2, 0, 0, a_materials);
        SetColorsRecurs(figures.Count / 2, figures.Count, 1, 0, a_materials);
    }

    void SetColorsRecurs(int a_startIndex, int a_endIndex, int a_materialIndex, int a_recursStep, Material[] a_materials)
    {
        //Debug.Log($"a_startIndex = {a_startIndex}; a_endIndex = {a_endIndex}; a_materialIndex = {a_materialIndex}; a_recursSte = {a_recursStep}");
        for (int i = a_startIndex; i < a_endIndex; ++i)
        {
            figures[i].AddMaterial(a_materials[a_materialIndex]);
        }
        ++a_recursStep;
        if ((a_endIndex - a_startIndex) > 1 && a_materialIndex < a_materials.Length)
        {
            int halfIndex = (a_startIndex + a_endIndex) / 2;

            SetColorsRecurs(a_startIndex, halfIndex, a_recursStep * 2 + 0, a_recursStep, a_materials);
            SetColorsRecurs(halfIndex, a_endIndex, a_recursStep * 2 + 1, a_recursStep, a_materials);
        }
    }

    #endregion

    #region //Editor part
    private void OnValidate()
    {


    }
    #endregion
}
