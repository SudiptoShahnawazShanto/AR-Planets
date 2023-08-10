using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string sceneName;
    private Vector3 previousPosition;
    private Vector3 currentPosition;
    private Vector3 initialLocalScale;
    private Vector3 initialLocalScaleRing;

    private Camera arCamera;
    public float resetDuration = 0.5f;

    private void Awake()
    {
        currentPosition = transform.position;
        initialLocalScale = transform.localScale;

        ParticleSystem ringParticle = GetComponentInChildren<ParticleSystem>();
        initialLocalScaleRing = ringParticle.transform.localScale;

        //Find the AR camera in the scene by tag or name
        arCamera = Camera.main;
    }

    public void SwitchScenes(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ResetTransformation()
    {
        //Store the current position of the planet before moving
        previousPosition = transform.position;

        //Reset planet's position to the current position
        transform.position = currentPosition;

        //Calculate the target position in front of the camera
        float distanceToCamera = Vector3.Distance(transform.position, arCamera.transform.position);
        Vector3 targetPosition = arCamera.transform.position + arCamera.transform.forward * distanceToCamera;

        StartCoroutine(MoveToTargetPosition(targetPosition, resetDuration));
        StartCoroutine(ScaleToInitialSize(initialLocalScale, resetDuration));
    }

    //Use IEnumertor for implementing coroutine (coroutine ensures smooth transition)
    private IEnumerator MoveToTargetPosition(Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = previousPosition;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            //Clamp01 forces the value to be between 0 and 1. 
            float t = Mathf.Clamp01(elapsedTime / duration); //t represents how much the object has moved.

            transform.position = Vector3.Lerp(startingPosition, targetPosition, t);
            yield return null; //Keep frame rate smooth by pausing the coroutine for 1 frame
        }
        transform.position = targetPosition; //Fix the fractional errors
    }

    //Use IEnumertor for implementing coroutine (coroutine ensures smooth transition)
    private IEnumerator ScaleToInitialSize(Vector3 targetScale, float duration)
    {
        ParticleSystem ringParticle = GetComponentInChildren<ParticleSystem>();
        Vector3 startingScaleRing = ringParticle.transform.localScale;

        float elapsedTime = 0f;
        Vector3 startingScale = transform.localScale;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            transform.localScale = Vector3.Lerp(startingScale, targetScale, t);
            ringParticle.transform.localScale = Vector3.Lerp(startingScaleRing, initialLocalScaleRing, t);
            yield return null; //Keep frame rate smooth by pausing the coroutine for 1 frame
        }
        transform.localScale = targetScale; //Fix the fractional errors
    }
}
