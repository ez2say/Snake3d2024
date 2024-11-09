using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandController : MonoBehaviour
{
    [Header("Island Settings")]
    [SerializeField] private float _sinkDepth = -5f;
    [SerializeField] private float _riseTime = 2f;
    [SerializeField] private float _sinkTime = 2f;

    [Header("Randomization Settings")]
    [SerializeField] private int _minIslandsToSink = 1;
    [SerializeField] private int _maxIslandsToSink = 3;
    [SerializeField] private float _intervalBetweenSinks = 5f;

    [Header("Surfaces")]
    [SerializeField] private Transform[] _surfaces;

    [Header("Bee Spawner")]
    [SerializeField] private BeeSpawner _beeSpawner;

    private Dictionary<Transform, Vector3> _originalPositions;
    private Dictionary<Transform, Animator> _surfaceAnimators;

    public void Construct()
    {
        InitializeOriginalPositions();
        InitializeSurfaceAnimators();
    }

    public void StartEarthquake()
    {
        StartCoroutine(SinkAndRiseRoutine());
    }

    private void InitializeOriginalPositions()
    {
        _originalPositions = new Dictionary<Transform, Vector3>();
        foreach (Transform surface in _surfaces)
        {
            _originalPositions[surface] = surface.position;
        }
    }

    private void InitializeSurfaceAnimators()
    {
        _surfaceAnimators = new Dictionary<Transform, Animator>();
        foreach (Transform surface in _surfaces)
        {
            Animator animator = surface.GetComponent<Animator>();
            if (animator != null)
            {
                _surfaceAnimators[surface] = animator;
                animator.enabled = false;
            }
        }
    }

    private IEnumerator SinkAndRiseRoutine()
    {
        while (true)
        {
            int surfacesToSinkCount = Random.Range(_minIslandsToSink, _maxIslandsToSink + 1);
            List<Transform> surfacesToSink = new List<Transform>(_surfaces);

            Transform beeSpawnPoint = _beeSpawner.GetCurrentSpawnPoint();
            surfacesToSink.Remove(beeSpawnPoint);

            for (int i = 0; i < surfacesToSinkCount; i++)
            {
                if (surfacesToSink.Count == 0) break;

                int randomIndex = Random.Range(0, surfacesToSink.Count);
                Transform surface = surfacesToSink[randomIndex];
                surfacesToSink.RemoveAt(randomIndex);

                StartCoroutine(SinkSurface(surface));
            }

            yield return new WaitForSeconds(_intervalBetweenSinks);
        }
    }

    private IEnumerator SinkSurface(Transform surface)
    {
        yield return StartCoroutine(PlayShakeAnimation(surface));
        yield return StartCoroutine(MoveSurface(surface, _sinkDepth, _sinkTime));
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        yield return StartCoroutine(MoveSurface(surface, _originalPositions[surface].y, _riseTime));
    }

    private IEnumerator PlayShakeAnimation(Transform surface)
    {
        if (_surfaceAnimators.ContainsKey(surface))
        {
            Animator animator = _surfaceAnimators[surface];
            animator.enabled = true;
            animator.Play("Shake");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            animator.enabled = false;
        }
    }

    private IEnumerator MoveSurface(Transform surface, float targetY, float time)
    {
        Vector3 startPosition = surface.position;
        Vector3 targetPosition = new Vector3(startPosition.x, targetY, startPosition.z);
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            surface.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        surface.position = targetPosition;
    }
}