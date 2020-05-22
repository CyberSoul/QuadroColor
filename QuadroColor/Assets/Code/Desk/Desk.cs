using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] GameObject deskEllementPrefab;

#if UNITY_EDITOR

#endif
#endregion

    #region //Private fields
    GameObject[,] deskEllements;
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
        UnityEditor.EditorApplication.delayCall += () =>
        {
            size = a_deskSize;
            //Delay for correct destroy.
            RemoveEllements();
            CreateMap();
        };
    }
    #endregion

    #region //Private

    void CreateMap()
    {
        if (deskView != null && deskEllementPrefab != null)
        {
            if (deskEllementContainer == null)
            {
                deskEllementContainer = this.gameObject;
            }

            deskView.localScale = new Vector3(Width * ellementStep, deskView.localScale.y, Height * ellementStep);

            deskEllements = new GameObject[Width, Height];
            for (int i = 0; i < deskEllements.GetLength(0); ++i)
            {
                for (int j = 0; j < deskEllements.GetLength(1); ++j)
                {
                    var ellement = Instantiate(deskEllementPrefab, deskEllementContainer.transform, false);

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

    #region //Editor part

    private void OnValidate()
    {
        if (IsAutoGenerateOnValidate && !Application.isPlaying )
        {
            UnityEditor.EditorApplication.delayCall += () =>
            {
                //Delay for correct destroy.
                RemoveEllements();
                CreateMap();
            };
        }
    }

    #endregion
}
