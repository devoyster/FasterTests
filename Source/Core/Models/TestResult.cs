using System;

namespace FasterTests.Core.Models
{
    [Serializable]
    public class TestResult
    {
        public bool IsSuccess { get; set; }
        public bool IsIgnored { get; set; }
        public string ErrorMessage { get; set; }
    }
}