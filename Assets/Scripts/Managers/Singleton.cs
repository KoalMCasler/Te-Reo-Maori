using UnityEngine;

public class Singleton : MonoBehaviour
{
    static Singleton instance;
    public bool replaceExistingInstance;

    private void Awake()
    {
        if(instance != null)
        {
            if(replaceExistingInstance)
            {
                Destroy(instance.gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }
 
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void ClearInstance()
    {
        instance = null;
    }
}
