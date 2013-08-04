using System;

namespace Funt.Core.Models
{
    [Serializable]
    public class TestResult
    {
        public TestDescriptor Test { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsIgnored { get; set; }
        public string ErrorMessage { get; set; }
    }
}