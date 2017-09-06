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

    float balloonSpawnDistance = 10.2f;
    int narrationIndex = 0;

	void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.tag == "balloon")
            coll.GetComponent<Balloon>().Collided();
	}

    private void Start()
    {
        StartCoroutine(SpawnBalloon());
        StartCoroutine(ChangeNarration());
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
}
