using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = System.Random;

public class Task2 : MonoBehaviour
{
    public int pointsCount;
    public float passDistance;
    public GameObject prefabObject;
    private GameObject[] runners;

    private GameObject _firstRunner;
    private int _currentRunnerIndex;
    private Vector3 _secondRunnerPosition;
    private bool _needTakePosition;

    void Start()
    {
        SetRandomPointsArray();
    }

    private void Update()
    {
        move();
        checkForward();
    }

    private void move()
    {
        // движение к позиции
        GetCurrentRunner().transform.position = Vector3.MoveTowards(
            GetCurrentRunner().transform.position,
            GetNextRunner().transform.position,
            Time.deltaTime
        );
    }

    private void checkForward()
    {
        if (_needTakePosition)
        {
            _firstRunner.transform.position = Vector3.MoveTowards(
                _firstRunner.transform.position,
                _secondRunnerPosition,
                Time.deltaTime
            );

            if (_firstRunner.transform.position == _secondRunnerPosition)
            {
                _needTakePosition = false;
            }
        }

        if (GetDistance() <= passDistance)
        {
            _firstRunner = GetCurrentRunner();
            _secondRunnerPosition = GetNextRunner().transform.position;

            _currentRunnerIndex = GetNextRunnerIndex();
            GetCurrentRunner().transform.LookAt(GetNextRunner().transform.position);
            GetCurrentRunner().transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }

    private GameObject GetNextRunner()
    {
        return _currentRunnerIndex < runners.Length - 1 ? runners[_currentRunnerIndex + 1] : runners[0];
    }

    private int GetNextRunnerIndex()
    {
        return _currentRunnerIndex < runners.Length - 1 ? _currentRunnerIndex + 1 : 0;
    }

    private GameObject GetCurrentRunner()
    {
        return runners[_currentRunnerIndex];
    }

    private float GetDistance()
    {
        return Vector3.Distance(GetCurrentRunner().transform.position, GetNextRunner().transform.position);
    }

    private void SetRandomPointsArray()
    {
        var random = new Random();
        runners = new GameObject[pointsCount];

        for (var i = 0; i < pointsCount; i++)
        {
            Vector3 vector = new Vector3(random.Next(15, 25), 1, random.Next(45, 65));
            runners[i] = Instantiate(prefabObject, vector, Quaternion.identity);
            runners[i].transform.position = vector;
        }

        for (var i = 0; i < pointsCount - 1; i++)
        {
            GetCurrentRunner().transform.LookAt(GetNextRunner().transform.position);
            GetCurrentRunner().transform.rotation = Quaternion.Euler(0, -90, 0);
        }

    }
}
