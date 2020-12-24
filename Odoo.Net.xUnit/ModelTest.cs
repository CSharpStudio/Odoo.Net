using System;
using System.Collections.Generic;
using Xunit;

namespace Odoo.Net.xUnit
{
    public class ModelTest
    {
        protected IServiceProvider ServiceProvider { get; }
        public ModelTest(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        [Fact]
        public void BrowseTest()
        {
            var env = Core.Environment.GetOrCreate(ServiceProvider);
            var bank = env["res.bank"];
            Assert.NotNull(bank);
            var browse = bank.Browse();
            Assert.NotNull(browse);
            Assert.Equal(browse.Env, env);
        }

        [Fact]
        public void WebSearchReadTest()
        {
            var env = Core.Environment.GetOrCreate(ServiceProvider);
            var bank = env["res.bank"];
            Assert.NotNull(bank);
            var records = bank.Call("web_search_read", "", new string[] { "Id", "Name" }, 0, 80, "Name");
            Assert.NotNull(records);
        }

        [Fact]
        public void CreateTest()
        {
            var env = Core.Environment.GetOrCreate(ServiceProvider);
            var bank = env["res.bank"];
            Assert.NotNull(bank);
            bank.Call("create", new List<Map> { new Map { { "Name", "Bank Name" }, { "Street", "Bank Street" } } });
            bank.Call("create", new Map { { "Name", "Bank Name" }, { "Street", "Bank Street" } });
        }
    }
}
