using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DayZ_CommandAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private static Dictionary<string, object> _dataStore = new Dictionary<string, object>();
        private static string? discordData;

        [HttpPost("postdata")]
        public IActionResult PostData([FromBody] Dictionary<string, object> data)
        {
            if (data.ContainsKey("commandName") && data["commandName"].ToString() == "reset")
            {
                _dataStore.Clear();
                return Ok("Data reset");
            }

            _dataStore = data;
            return Ok("Data received");
        }

        [HttpGet("getdata")]
        public IActionResult GetData()
        {
            return Ok(_dataStore);
        }

        [HttpPost("discordpostdata")]
        public async Task<IActionResult> DiscordPostData()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                string data = await reader.ReadToEndAsync();
                discordData = data;
                return Ok();
            }
        }

        [HttpGet("discordgetdata")]
        public IActionResult DiscordGetData()
        {
            return Ok(discordData);
        }

    }
}
