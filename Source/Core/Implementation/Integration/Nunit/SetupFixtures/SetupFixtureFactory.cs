﻿using System;
using FasterTests.Core.Interfaces.Integration.Nunit;

namespace FasterTests.Core.Implementation.Integration.Nunit.SetupFixtures
{
    public class SetupFixtureFactory : ISetupFixtureFactory
    {
        public ISetupFixture Create(Type type)
        {
            return new SetupFixture(type);
        }
    }
}