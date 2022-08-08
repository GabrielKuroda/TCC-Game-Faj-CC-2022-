using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoom : IPersistentSingleton<CameraRoom>
{
 
   public void SetPosition(Vector2 position)
    {
        transform.position = new Vector3(position.x, position.y, -10);
    }
}
