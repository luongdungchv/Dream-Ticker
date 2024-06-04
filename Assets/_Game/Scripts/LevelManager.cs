using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] private List<LevelCube> cubeList;

    private void Awake(){
        Instance = this;
    }

    public void InterateCube(UnityAction<LevelCube, int> callback){
        for (int i = 0; i < cubeList.Count; i++)
        {
            callback?.Invoke(cubeList[i], i);
        }
    }
}
