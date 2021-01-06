using System;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TfsDeploymentChecker.BusinessLogic.Implementations;
using TfsDeploymentChecker.BusinessLogic.UnitTests.Models;
using TfsDeploymentChecker.Models.AppSetting;
using TfsDeploymentChecker.Models.ErrorCodes;
using TfsDeploymentChecker.Models.Exceptions;

namespace TfsDeploymentChecker.BusinessLogic.UnitTests.ConfigurationRetrieverTests
{
    [TestClass]
    public class ConstructorTests
    {
        private static ConfigurationRetriever _configurationRetriever;
        
        private static Mock<IConfiguration> _configuration;
        
        private Exception ExpectedException;
        
        private string _validSystemsOfInterest = "SystemA, SystemB";
        
        private string _missingSystemsOfInterest = null;
        
        private string _emptySystemsOfInterest = string.Empty;
        
        private string _validTeamProjectsAndReleaseIdsPairs = "[{\"TfsTeamProjectName\":\"TeamProject1\",\"ProjectReleaseDefinitionIds\":[50,51,52]},{\"TfsTeamProjectName\":\"TeamProject2\",\"ProjectReleaseDefinitionIds\":[1,2,3]}]";
        
        private string _invalidTeamProjectsAndReleaseIdsPairs = "[{\"InvalidParam\":\"TeamProject1\",\"ProjectReleaseDefinitionIds\":[50,51,52]},{\"InvalidParam\":\"TeamProject2\",\"ProjectReleaseDefinitionIds\":[1,2,3]}]";
        
        private string _notWellFormedTeamProjectsAndReleaseIdsPairs = "[{\"TfsTeamProjectName\": ,\"ProjectReleaseDefinitionIds\":[50,51,52]},{\"TfsTeamProjectName\":\"TeamProject2\",\"ProjectReleaseDefinitionIds\":}]";
        
        private string _validApiVersion = "1221";

        private string _validTfsUrl = "http://myTfs";

        private int _validHealthCheckIntervalInSeconds = 1346;

        private string _validTfsToken = "1226edbddbdu";

        private bool _validAllowUntrustedSslCertificates = false;
        
        [TestInitialize]
        public void TestInitialize(){ 
            _configuration = new Mock<IConfiguration>();
        }

