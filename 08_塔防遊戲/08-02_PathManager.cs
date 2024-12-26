using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public static PathManager Instance { get; private set; }

    [SerializeField] private Transform[] pathPoints; //  設置簽到點
    private List<Vector2> path = new List<Vector2>();

    private void Awake()
    {
        Instance = this;
        InitializePath();
    }

    private void InitializePath()
    {
        foreach (Transform point in pathPoints)
        {
            path.Add(point.position);
        }
    }

    public List<Vector2> GetPath()
    {
        return path;
    }
}
