using System.Collections;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] float BalloonSpeed;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    Collider2D collider2d;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();
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
        spriteRenderer.enabled = false;
        collider2d.enabled = false;
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
}
