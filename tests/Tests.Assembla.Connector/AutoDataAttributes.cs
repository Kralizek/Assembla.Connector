using System;
using System.IO;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.NUnit3;

namespace Tests
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CustomAutoDataAttribute : AutoDataAttribute
    {
        public CustomAutoDataAttribute() : base (FixtureHelpers.CreateFixture) { }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class CustomInlineAutoDataAttribute : InlineAutoDataAttribute
    {
        public CustomInlineAutoDataAttribute(params object[] arguments) : base (FixtureHelpers.CreateFixture, arguments) { }
    }


    public static class FixtureHelpers
    {
        public static IFixture CreateFixture()
        {
            IFixture fixture = new Fixture();

            fixture.Customize(new AutoMoqCustomization
            {
                ConfigureMembers = true,
                GenerateDelegates = true
            });

            fixture.Register(() => Stream.Null);

            return fixture;
        }
    }
}