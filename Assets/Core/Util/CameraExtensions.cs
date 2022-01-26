using UnityEngine;

namespace Core.Util
{
    public static class CameraExtensions
    {
        public static Vector3 GetWorldPositionOnPlane(this Camera camera, Vector3 screenPosition, float z)
        {
            Ray ray = camera.ScreenPointToRay(screenPosition);
            Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
            xy.Raycast(ray, out var distance);

            return ray.GetPoint(distance);
        }
    }
}