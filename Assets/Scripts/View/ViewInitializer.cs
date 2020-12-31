using System;
using UnityEngine;

#nullable enable

namespace View
{
    public class ViewInitializer : MonoBehaviour
    {
        [SerializeField] private CameraView? cameraView;

        public void Initialize()
        {
            if (cameraView == null) throw new NullReferenceException();
            cameraView.Initialize();
        }
    }
}