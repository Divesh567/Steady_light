using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class SimpleInjector
{
    public static void Inject(object target, GameObject context = null)
    {
        if (target == null)
        {
            Debug.LogError("[SimpleInjector] Target is null, cannot inject.");
            return;
        }

        if (context == null && target is Component c)
        {
            context = c.gameObject;
            Debug.Log($"[SimpleInjector] Context not provided, using {context.name}");
        }

        var fields = target.GetType()
            .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
            .Where(f => Attribute.IsDefined(f, typeof(InjectAttribute)));

        Debug.Log($"[SimpleInjector] Found {fields.Count()} injectable fields in {target.GetType().Name}");

        foreach (var field in fields)
        {
            var currentValue = field.GetValue(target);
            Debug.Log($"[SimpleInjector] Checking field {field.Name} ({field.FieldType.Name}), current value = {(currentValue == null ? "null" : currentValue.ToString())}");

            if (currentValue != null)
            {
                Debug.Log($"[SimpleInjector] Skipping {field.Name}, already assigned.");
                continue; // already assigned in Inspector
            }

            object value = null;

            // Try to resolve by type
            if (typeof(Component).IsAssignableFrom(field.FieldType))
            {
                value = context?.GetComponentInChildren(field.FieldType, true);
                Debug.Log($"[SimpleInjector] Tried GetComponentInChildren<{field.FieldType.Name}> on {context?.name}, result = {(value != null ? value.ToString() : "null")}");

                if (value == null)
                {
                    value = UnityEngine.Object.FindAnyObjectByType(field.FieldType);
                    Debug.Log($"[SimpleInjector] Tried FindAnyObjectByType<{field.FieldType.Name}>, result = {(value != null ? value.ToString() : "null")}");
                }
            }
            else if (field.FieldType == typeof(GameObject))
            {
                value = context;
                Debug.Log($"[SimpleInjector] Assigned GameObject context {context?.name} to {field.Name}");
            }
            else if (typeof(ScriptableObject).IsAssignableFrom(field.FieldType))
            {
                value = Resources.FindObjectsOfTypeAll(field.FieldType).FirstOrDefault();
                Debug.Log($"[SimpleInjector] Tried to resolve ScriptableObject {field.FieldType.Name}, result = {(value != null ? value.ToString() : "null")}");
            }

            if (value != null)
            {
                field.SetValue(target, value);
                Debug.Log($"[SimpleInjector] Injected {field.Name} with {value}");
            }
            else
            {
                Debug.LogWarning($"[SimpleInjector] Could not resolve {field.FieldType.Name} for {target.GetType().Name}.{field.Name}");
            }
        }

        Debug.Log($"[SimpleInjector] Injection finished for {target.GetType().Name}");
    }
}
