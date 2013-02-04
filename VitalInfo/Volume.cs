using System;
using CoreAudioApi;

namespace VitalInfo
{
    class Volume : IDisposable
    {
        private MMDeviceEnumerator _devEnum;
        private MMDevice _defaultDevice;
        public Volume()
        {
            _devEnum = new MMDeviceEnumerator();
            _defaultDevice = _devEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia);
        }

        public int GetVol()
        {
            return (int) Math.Round(100 * _defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar);
        }

        public bool GetMute()
        {
            return _defaultDevice.AudioEndpointVolume.Mute;
        }

        public void Dispose()
        {
            
        }
    }
}
