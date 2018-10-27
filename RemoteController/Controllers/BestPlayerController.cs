using System;
using Microsoft.AspNetCore.Mvc;

namespace RemoteController.Controllers
{
    [Route("api/[controller]")]
    public class BestPlayerController : Controller
    {
        private static IntPtr _handle;
        private static int _volume;
        [HttpGet("reset")]
        public void Reset()
        {
            _handle = SystemFunctions.GetWindowConstains("bestplayer");
            SystemFunctions.SendKey(_handle, SystemFunctions.UpArrow, 25);
            _volume = 100;
        }

        [HttpGet("volume")]
        public int GetVolume()
        {
            if (_handle == IntPtr.Zero)
            {
                Reset();
            }

            return _volume;
        }

        [HttpPost("volume/{value}")]
        public string SetVolume(int value)
        {
            if (_handle == IntPtr.Zero)
            {
                Reset();
            }

            if (value > 100.0 || value < 0.0)
            {
                return "Volume out of range";
            }

            if (_volume == value)
            {
                return "No volume change";
            }

            if (_volume < value)
            {
                int howManuClicksUp = (value - _volume) / 4;
                SystemFunctions.SendKey(_handle, SystemFunctions.UpArrow, howManuClicksUp);
                _volume = _volume + howManuClicksUp * 4;
            }
            else
            {
                int howManyClicksDown = (_volume - value) / 4;
                SystemFunctions.SendKey(_handle, SystemFunctions.DownArrow, howManyClicksDown);
                _volume = _volume - howManyClicksDown * 4;
            }

            return $"Volume set to {_volume}";
        }

        [HttpGet("space")]
        public string TogglePlayPause()
        {
            if (_handle == IntPtr.Zero)
            {
                Reset();
            }

            SystemFunctions.SendKey(_handle, SystemFunctions.Space);
            return "Toggled";
        }

        [HttpGet("top")]
        public string ToTop()
        {
            if (_handle == IntPtr.Zero)
            {
                Reset();
            }

            SystemFunctions.BringToForeground(_handle);
            return "top";
        }
    }
}
