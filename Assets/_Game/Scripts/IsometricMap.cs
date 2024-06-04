using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricMap : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Dictionary<Vector2Int, bool> occupationMap;

    private void Awake() {
        occupationMap = new Dictionary<Vector2Int, bool>();
    }

    public void AddCube(LevelCube cube){
        var cube3DPos = cube.transform.position;
        var isometricPos = new Vector2Int((int)cube3DPos.x, (int)cube3DPos.y);
        isometricPos += Vector2Int.one * (int)cube3DPos.z;
        if(occupationMap.ContainsKey(isometricPos)) occupationMap.Add(isometricPos, true);
        else occupationMap[isometricPos] = true;
    }

    public void RemapCubes(List<LevelCube> cubeList){
        occupationMap.Clear();
        foreach(var cube in cubeList){
            AddCube(cube);
        }
    }

    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int end){
        var open = new List<Node>();
        var close = new List<Node>();

        Vector2Int[] dirList = {Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left};
        close.Add(new Node(start, 0, 0));
        foreach(var dir in dirList){
            var coord = start + dir;
            if(!CheckCoord(coord)) continue;
            open.Add(new Node(coord, CalculateManhattan(coord, end), CalculateManhattan(coord, start)));
        }

        bool finished = false;
        while(!finished){
            var minNode = open[0];
            foreach(var openNode in open){
                if(openNode.f < minNode.f) minNode = openNode;
            }
            if(minNode.coord == end){
                return GetPath(minNode);
            }
            open.Remove(minNode);
            close.Add(minNode);   
        }
        return null;
    }

    private List<Vector2Int> GetPath(Node endNode){
        var result = new List<Vector2Int>();
        var currentNode = endNode;
        while(currentNode.HasParent){
            result.Add(currentNode.coord);
            currentNode = currentNode.parent;
        }
        result.Reverse();
        return result;
    }

    private bool CheckCoord(Vector2Int coord){
        return occupationMap.ContainsKey(coord) && occupationMap[coord];
    }

    private float CalculateManhattan(Vector2Int start, Vector2Int end){
        return Mathf.Abs(start.x - end.x) + Mathf.Abs(start.y - end.y);
    }

    public class Node{
        public Vector2Int coord;
        public Node parent;
        public float g, h;
        public float f => g + h;
        public bool HasParent => this.parent != null;
        public Node(Vector2Int coord, float g, float h){
            this.coord = coord;
            this.g = g;
            this.h = h;
        }
        public Node(Vector2Int coord, float g, float h, Node parent): this(coord, g, h){
            this.parent = parent;
        }
    }
}
