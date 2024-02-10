using System;
using UnityEngine;
using Unity;

[Serializable]
public class SaveData 
{
    public bool[] unlockedWorlds = { true, false, false, false, false, false };
    public bool[] _allLevels = {true, false, false, false, false, false, false,
                                 false, false, false, false, false, false, false, false, false,
                                    false, false, false, false, false, false, false, false, false, false, false,
                                      false, false, false, false, false, false, false ,false, false};

    public bool[] _diamondData = { false, false, false, false, false, false, false, false, false, false,
                                    false, false, false, false, false, false, false, false, false, false,
                                     false, false, false, false, false, false, false, false, false, false,
                                      false, false, false, false, false, false, false, false, false, false,
                                       false, false, false, false, false, false, false, false, false, false,
                                        false, false, false, false, false, false, false, false, false, false,
                                         false, false, false, false, false, false, false, false, false, false,
                                          false, false, false, false, false, false, false, false, false, false,
                                            false, false, false, false, false, false, false, false, false, false,
                                              false, false, false, false, false, false, false, false, false, false,
                                                false, false, false, false, false, false, false, false, false, false,
                                                  false,};

    public int _lifes = 0;
    public float _time = 0;
    public int _powerUp = 0;

    public bool _lifeMaximun;
    public bool _timeMaximun;
    public bool _powerUpMaximum;
    public int _Diamonds = 5;
    public string hashValue = String.Empty;

    
}
