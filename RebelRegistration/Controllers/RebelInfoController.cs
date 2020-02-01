using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RebelRegistration.Models;

namespace RebelRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RebelInfoController : ControllerBase
    {
        private StreamWriter _streamWriter;                                     // StreamWriter object to write to rebel log
        private StreamReader _streamReader;                                     // StreamWriter object to write to rebel log
        private readonly string _logPath = "Logs\\RebelLog.txt";                // Rebel log path
        private readonly string _dateFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";  // Date format for Rebel log

        // GET: api/RebelInfo
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    try
        //    {
        //        _streamReader = new StreamReader(_logPath);
        //    }
        //    catch (DirectoryNotFoundException ex)
        //    {
        //        return BadRequest($"An error ocurred retrieving rebel log entries.\nError: {ex.Message}.\nTry again later.");
        //    }
        //    catch (IOException ex)
        //    {
        //        return BadRequest($"An error ocurred retrieving rebel log entries.\nError: {ex.Message}.\nTry again later.");
        //    }

        //    // TODO - Read all entries Async

        //    List<string> l_logEntries = new List<string>();

        //    while (!_streamReader.EndOfStream)
        //    {
        //        l_logEntries.Add(_streamReader.ReadLine());
        //    }

        //    _streamReader.Dispose();

        //    return l_logEntries.ToArray();
        //}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            try
            {
                _streamReader = new StreamReader(_logPath);
            }
            catch (DirectoryNotFoundException ex)
            {
                return BadRequest($"An error ocurred retrieving rebel log entries.\nError: {ex.Message}.\nTry again later.");
            }
            catch (IOException ex)
            {
                return BadRequest($"An error ocurred retrieving rebel log entries.\nError: {ex.Message}.\nTry again later.");
            }

            return await Task.Run(() =>
            {
                List<string> l_logEntries = new List<string>();

                while (!_streamReader.EndOfStream)
                {
                    l_logEntries.Add(_streamReader.ReadLine());
                }

                _streamReader.Dispose();

                return l_logEntries.ToArray();
            }
            );
        }


        // GET: api/RebelInfo/Aitor
        //[HttpGet("{name}", Name = "Get")]
        //public ActionResult<IEnumerable<string>> Get(string name)
        //{
        //    try
        //    {
        //        _streamReader = new StreamReader(_logPath);
        //    }
        //    catch (DirectoryNotFoundException ex)
        //    {
        //        return BadRequest($"An error ocurred retrieving rebel log entries.\nError: {ex.Message}.\nTry again later.");
        //    }
        //    catch (IOException ex)
        //    {
        //        return BadRequest($"An error ocurred retrieving rebel log entries.\nError: {ex.Message}.\nTry again later.");
        //    }

        //    List<string> l_logEntries = new List<string>();

        //    while (!_streamReader.EndOfStream)
        //    {
        //        string l_logEntry = _streamReader.ReadLine();
        //        int l_pos = l_logEntry.IndexOf(name);

        //        if (l_pos != -1)
        //            l_logEntries.Add(l_logEntry);
        //    }

        //    _streamReader.Dispose();

        //    if (l_logEntries.Count > 0)
        //    {
        //        return l_logEntries.ToArray();
        //    }
        //    else
        //    {
        //        return new string[] { "Rebel not found in log. Keep searching for the enemies of the Republic!" };
        //    }
        //}
        [HttpGet("{name}", Name = "Get")]
        public async Task<ActionResult<IEnumerable<string>>> Get(string name)
        {
            try
            {
                _streamReader = new StreamReader(_logPath);
            }
            catch (DirectoryNotFoundException ex)
            {
                return BadRequest($"An error ocurred retrieving rebel log entries.\nError: {ex.Message}.\nTry again later.");
            }
            catch (IOException ex)
            {
                return BadRequest($"An error ocurred retrieving rebel log entries.\nError: {ex.Message}.\nTry again later.");
            }

            return await Task.Run(() =>
            {
                List<string> l_logEntries = new List<string>();

                while (!_streamReader.EndOfStream)
                {
                    string l_logEntry = _streamReader.ReadLine();
                    int l_pos = l_logEntry.IndexOf(name);

                    if (l_pos != -1)
                        l_logEntries.Add(l_logEntry);
                }

                _streamReader.Dispose();

                if (l_logEntries.Count > 0)
                {
                    return l_logEntries.ToArray();
                }
                else
                {
                    return new string[] { "Rebel not found in log. Keep searching for the enemies of the Republic!" };
                }
            }
            );
        }

        // POST: api/RebelInfo
        [HttpPost]
        //public void Post([FromBody] string value)
        public ActionResult<string> Post([FromBody] RebelInfo rebelInfo)
        {
            try
            {
                _streamWriter = new StreamWriter(_logPath, true);
            }
            catch(DirectoryNotFoundException ex)
            {
                return BadRequest($"An error ocurred during the rebel registration.\nError: {ex.Message}.\nTry again later, don't let the rebel get away!");
            }
            catch(IOException ex)
            {
                return BadRequest($"An error ocurred during the rebel registration.\nError: {ex.Message}.\nTry again later, don't let the rebel get away!");
            }

            string l_dateTime = DateTime.Now.ToString(_dateFormat);

            // Write to Rebel log
            _streamWriter.WriteLine($"Rebel {rebelInfo.Name} on {rebelInfo.Planet} at {l_dateTime}");
            _streamWriter.Flush();
            _streamWriter.Dispose();

            return CreatedAtAction("Get", $"Rebel {rebelInfo.Name} registered on planet {rebelInfo.Planet} at {l_dateTime}");
        }

        // PUT: api/RebelInfo/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
