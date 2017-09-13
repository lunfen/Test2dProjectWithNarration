using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    [SerializeField] GameObject BalloonPrefab;
    [SerializeField] float BalloonSpawnTime;
    [SerializeField] float NarrationChangeTime;
    [SerializeField] Text NarrationText;
    [SerializeField] string[] NarrationArray = new string[5];
    [SerializeField] Animation ParralaxAnimation;

    float balloonSpawnDistance = 10.2f;
    int narrationIndex = 0;

    Vector2 screenLimit;
    float speedMultiplier = 0.25f;
    BoxCollider2D collider2d;

    float backgroundSpeed = 1;

    private void Awake()
    {
        BasicSetup();
    }

    private void Start()
    {
        StartCoroutine(SpawnBalloon());
        StartCoroutine(ChangeNarration());
    }

    private void Update()
    {
        ApplyControls();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "balloon")
            coll.GetComponent<Balloon>().Collided();
    }

    IEnumerator SpawnBalloon()
    {
        yield return new WaitForSeconds(BalloonSpawnTime);
        Instantiate(BalloonPrefab, Vector3.right * balloonSpawnDistance, Quaternion.identity);
        yield return SpawnBalloon();
    }

    IEnumerator ChangeNarration()
    {
        if (narrationIndex >= NarrationArray.Length) narrationIndex = 0;
        NarrationText.text = NarrationArray[narrationIndex];
        yield return new WaitForSeconds(NarrationChangeTime);
        narrationIndex++;
        yield return ChangeNarration();
    }

    void BasicSetup()
    {
        collider2d = GetComponent<BoxCollider2D>();

        Vector2 birdColliderSize = 0.5f * collider2d.size * transform.localScale.x;

        screenLimit = new Vector2( Camera.main.ViewportToWorldPoint(Vector3.right).x - birdColliderSize.x, Camera.main.ViewportToWorldPoint(Vector3.up).y - birdColliderSize.y );
    }

    float x_axis_input;
    float y_axis_input;

    void ApplyControls()
    {
        x_axis_input = Input.GetAxis("Horizontal");
        y_axis_input = Input.GetAxis("Vertical");

        Debug.Log("Current move speed. hor: " + x_axis_input.ToString() + " ver: " + y_axis_input.ToString());

        // Apply Wind
        if (x_axis_input == 0)
            x_axis_input = -0.5f;

        // Apply Screen Limits
        if (x_axis_input > 0 && transform.position.x > screenLimit.x)
        {
            x_axis_input = 0;
            ParralaxAnimation["BackgroundScroll"].speed = 2;
        }
        else
        {
            ParralaxAnimation["BackgroundScroll"].speed = 1;
        }

        if (x_axis_input < 0 && transform.position.x < -screenLimit.x)
            x_axis_input = 0;

        if (y_axis_input > 0 && transform.position.y > screenLimit.y)
            y_axis_input = 0;

        if (y_axis_input < 0 && transform.position.y < -screenLimit.y)
            y_axis_input = 0;

        // Apply Movement
        transform.Translate(new Vector2(x_axis_input, y_axis_input) * speedMultiplier);
    }
}
