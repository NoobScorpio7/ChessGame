using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAXDEPTH : MonoBehaviour
{
    public static MAXDEPTH Instance { set; get; }

    public int MaxDepth = 0;
    void Start()
    {
        Instance = this;
    }



}
