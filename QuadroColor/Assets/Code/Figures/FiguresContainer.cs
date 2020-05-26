using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiguresContainer : MonoBehaviour
{
    #region //SerializeFields
    [SerializeField][Range(0, 10)] float figureStep = 1;
    [SerializeField] int widthFigureAmount = 4;

    [SerializeField] GameObject selecetedView;

    #endregion

    #region //Events
    #endregion

    #region //Private fields
    public List<Figure> figures;
    public Figure selectedFigure = null;

    Vector3 startPosition = Vector3.negativeInfinity;
    #endregion

    #region //Properies

    public Figure SelectedFigure { get { return selectedFigure; } }

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
        GameEvents.Instance.OnFigureTap += OnFigureTap;
        GameEvents.Instance.OnAttachFigure += OnFigureAttached;
        GameEvents.Instance.OnResetGame += OnResetGame;
        //GameEvents.Instance.OnDeskEllementTap += OnDeskTap;
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

        int heightFigureAmount = figureAmount / widthFigureAmount;
        
        // for (int i =0; i < figureAmount; ++i)
        for (int i = 0; i < widthFigureAmount; ++i)
            for (int j = 0; j < heightFigureAmount; ++j)
            {
                var figure = Instantiate(a_figurePrefab, transform);
                figure.transform.localPosition = new Vector3((i - widthFigureAmount / 2f) * figureStep, 0, (j - heightFigureAmount / 2f) * figureStep);
                figures.Add(figure);
            }
        
        transform.localPosition = new Vector3(startPosition.x + figureStep / 2, startPosition.y, startPosition.z + figureStep / 2);

        SetupColors(a_materials);
    }
    #endregion

    #region //Event callbacks
    void OnResetGame(int deskSize)
    {
        if (selectedFigure != null)
        {
            selectedFigure.UnSelect();
        }
        selectedFigure = null;

        int index = 0;
        foreach (var figure in figures)
        {
            int i = index / deskSize;
            int j = index % deskSize;
            figure.transform.parent = transform;
            figure.transform.localPosition = new Vector3(( i - deskSize / 2f) * figureStep, 0, ( j - deskSize / 2f) * figureStep);
            ++index;
        }
        /*for (int i = 0; i < figures.; ++i)
            for (int j = 0; j < heightFigureAmount; ++j)
            {
                var figure = Instantiate(a_figurePrefab, transform);
                figure.transform.localPosition = new Vector3((i - widthFigureAmount / 2f) * figureStep, 0, (j - heightFigureAmount / 2f) * figureStep);
                figures.Add(figure);
            }*/
    }

    private void OnFigureTap(Figure a_figure)
    {
        Debug.Log("OnFigureTap");
        bool isSame = a_figure == selectedFigure;

        //TO_DO check for attached
        if (selectedFigure != null)
        {
            selectedFigure.UnSelect();
        }

        if (!isSame)
        {
            selectedFigure = a_figure;
            selectedFigure.Select();
        }
        else
        {
            selectedFigure = null;
        }
    }

    private void OnFigureAttached(DeskEllement a_deskEllement, Figure a_figure)
    {
        selectedFigure = null;
    }

    /*private void OnDeskTap( DeskEllement ellement )
    {
        if( selectedFigure != null )
        {
            ellement.AttachFigure(selectedFigure);
            selectedFigure.Attach();

            selectedFigure = null;
        }
    }*/
    #endregion

    #region //Private

    void SetupColors(Material[] a_materials)
    {
        SetColorsRecurs(0, figures.Count/2, 0, 0, a_materials);
        SetColorsRecurs(figures.Count / 2, figures.Count, 1, 0, a_materials);
    }

    void SetColorsRecurs(int a_startIndex, int a_endIndex, int a_materialIndex, int a_recursStep, Material[] a_materials)
    {
        Debug.Log($"a_startIndex = {a_startIndex}; a_endIndex = {a_endIndex}; a_materialIndex = {a_materialIndex}; a_recursSte = {a_recursStep}");
        for (int i = a_startIndex; i < a_endIndex; ++i)
        {
            Debug.Log($"Apply color with index {a_materialIndex} to item in list {i}");
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
