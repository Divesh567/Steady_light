using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testing
{
    public class MoveToNextCheckPoint
    {
        public int currentCheckPoint;
        public int maxPoints;

        public int MoveToCheckPoint(int moveBy)
        {
            currentCheckPoint += moveBy;

            if (currentCheckPoint < 0) { currentCheckPoint = 0; return 0; }

            if (currentCheckPoint > maxPoints) { currentCheckPoint = maxPoints; }

            return currentCheckPoint;
           
        }
    }
}