        [TestMethod]
        public void When_AllParametersAreOk_Then_NoErrorIsThrown ()
        {
            // Arrange
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.SYSTEMS_OF_INTEREST)))
                .Returns(new ConfigurationSection() { Value =  _validSystemsOfInterest });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.TEAM_PROJECTS_AND_RELEASE_IDS_PAIRS)))
                .Returns(new ConfigurationSection() { Value =  _validTeamProjectsAndReleaseIdsPairs });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.TFS_API_VERSION)))
                .Returns(new ConfigurationSection() { Value =  _validApiVersion });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.TFS_URL)))
                .Returns(new ConfigurationSection() { Value =  _validTfsUrl });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.HEALTHCHECK_INTERVAL_IN_SECONDS)))
                .Returns(new ConfigurationSection() { Value =  _validHealthCheckIntervalInSeconds.ToString() });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.TFS_TOKEN)))
                .Returns(new ConfigurationSection() { Value =  _validTfsToken });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.ALLOW_UNTRUSTED_SSL_CERTIFICATES)))
                .Returns(new ConfigurationSection() { Value =  _validAllowUntrustedSslCertificates.ToString() });

            //Act
            try
            {
                _configurationRetriever = new ConfigurationRetriever(_configuration.Object);
            }
            catch(Exception ex)
            {
                ExpectedException = ex;
            }

            //Assert
            Assert.IsNull(ExpectedException, $"No exception was expected, but one was thrown");
        }

        [TestMethod]
        public void When_OneParametersIsMissing_Then_AnErrorIsThrown ()
        {
            // Arrange
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.SYSTEMS_OF_INTEREST)))
                .Returns(new ConfigurationSection() { Value =  _missingSystemsOfInterest });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.TEAM_PROJECTS_AND_RELEASE_IDS_PAIRS)))
                .Returns(new ConfigurationSection() { Value =  _validTeamProjectsAndReleaseIdsPairs });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.TFS_API_VERSION)))
                .Returns(new ConfigurationSection() { Value =  _validApiVersion });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.TFS_URL)))
                .Returns(new ConfigurationSection() { Value =  _validTfsUrl });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.HEALTHCHECK_INTERVAL_IN_SECONDS)))
                .Returns(new ConfigurationSection() { Value =  _validHealthCheckIntervalInSeconds.ToString() });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.TFS_TOKEN)))
                .Returns(new ConfigurationSection() { Value =  _validTfsToken });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.ALLOW_UNTRUSTED_SSL_CERTIFICATES)))
                .Returns(new ConfigurationSection() { Value =  _validAllowUntrustedSslCertificates.ToString() });

            //Act
            try
            {
                _configurationRetriever = new ConfigurationRetriever(_configuration.Object);
            }
            catch(Exception ex)
            {
                ExpectedException = ex;
            }

            //Assert
            Assert.IsNotNull(ExpectedException, $"An exception was expected, but none was thrown");
            Assert.IsTrue(ExpectedException is InvalidConfigurationException, $"The thrown exception was not of the expected type");
            Assert.AreEqual(ConfigurationErrorCodes.MISSING_SYSTEMS_OF_INTEREST, (ExpectedException as InvalidConfigurationException).ErrorCode,
                $"The exception error code was not the expected one");
        }

        [TestMethod]
        public void When_OneParametersIsEmpty_Then_AnErrorIsThrown ()
        {
            // Arrange
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.SYSTEMS_OF_INTEREST)))
                .Returns(new ConfigurationSection() { Value =  _emptySystemsOfInterest });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.TEAM_PROJECTS_AND_RELEASE_IDS_PAIRS)))
                .Returns(new ConfigurationSection() { Value =  _validTeamProjectsAndReleaseIdsPairs });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.TFS_API_VERSION)))
                .Returns(new ConfigurationSection() { Value =  _validApiVersion });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.TFS_URL)))
                .Returns(new ConfigurationSection() { Value =  _validTfsUrl });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.HEALTHCHECK_INTERVAL_IN_SECONDS)))
                .Returns(new ConfigurationSection() { Value =  _validHealthCheckIntervalInSeconds.ToString() });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.TFS_TOKEN)))
                .Returns(new ConfigurationSection() { Value =  _validTfsToken });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.ALLOW_UNTRUSTED_SSL_CERTIFICATES)))
                .Returns(new ConfigurationSection() { Value =  _validAllowUntrustedSslCertificates.ToString() });

            //Act
            try
            {
                _configurationRetriever = new ConfigurationRetriever(_configuration.Object);
            }
            catch(Exception ex)
            {
                ExpectedException = ex;
            }

            //Assert
            Assert.IsNotNull(ExpectedException, $"An exception was expected, but none was thrown");
            Assert.IsTrue(ExpectedException is InvalidConfigurationException, $"The thrown exception was not of the expected type");
            Assert.AreEqual(ConfigurationErrorCodes.MISSING_SYSTEMS_OF_INTEREST, (ExpectedException as InvalidConfigurationException).ErrorCode,
                $"The exception error code was not the expected one");
        }

        [TestMethod]
        public void When_OneJsonParametersIsUsingWrongFieldName_Then_AnErrorIsThrown ()
        {
            // Arrange
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.SYSTEMS_OF_INTEREST)))
                .Returns(new ConfigurationSection() { Value =  _validSystemsOfInterest });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.TEAM_PROJECTS_AND_RELEASE_IDS_PAIRS)))
                .Returns(new ConfigurationSection() { Value =  _invalidTeamProjectsAndReleaseIdsPairs });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.TFS_API_VERSION)))
                .Returns(new ConfigurationSection() { Value =  _validApiVersion });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.TFS_URL)))
                .Returns(new ConfigurationSection() { Value =  _validTfsUrl });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.HEALTHCHECK_INTERVAL_IN_SECONDS)))
                .Returns(new ConfigurationSection() { Value =  _validHealthCheckIntervalInSeconds.ToString() });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.TFS_TOKEN)))
                .Returns(new ConfigurationSection() { Value =  _validTfsToken });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.ALLOW_UNTRUSTED_SSL_CERTIFICATES)))
                .Returns(new ConfigurationSection() { Value =  _validAllowUntrustedSslCertificates.ToString() });

            //Act
            try
            {
                _configurationRetriever = new ConfigurationRetriever(_configuration.Object);
            }
            catch(Exception ex)
            {
                ExpectedException = ex;
            }

            //Assert
            Assert.IsNotNull(ExpectedException, $"An exception was expected, but none was thrown");
            Assert.IsTrue(ExpectedException is InvalidConfigurationException, $"The thrown exception was not of the expected type");
            Assert.AreEqual(ConfigurationErrorCodes.INVALID_JSON_MODEL, (ExpectedException as InvalidConfigurationException).ErrorCode,
                $"The exception error code was not the expected one");
        }

        [TestMethod]
        public void When_OneJsonParametersIsNotWellFormed_Then_AnErrorIsThrown ()
        {
            // Arrange
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.SYSTEMS_OF_INTEREST)))
                .Returns(new ConfigurationSection() { Value =  _validSystemsOfInterest });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.TEAM_PROJECTS_AND_RELEASE_IDS_PAIRS)))
                .Returns(new ConfigurationSection() { Value =  _notWellFormedTeamProjectsAndReleaseIdsPairs });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.TFS_API_VERSION)))
                .Returns(new ConfigurationSection() { Value =  _validApiVersion });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.TFS_URL)))
                .Returns(new ConfigurationSection() { Value =  _validTfsUrl });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.HEALTHCHECK_INTERVAL_IN_SECONDS)))
                .Returns(new ConfigurationSection() { Value =  _validHealthCheckIntervalInSeconds.ToString() });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.TFS_TOKEN)))
                .Returns(new ConfigurationSection() { Value =  _validTfsToken });
            _configuration.Setup(configuration => configuration.GetSection(
                    It.Is<string>(value => value == AppSettingEntries.ALLOW_UNTRUSTED_SSL_CERTIFICATES)))
                .Returns(new ConfigurationSection() { Value =  _validAllowUntrustedSslCertificates.ToString() });

            //Act
            try
            {
                _configurationRetriever = new ConfigurationRetriever(_configuration.Object);
            }
            catch(Exception ex)
            {
                ExpectedException = ex;
            }

            //Assert
            Assert.IsNotNull(ExpectedException, $"An exception was expected, but none was thrown");
            Assert.IsTrue(ExpectedException is InvalidConfigurationException, $"The thrown exception was not of the expected type");
            Assert.AreEqual(ConfigurationErrorCodes.INVALID_JSON_MODEL, (ExpectedException as InvalidConfigurationException).ErrorCode,
                $"The exception error code was not the expected one");
            Assert.IsNotNull(ExpectedException.InnerException, $"An inner exception was expected, but none was present");
            Assert.IsTrue(ExpectedException.InnerException is JsonException, $"The inner exception was not of the expected type");
        }
    }
}
