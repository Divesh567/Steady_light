using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneInitializer
{
    public class OnInitStart : UnityEvent {}

    public OnInitStart initStart = new OnInitStart();

    public class OnInitEnd : UnityEvent {}

    public OnInitEnd initEnd = new OnInitEnd();
}
