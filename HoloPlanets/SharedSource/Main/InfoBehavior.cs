using System;
using System.Runtime.Serialization;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;
using WaveEngine.Hololens;
using WaveEngine.Hololens.Interaction;

namespace HoloPlanets
{
    [DataContract]
    public class InfoBehavior : Behavior
    {
        [RequiredComponent]
        public Transform3D _transform;

        private SpatialState _lastState;

        private HololensService _hololensService;
        private SpatialInputService _spatialInputManager;

        [DataMember]
        public bool AllowInfo { get; set; }

        protected override void DefaultValues()
        {
            base.DefaultValues();

            this.AllowInfo = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _hololensService = WaveServices.GetService<HololensService>();
            _spatialInputManager = WaveServices.GetService<SpatialInputService>();
        }

        protected override void Update(TimeSpan gameTime)
        {
            var gesture = _spatialInputManager.SpatialState;

            if (gesture.IsSelected && !_lastState.IsSelected)
            {
                this.ShowInfo();
            }

            _hololensService.SetStabilizationPlane(_transform.Position);

            _lastState = gesture;
        }

        private void ShowInfo()
        {
            Camera3D camera = this.RenderManager.ActiveCamera3D;

            if (AllowInfo && camera != null)
            {
                // Show Planet Info
             
            }
        }
    }
}