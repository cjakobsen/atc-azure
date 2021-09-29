using Atc.Azure.Environment;
using Atc.Test;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace Clever.DataPlatform.BCIntegration.Functions.Tests.Environment
{
    public class EnvironmentOptionsTest
    {
        [Theory, AutoNSubstituteData]
        public void Should_Initialize_SectionName([Frozen] string sectionName, EnvironmentOptions sut)
            => sut.SectionName.Should().Be(sectionName);

        [Theory, AutoNSubstituteData]
        public void Should_Initialize_CompanyAbbreviation([Frozen] string companyAbbreviation, EnvironmentOptions sut)
            => sut.CompanyAbbreviation.Should().Be(companyAbbreviation);

        [Theory, AutoNSubstituteData]
        public void Should_Initialize_SystemAbbreviation([Frozen] string systemAbbreviation, EnvironmentOptions sut)
            => sut.SystemAbbreviation.Should().Be(systemAbbreviation);

        [Theory, AutoNSubstituteData]
        public void Should_Initialize_ServiceAbbreviation([Frozen] string serviceAbbreviation, EnvironmentOptions sut)
            => sut.ServiceAbbreviation.Should().Be(serviceAbbreviation);
    }
}
