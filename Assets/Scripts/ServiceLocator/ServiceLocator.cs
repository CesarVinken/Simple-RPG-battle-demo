using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator
{
    private ServiceLocator() { }

    private readonly Dictionary<string, IGameService> _services = new Dictionary<string, IGameService>();

    public static ServiceLocator Instance { get; private set; }

    public static void Setup()
    {
        Instance = new ServiceLocator();
    }

    public T Get<T>() where T : IGameService
    {
        string key = typeof(T).Name;
        if (!_services.ContainsKey(key))
        {
            ConsoleLog.Error(LogCategory.General, $"{key} not registered with {GetType().Name}");
            throw new InvalidOperationException();
        }

        return (T)_services[key];
    }

    public void Register<T>(T service) where T : IGameService
    {
        string key = typeof(T).Name;
        if (_services.ContainsKey(key))
        {
            ConsoleLog.Error(LogCategory.General, $"Attempted to register service of type {key} which is already registered with the {GetType().Name}.");
            return;
        }

        _services.Add(key, service);
    }

    public void Deregister<T>() where T : IGameService
    {
        string key = typeof(T).Name;
        if (!_services.ContainsKey(key))
        {
            ConsoleLog.Error(LogCategory.General, $"Attempted to unregister service of type {key} which is not registered with the {GetType().Name}.");
            return;
        }

        _services.Remove(key);
    }
}
