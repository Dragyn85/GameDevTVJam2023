using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameServiceLocator : MonoBehaviour
{
    Dictionary<Type, MonoBehaviour> servicesDictionray = new Dictionary<Type, MonoBehaviour>();
    bool spawned = false;
    [SerializeField] GameObject persitingServiceContainerPrefab = null;

    private void Awake()
    {
        if (!spawned)
        {
            var instance = Instantiate(persitingServiceContainerPrefab);
            DontDestroyOnLoad(instance);
        }
    }

    public T GetService<T>() where T : MonoBehaviour
    {
        if(servicesDictionray.TryGetValue(typeof(T),out MonoBehaviour MonoService))
        {
            return (T)MonoService;
        }

        T service = FindObjectOfType<T>();

        if(service != null)
        {
            RegistrerService<T>(service);
        }
        if(service == null)
        {
            Debug.LogError($"No service of type {typeof(T).FullName} available, please make sure to have all services added");
        }

        return (T)service;
    }

    public void RegistrerService<T>(T service) where T: MonoBehaviour
    {
        if (servicesDictionray.ContainsKey(typeof(T))) return;

        servicesDictionray.Add(typeof(T), service);
    }
}
