using System;
using System.Collections.Generic;
using UnityEngine;

public class GameServiceLocator : MonoBehaviour
{
    Dictionary<Type, ServiceBehaviour> servicesDictionray = new Dictionary<Type, ServiceBehaviour>();
    static bool spawned = false;
    [SerializeField] GameObject persitingServiceContainerPrefab = null; 

    public static GameServiceLocator GetInstance()
    {
        return GameObject.FindGameObjectWithTag("ServiceLocator").GetComponent<GameServiceLocator>();
    }
    private void Awake()
    {
        if (!spawned)
        {   
            var instance = Instantiate(persitingServiceContainerPrefab);
            DontDestroyOnLoad(instance);
            spawned = true;
        }
    }

    public T GetService<T>() where T : ServiceBehaviour
    {
        if(servicesDictionray.TryGetValue(typeof(T),out ServiceBehaviour MonoService))
        {
            MonoService.InitializeServiece();
            return (T)MonoService;
        }

        T service = FindObjectOfType<T>();

        if(service != null)
        {
            service.InitializeServiece();
            RegistrerService<T>(service);
        }
        if(service == null)
        {
            Debug.LogError($"No service of type {typeof(T).FullName} available, please make sure to have all services added");
        }

        return (T)service;
    }

    public void RegistrerService<T>(T service) where T: ServiceBehaviour
    {
        if (servicesDictionray.ContainsKey(typeof(T))) return;

        servicesDictionray.Add(typeof(T), service);
    }
}
