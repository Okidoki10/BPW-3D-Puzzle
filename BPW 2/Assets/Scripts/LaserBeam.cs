using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{

    Vector3 pos, dir;

    public GameObject laserObj;
    LineRenderer laser;
    List<Vector3> LaserIndices = new List<Vector3>();

    public LaserBeam(Vector3 pos, Vector3 dir, Material material)
    {
        this.laser = new LineRenderer();
        this.laserObj = new GameObject();
        this.laserObj.name = "Laser Beam";
        this.pos = pos;
        this.dir = dir;

        this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = 0.1f;
        this.laser.endWidth = 0.1f;
        this.laser.material = material;
        this.laser.startColor = Color.red;
        this.laser.endColor = Color.red;

        CastRay(pos, dir, laser);
    }

    void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser)
    {
        LaserIndices.Add(pos);
        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, 1))
        {
            CheckHit(hit, dir, laser);
        } 
        else
        {
            LaserIndices.Add(ray.GetPoint(100));
            UpdateLaser();
        }
    }

    void UpdateLaser()
    {
        int count = 0;
        laser.positionCount = LaserIndices.Count;

        foreach(Vector3 idx in LaserIndices)
        {
            laser.SetPosition(count, idx);
            count++;
        }
    }

    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser)
    {
        if (hitInfo.collider.gameObject.tag == "Mirror")
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);

            CastRay(pos, dir, laser);
        }
        else if (hitInfo.collider.gameObject.tag == "LaserActivator")
        {
            hitInfo.collider.gameObject.GetComponent<ActivatorScript>().OnHit();
            LaserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
        else
        {
            LaserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
    }


}
