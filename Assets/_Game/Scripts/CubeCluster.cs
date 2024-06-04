using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCluster : MonoBehaviour
{
    [SerializeField] protected List<LevelCube> cubeList;
    [SerializeField] protected LayerMask mask;
    protected Camera mainCam;
    protected HashSet<LevelCube> cubeSet;

    protected void Awake(){
        mainCam = Camera.main;
        cubeSet = new(cubeList);
    }
}
