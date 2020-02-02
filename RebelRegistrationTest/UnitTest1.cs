using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using RebelRegistration.Controllers;
using RebelRegistration.Models;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace RebelRegistrationTest
{
    public class UnitTest1
    {
        [Fact]
        public void TestGet()
        {
            // Logger dependency
            ILogger<RebelInfoController> _logger = new Logger<RebelInfoController>(new NullLoggerFactory());
            // Create Get controller
            var l_controller = new RebelInfoController(_logger);
            // Run request
            var l_result = l_controller.Get();

            // Prepare log entries from request
            List<string> l_logEntriesReq = new List<string>();

            using (IEnumerator<string> enumerator = l_result.Result.Value.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    l_logEntriesReq.Add(enumerator.Current);
                }
            }

            // Prepare log entries read from log file by streamreader
            List<string> l_logEntriesSR = new List<string>();

            using (StreamReader streamReader = new StreamReader("Logs\\RebelLog.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    l_logEntriesSR.Add(streamReader.ReadLine());
                }
            }

            // Assert result
            Assert.Equal(l_logEntriesSR.Count, l_logEntriesReq.Count);
        }

        [Theory]
        [InlineData("Aitor")]
        public void TestGetRebel(string name)
        {
            // Logger dependency
            ILogger<RebelInfoController> _logger = new Logger<RebelInfoController>(new NullLoggerFactory());
            // Create Get controller
            var l_controller = new RebelInfoController(_logger);
            // Run request
            var l_result = l_controller.Get(name);

            // Prepare log entries from request
            List<string> l_logEntriesReq = new List<string>();

            using (IEnumerator<string> enumerator = l_result.Result.Value.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    l_logEntriesReq.Add(enumerator.Current);
                }
            }

            // Assert result
            Assert.True(l_logEntriesReq.Count > 0);
        }

        [Theory]
        [InlineData("Aitor", "Mars")]
        public void TestPostRebel(string name, string planet)
        {
            // Logger dependency
            ILogger<RebelInfoController> _logger = new Logger<RebelInfoController>(new NullLoggerFactory());
            // Create Get controller
            var l_controller = new RebelInfoController(_logger);
            // Run request
            var l_rebelInfo = new RebelInfo();
            l_rebelInfo.Name = name;
            l_rebelInfo.Planet = planet;
            var l_result = l_controller.Post(l_rebelInfo);

            // Assert result
            Assert.NotEqual("An error", l_result.Value.Substring(0, 8));

            /*
             * No funciona del todo bien, no se puede acceder a la propierdad l_result.Result.Value¿?¿?
             */
        }
    }
}
