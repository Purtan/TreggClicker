using UnityEngine;

public class Cheats : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Game.Treggs += 100;
    }
}
