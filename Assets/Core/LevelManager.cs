using System;
using Core.Character;
using UnityEngine;

namespace Core
{
    public class LevelManager : MonoBehaviour
    {
        public Transform cameraBorders;

        private void Start()
        {
            CameraController.Instance.SetBorders(cameraBorders);
        }
    }
}