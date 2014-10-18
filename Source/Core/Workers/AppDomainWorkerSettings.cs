using System;
using FasterTests.Core.Interfaces;

namespace FasterTests.Core.Workers
{
    [Serializable]
    public class AppDomainWorkerSettings
    {
        public AppDomainWorkerSettings()
        {
        }

        public AppDomainWorkerSettings(TestRunSettings other)
        {
            IncludeCategories = other.IncludeCategories;
            ExcludeCategories = other.ExcludeCategories;
        }

        public string[] IncludeCategories { get; set; }
        public string[] ExcludeCategories { get; set; }
    }
}