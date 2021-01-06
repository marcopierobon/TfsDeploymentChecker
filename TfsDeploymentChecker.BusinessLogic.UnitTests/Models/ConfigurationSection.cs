using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace TfsDeploymentChecker.BusinessLogic.UnitTests.Models
{
    public class ConfigurationSection : Microsoft.Extensions.Configuration.IConfigurationSection
    {
        public string this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Key { get; set; }

        public string Path { get; set; }

        public string Value { get; set; }

        public IEnumerable<IConfigurationSection> GetChildren ()
        {
            throw new NotImplementedException();
        }

        public IChangeToken GetReloadToken ()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection (string key)
        {
            throw new NotImplementedException();
        }
    }
}
