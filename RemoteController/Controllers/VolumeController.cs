using System;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RemoteController.Controllers
{
    [Route("api/[controller]")]
    public class VolumeController : Controller
    {
        [HttpGet]
        public double GetVolume()
        {
            return AudioManager.GetMasterVolume();
        }

        //[HttpGet("{pid}")]
        //public double GetProgramVolume(int pid)
        //{
        //    return AudioManager.GetApplicationVolume(pid) ?? -1;
        //}

        // POST api/<controller>
        [HttpPost("{percent}")]
        public string SetVolume(double percent)
        {
            if (percent > 100.0 || percent < 0.0)
            {
                return "Volume out of range";
            }

            AudioManager.SetMasterVolume((float) percent);

            return $"Volume set to {percent}";
        }

        //// POST api/<controller>
        //[HttpPost(@"{pid}\{percent}")]
        //public string SetProgramVolume(int pid, double percent)
        //{
        //    if (percent > 100.0 || percent < 0.0)
        //    {
        //        return "Volume out of range";
        //    }

        //    AudioManager.SetApplicationVolume(pid, (float)percent);

        //    return $"Volume set to {percent}";
        //}
    }
}
