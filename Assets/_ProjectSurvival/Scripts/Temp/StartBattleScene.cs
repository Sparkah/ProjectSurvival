using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBattleScene : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/HordeScene");
    }
}
