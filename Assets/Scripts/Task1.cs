using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class Task1 : MonoBehaviour
{
    public int pointsCount;
    private Vector3[] _points;
    private int _currentPointIndex;
    private bool _forward = true;

    private void Start()
    {
        SetRandomPointsArray();
        transform.LookAt(_points[_currentPointIndex]);
    }

    private void Update()
    {
        move();
        checkForward();
    }

    private void move()
    {
        // движение к позиции
        transform.position = Vector3.MoveTowards(
            transform.position,
            _points[_currentPointIndex],
            Time.deltaTime
        );
    }

    private void checkForward()
    {
        if (transform.position == _points[_currentPointIndex])
        {
            // меняем направление
            if ((_forward && _currentPointIndex == pointsCount - 1)  // вперед
                || (!_forward && _currentPointIndex == 0))           // назад
            {
                _forward = !_forward;
            }

            _currentPointIndex = _forward ? _currentPointIndex += 1 : _currentPointIndex -= 1;
            transform.LookAt(_points[_currentPointIndex]); // поворачиваемся
        }
    }

    private void SetRandomPointsArray()
    {
        var random = new Random();
        _points = new Vector3[pointsCount];

        for (var i = 0; i < pointsCount; i++)
            _points[i] = new Vector3(random.Next(0, 6), 0, random.Next(0, 6));
    }
}
