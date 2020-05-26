using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Desk : MonoBehaviour
{
    #region //SerializeFields
    [SerializeField] bool IsAutoGenerateOnValidate = false;
    [SerializeField] [Range(3, 7)] int size = 3;
    /*[SerializeField] [Range(3, 7)] int width = 3;
    [SerializeField] [Range(3, 7)] int height = 3;*/
    [SerializeField] [Range(0, 20)] float ellementStep = 1;

    [Header("Prefabs")]
    [SerializeField] Transform deskView;
    [SerializeField] GameObject deskEllementContainer;
    //[SerializeField] Transform borderView;//TO_DO
    [SerializeField] DeskEllement deskEllementPrefab;

#if UNITY_EDITOR

#endif
    #endregion

    #region //Private fields
    DeskEllement[,] deskEllements;
    #endregion

    #region //Properies
    public int Size { get { return size; } }
    public int Height { get { return size; } }
    public int Width { get { return size; } }
    #endregion

    #region //Overrides

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.Instance.OnAttachFigure += OnFigureAttached;

        /*if (deskEllements == null)
        {
            CreateMap();
        }*/

    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion

    #region //Public
    public void CreateMap(int a_deskSize)
    {
        size = a_deskSize;
        //Delay for correct destroy.
        RemoveEllements();
        CreateMap();
    }

    public Vector2Int GetDeskEllemenIndexes(DeskEllement a_ellement)
    {
        for (int i = 0; i < deskEllements.GetLength(0); ++i)
            for (int j = 0; j < deskEllements.GetLength(1); ++j)
            {
                if (deskEllements[i, j] == a_ellement)
                {
                    return new Vector2Int(i, j);
                }
            }

        Debug.LogError("Can not find ellement");
        return new Vector2Int(0, 0);
    }
    #endregion

    #region //Event callbacks

    void OnFigureAttached( DeskEllement a_ellement, Figure a_figure )
    {
        //Check for complete lines.
        CheckWinCondition(a_ellement);
    }

    #endregion

    #region //Private

    #region //Generation
    void CreateMap()
    {
        Debug.Log("CreateMap");
        if (deskView != null && deskEllementPrefab != null)
        {
            if (deskEllementContainer == null)
            {
                deskEllementContainer = this.gameObject;
            }

            deskView.localScale = new Vector3(Width * ellementStep, deskView.localScale.y, Height * ellementStep);

            deskEllements = new DeskEllement[Width, Height];
            for (int i = 0; i < deskEllements.GetLength(0); ++i)
            {
                for (int j = 0; j < deskEllements.GetLength(1); ++j)
                {
                    DeskEllement ellement = Instantiate(deskEllementPrefab, deskEllementContainer.transform, false) as DeskEllement;

                    ellement.transform.localPosition = new Vector3((i - Width / 2f )* ellementStep , 0, (j - Height / 2f )* ellementStep );
                    deskEllements[i, j] = ellement;
                }
            }
        }

        deskEllementContainer.transform.localPosition = new Vector3(ellementStep / 2, deskEllementContainer.transform.localPosition.y, ellementStep / 2);

        /*var render = deskEllementContainer.GetComponent<Renderer>();
        if (render != null)
        {
            Vector3 rendererSize = render.bounds.size;
            deskEllementContainer.transform.localPosition = new Vector3(-rendererSize.x/2, deskEllementContainer.transform.localPosition.y, -rendererSize.z / 2);
        }*/

        /*float offsetPos = -ellementStep * size / 2;
        deskEllementContainer.transform.localPosition = new Vector3(offsetPos, deskEllementContainer.transform.localPosition.y, offsetPos);*/
    }

    void RemoveEllements()
    {
        Debug.Log("RemoveEllements");
        var deskEllements = GameObject.FindGameObjectsWithTag("deskEllement");
        for (int i = 0; i < deskEllements.Length; ++i)
        {
            if (Application.isEditor)
            {
                DestroyImmediate(deskEllements[i]);
            }
            else
            {
                Destroy(deskEllements[i]);
            }
        }

        /*if (deskEllements != null)
        {
            for (int i = 0; i < deskEllements.GetLength(0); ++i)
            {
                for (int j = 0; j < deskEllements.GetLength(1); ++j)
                {
                    {
                        if (Application.isEditor)
                        {
                            DestroyImmediate(deskEllements[i, j]);
                        }
                        else
                        {
                            Destroy(deskEllements[i, j]);
                        }
                    }
                }
            }
            deskEllements = null;
        }*/
    }
    #endregion

    #region //Check win condition

    void CheckWinCondition(DeskEllement a_updatedEllement)
    {
        //Should find position of updated figure.
        Vector2Int indexes = GetDeskEllemenIndexes(a_updatedEllement);

        bool isWin = CheckWinCondition(new Vector2Int(indexes.x, 0), new Vector2Int(0, 1));
        if (!isWin)
        {
            isWin = CheckWinCondition(new Vector2Int(0, indexes.y), new Vector2Int(1, 0));
            if (!isWin)
            {
                if (indexes.x == indexes.y)
                {
                    //Diagonal check
                    isWin = CheckWinCondition(new Vector2Int(0, 0), new Vector2Int(1, 1));
                }

                if (!isWin)
                {
                    if ((indexes.x + indexes.y) == (deskEllements.GetLength(0) - 1))
                    {
                        //Diagonal check
                        isWin = CheckWinCondition(new Vector2Int(0, 3), new Vector2Int(1, -1));
                    }
                }
            }
        }

        if (isWin)
        {
            GameEvents.Instance.OnCollectLine();
        }
    }

    bool CheckWinCondition(Vector2Int a_startIndex, Vector2Int a_indexStep)
    {
        List<Material> figuresMaterials = null;
        
        for (int i = 0; i < deskEllements.GetLength(0); ++i)
        {
            if (deskEllements[a_startIndex.x, a_startIndex.y].IsOccupied)
            {
                var materials = deskEllements[a_startIndex.x, a_startIndex.y].Occupier.Materials;
                if (figuresMaterials == null)
                {
                    figuresMaterials = new List<Material>();
                    figuresMaterials.AddRange(materials);
                }
                else
                {
                    figuresMaterials = figuresMaterials.Where(obj => materials.Contains(obj)).ToList();
                }
            }
            else
            {
                return false;
            }
            a_startIndex += a_indexStep;
        }

        return figuresMaterials.Count > 0;
    }
    #endregion
    
    #endregion

    #region //Editor part

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (IsAutoGenerateOnValidate && !Application.isPlaying )
        {
            UnityEditor.EditorApplication.delayCall += () =>
            {
                //Delay for correct destroy.
                RemoveEllements();
                CreateMap();
            };
        }
#endif
    }

#endregion
}
