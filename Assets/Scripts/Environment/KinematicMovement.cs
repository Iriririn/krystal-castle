using UnityEngine;

public class KinematicMovement : MonoBehaviour
{
       [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private float _speed;

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement ()
    {
        float time = Mathf.PingPong(Time.time * _speed, 1);
        transform.position = Vector3.Lerp(_pointA.position, _pointB.position, time);
    }
}
