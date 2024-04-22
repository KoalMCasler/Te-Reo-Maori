using UnityEngine;

public class Singleton : MonoBehaviour
{
    static Singleton instance;

    private void Awake()
    {
        if(instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            GameObject.DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
}
