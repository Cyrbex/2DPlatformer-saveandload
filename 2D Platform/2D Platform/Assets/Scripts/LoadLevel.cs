using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoadLevel : MonoBehaviour
{
    public string LevelToLoad;
    public bool cleared;

    // Start is called before the first frame update
  
    void Start()
    {
        if(GameStatus.status.GetType().GetField(LevelToLoad).
            GetValue(GameStatus.status).ToString() == "True")
        {
            Cleared(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cleared(bool isClear)
    {
        if(isClear == true)
        {
            cleared = true;
            //Casting
            GameStatus.status.GetType().GetField(LevelToLoad).SetValue(GameStatus.status, true);
            transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
