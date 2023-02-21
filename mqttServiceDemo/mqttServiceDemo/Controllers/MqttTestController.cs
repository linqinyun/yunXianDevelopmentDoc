using Microsoft.AspNetCore.Mvc;
using mqttServiceDemo.Models;
using mqttServiceDemo.service;
using Newtonsoft.Json;
using System.Security.AccessControl;
using System.Text;

namespace mqttServiceDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MqttTestController : Controller
    {
        // {"Da":1,"En":"gating","Oa":1,"Eo":"status","Nv":"0"}
        [HttpPost]
        public IActionResult Test(Uplink uplink)
        {

            string data = JsonConvert.SerializeObject(uplink);
            MqttService.PublishData(data);
            return Ok();
        }
    }
}
