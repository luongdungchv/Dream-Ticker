using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCluster : CubeCluster
{
    [SerializeField] private RotateAxis axis;


    private void Update()
    {
        var ray = this.mainCam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out var hitInfo, 100, mask))
            {
                var cube = hitInfo.collider.gameObject.GetComponent<LevelCube>();
                if (this.cubeList.Contains(cube))
                {

                }
            }
        }
    }
    [Sirenix.OdinInspector.Button]
    private void PerformRotation(int sign = 1)
    {
        var currentEuler = transform.eulerAngles;
        if (axis == RotateAxis.X)
        {
            var hasCollision = false;
            foreach (var cube in this.cubeList)
            {
                if (cube.transform == this.transform) continue;
                var pos = new Vector3();
                var cubeLocalPos = cube.transform.position - transform.position;
                pos.x = cubeLocalPos.x;
                pos.y = cubeLocalPos.z * sign;
                pos.z = -cubeLocalPos.y * sign;
                var center = transform.position;
                var radius = cubeLocalPos.magnitude + 0.5f;
                bool collide = false;
                LevelManager.Instance.InterateCube((item, index) =>
                {
                    if (item.transform.position.x != center.x) return;
                    if (collide) return;
                    var dir = item.transform.position - transform.position;
                    var dot1 = Vector3.Dot(dir, cubeLocalPos);
                    var dot2 = Vector3.Dot(dir, pos);
                    if (dot1 < -1 || dot2 < -1) return;
                    collide = DL.Utils.MathUtils.IsCircleOverlapSquare(center.ZY(), radius, item.transform.position.ZY(), 0.5f);
                });
                if (collide)
                {
                    hasCollision = true;
                    break;
                }
            }
            if (!hasCollision)
            {
                currentEuler.x += 90 * sign;
                transform.rotation = Quaternion.Euler(currentEuler);
            }
        }
        else if (axis == RotateAxis.Y)
        {
            var hasCollision = false;
            foreach (var cube in this.cubeList)
            {
                if (cube.transform == this.transform) continue;
                var pos = new Vector3();
                var cubeLocalPos = cube.transform.position - transform.position;
                pos.x = cubeLocalPos.x;
                pos.y = cubeLocalPos.x * sign;
                pos.z = cubeLocalPos.z;
                var center = transform.position;
                var radius = cubeLocalPos.Set(z: 0).magnitude + 0.5f;
                bool collide = false;
                LevelManager.Instance.InterateCube((item, index) =>
                {
                    if (item.transform.position.z != cube.transform.position.z) return;
                    if(item == cube || this.cubeSet.Contains(item)) return;
                    if (collide) return;
                    var dir = (item.transform.position - transform.position).XY();
                    var dot1 = Vector3.Dot(dir, cubeLocalPos.XY());
                    var dot2 = Vector3.Dot(dir, pos.XY());
                    if (dot1 < -0.01f || dot2 < -0.01f) return;
                    collide = DL.Utils.MathUtils.IsCircleOverlapSquare(Vector2.zero, radius, dir, 0.5f);
                    Debug.Log((cube.name, item.name, dir, radius, cubeLocalPos, pos, collide));
                });
                if (collide)
                {
                    hasCollision = true;
                    break;
                }
            }
            if (!hasCollision)
            {
                currentEuler.z += 90 * sign;
                transform.rotation = Quaternion.Euler(currentEuler);
                Debug.Log("rotate");
            }
        }
        else if (axis == RotateAxis.Z)
        {
            var hasCollision = false;
            foreach (var cube in this.cubeList)
            {
                if (cube.transform == this.transform) continue;
                var pos = new Vector3();
                var cubeLocalPos = cube.transform.position - transform.position;
                pos.x = -cubeLocalPos.y * sign;
                pos.y = cubeLocalPos.x * sign;
                pos.z = cubeLocalPos.z;
                var center = transform.position;
                var radius = cubeLocalPos.Set(z: 0).magnitude + 0.5f;
                bool collide = false;
                LevelManager.Instance.InterateCube((item, index) =>
                {
                    if (item.transform.position.z != cube.transform.position.z) return;
                    if(item == cube || this.cubeSet.Contains(item)) return;
                    if (collide) return;
                    var dir = (item.transform.position - transform.position).XY();
                    var dot1 = Vector3.Dot(dir, cubeLocalPos.XY());
                    var dot2 = Vector3.Dot(dir, pos.XY());
                    if (dot1 < -0.01f || dot2 < -0.01f) return;
                    collide = DL.Utils.MathUtils.IsCircleOverlapSquare(Vector2.zero, radius, dir, 0.5f);
                    Debug.Log((cube.name, item.name, dir, radius, cubeLocalPos, pos, collide));
                });
                if (collide)
                {
                    hasCollision = true;
                    break;
                }
            }
            if (!hasCollision)
            {
                currentEuler.z += 90 * sign;
                transform.rotation = Quaternion.Euler(currentEuler);
                Debug.Log("rotate");
            }
        }

    }

    private enum RotateAxis
    {
        X, Y, Z
    }
}
