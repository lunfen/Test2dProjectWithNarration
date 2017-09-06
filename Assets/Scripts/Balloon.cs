using System.Collections;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] float BalloonSpeed;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();        
    }

    private void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * BalloonSpeed);
    }

    public void Collided()
    {
        StartCoroutine(CollisionEvent());
    }

    IEnumerator CollisionEvent()
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}
