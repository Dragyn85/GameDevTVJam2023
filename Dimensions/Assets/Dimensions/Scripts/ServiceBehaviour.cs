using UnityEngine;

public abstract class ServiceBehaviour : MonoBehaviour
{
    bool initialized = false;
    protected abstract void Initialize();
    public void InitializeServiece()
    {
        if (initialized) return;

        initialized = true;
        Initialize();
    }
}
