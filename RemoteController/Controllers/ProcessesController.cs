using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RemoteController.Controllers
{
    [Route("api/[controller]")]
    public class ProcessesController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public string Get()
        {
            List<Models.Process> processes = new List<Models.Process>();
            foreach (Process process in Process.GetProcesses())
            {
                if (!string.IsNullOrEmpty(process.MainWindowTitle))
                {
                    processes.Add(new Models.Process(process.MainWindowTitle, process.ProcessName, process.Id));
                }
            }

            return JsonConvert.SerializeObject(processes);
        }
    }
}
