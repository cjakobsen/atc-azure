namespace Atc.Azure.Environment
{
    public class EnvironmentOptions
    {
        public string SectionName { get; private set; }

        public string CompanyAbbreviation { get; private set; }

        public string SystemAbbreviation { get; private set; }

        public string ServiceAbbreviation { get; private set; }

        public EnvironmentType EnvironmentType { get; set; }

        public string EnvironmentName { get; set; } = string.Empty;

        public EnvironmentOptions(string sectionName, string companyAbbreviation, string systemAbbreviation, string serviceAbbreviation)
        {
            SectionName = sectionName;
            CompanyAbbreviation = companyAbbreviation;
            SystemAbbreviation = systemAbbreviation;
            ServiceAbbreviation = serviceAbbreviation;
        }
    }
}