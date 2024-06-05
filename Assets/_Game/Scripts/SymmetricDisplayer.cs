using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymmetricDisplayer : MonoBehaviour
{
    public static SymmetricDisplayer Instance;
    [SerializeField] private Mirror mirror;
    [SerializeField] private int projectionSide;
    [SerializeField] private Material projImageMaterial;
    [SerializeField] private LevelCube projImagePrefab;

    public int ProjectionSide => this.projectionSide;

    

    private void Awake(){
        Instance = this;
    }
    [Sirenix.OdinInspector.Button]
    public void Project()
    {
        mirror.SetSide(this.projectionSide);
        LevelManager.Instance.InterateCube((item, index) =>
        {
            if (projectionSide == 0 && item.transform.position.z > 0)
            {
                var projImagePos = item.transform.position;
                projImagePos.z = -projImagePos.z;
                var projImage = Instantiate(projImagePrefab, projImagePos, Quaternion.identity);
                projImage.GetComponent<MeshRenderer>().sharedMaterial = projImageMaterial;
                item.SetProjectionImage(projImage);
                LevelManager.Instance.AddProjectionImage(projImage);
            }
            else if (projectionSide == 1 && item.transform.position.x > 0)
            {
                var projImagePos = item.transform.position;
                projImagePos.x = -projImagePos.x;
                var projImage = Instantiate(projImagePrefab, projImagePos, Quaternion.identity);
                projImage.GetComponent<MeshRenderer>().sharedMaterial = projImageMaterial;
                item.SetProjectionImage(projImage);
                LevelManager.Instance.AddProjectionImage(projImage);
            }
        });
    }
}
