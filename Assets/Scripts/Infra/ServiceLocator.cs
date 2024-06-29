using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infra
{
    public static class ServiceLocator
    {
        #region --- Variables ---
        
        private static readonly Dictionary<Type, IService> Services = new Dictionary<Type, IService>();

        #endregion
        
        
        #region --- Public Methods ---
        
        public static void RegisterService<T>(T service) where T : class, IService
        {
            var type = typeof(T);
            
            if (service == null)
            {
                Debug.LogError($"Cannot register a null service of type {type.Name}.");
                return;
            }

            if (Services.TryAdd(type, service)) return;
            
            Debug.LogWarning($"Service {type.Name} is already registered.");
        }

        public static void UnregisterService<T>() where T : class, IService
        {
            var type = typeof(T);
            
            if (!Services.ContainsKey(type))
            {
                Debug.LogWarning($"Service {type.Name} is not registered.");
                return;
            }
            
            Services.Remove(type);
        }

        public static T GetService<T>() where T : class, IService
        {
            var type = typeof(T);
            
            if (Services.TryGetValue(type, out var service)) return (T)service;
            
            Debug.LogError($"Service {type.Name} is not registered.");
            return null;
        }
        
        #endregion
    }
}
