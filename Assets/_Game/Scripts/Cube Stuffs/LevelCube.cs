using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCube : MonoBehaviour
{
    private LevelCube projImage;

    public void SetProjectionImage(LevelCube projImage){
        this.projImage = projImage;
    }

    public void SynchronizeProjectionImage(int side){
        if(this.projImage == null) return;
        if(side == 0){
            projImage.transform.position = transform.position.Set(z: -transform.position.z);
            var eulerAngles = transform.eulerAngles;
            var projEuler = eulerAngles;
            projEuler.y = -projEuler.y;
            projEuler.x = -projEuler.x;
            this.projImage.transform.rotation = Quaternion.Euler(projEuler);
        }
        else if(side == 1){
            projImage.transform.position = transform.position.Set(x: -transform.position.x);
            var eulerAngles = transform.eulerAngles;
            var projEuler = eulerAngles;
            projEuler.y = -projEuler.y;
            projEuler.z = -projEuler.z;
            this.projImage.transform.rotation = Quaternion.Euler(projEuler);
        }
    }
}
