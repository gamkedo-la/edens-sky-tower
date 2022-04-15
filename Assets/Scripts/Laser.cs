using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private int _maxBounce = 20;

    private int _count;
    private LineRenderer _laser;

    [SerializeField]
    private Vector3 _offSet;

    private void Start()
    {
        _laser = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        _count = 0;
        castLaser(transform.position + _offSet, transform.up);
    }
    private void castLaser(Vector3 position, Vector3 direction)
    {
        _laser.SetPosition(0, transform.position + _offSet);

        for (int i = 0; i < _maxBounce; i++)
        {

            Ray ray = new Ray(position, direction);
            RaycastHit hit;

            if (_count < _maxBounce - 1)
                _count++;
            if (Physics.Raycast(ray, out hit, 300))
            {

                position = hit.point;
                direction = Vector3.Reflect(direction, hit.normal);
                _laser.SetPosition(_count, hit.point);

                if (hit.transform.tag != "Mirror")
                {

                    for (int j = (i + 1); j < _maxBounce; j++)
                    {
                        if (j >= 0 && j < _laser.positionCount)
                        {
                            _laser.SetPosition(j, hit.point);
                        }
                            

                    }
                    break;
                }
                else
                {

                    _laser.SetPosition(_count, hit.point);
                }
            }
            else
            {
                if(_count >= 0 && _count < _laser.positionCount)
                {
                     _laser.SetPosition(_count, ray.GetPoint(300));
                }
               

            }


        }

    }
}
