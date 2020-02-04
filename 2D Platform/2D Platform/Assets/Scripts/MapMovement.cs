using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMovement : MonoBehaviour
{

    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        // Tarkastetaan onko currenLevel muutujassa jotain. Jos on, ollaan silloin tultu
        // jostain tasosta takaisin karttaan
        if (GameStatus.status.currentLevel != "")
        {
            GameObject.Find(GameStatus.status.currentLevel).GetComponent<LoadLevel>().Cleared(true);

            transform.position = GameObject.Find(GameStatus.status.currentLevel).
                transform.GetChild(0).transform.position;

        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime,
            Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LevelTrigger") && collision.GetComponent<LoadLevel>().cleared == false)
        {
            GameStatus.status.currentLevel = collision.GetComponent<LoadLevel>().LevelToLoad;
            SceneManager.LoadScene(collision.GetComponent<LoadLevel>().LevelToLoad);

            /*
                KOtitehtävä. Tee jokaisen Levelin loppuun partikkeliefekti. Kun hahmo
                osuu efektiin palautuu peli MapSceneen. Pelaaja tulisi ilmestyä levelin
                alla olevaan Spawnpointiin. 
                Tee toimimaan kaikilla Leveleillä 1-3

            */

        }
    }
}
