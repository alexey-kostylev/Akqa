using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Akqa.Logic.Implementation;
using FluentAssertions;
using System.Threading.Tasks;

namespace Akqa.Tests
{
    [TestClass]
    public class NumberConverterTests : TestBase
    {
        private readonly NumberConverter _converter = new NumberConverter();

        [TestMethod]
        public async Task ShouldConvertIntNumber()
        {
            var actual = await _converter.Convert(123);

            actual.Should().Be("ONE HUNDRED TWENTY THREE");
        }        

        [TestMethod]
        public async Task ShouldConvertMillinos()
        {
            var n = (decimal)Math.Pow(10, 6);
            var actual = await _converter.Convert(n);

            actual.Should().Be("ONE MILLION");
        }

        [TestMethod]
        public async Task ShouldConvertBillions()
        {
            var n = (decimal)Math.Pow(10, 9);
            var actual = await _converter.Convert(n);

            actual.Should().Be("ONE BILLION");
        }

        [TestMethod]
        public async Task ShouldConvertIntMaxValue()
        {
            var n = (decimal)int.MaxValue;
            var actual = await _converter.Convert(n);

            actual.Should().StartWith("TWO BILLION ONE HUNDRED FOURTY SEVEN MILLION FOUR HUNDRED EIGHTY THREE THOUSAND SIX HUNDRED FOURTY SEVEN");
        }

        [TestMethod]
        public void ShouldFailOnConvertingAboveIntMaxValue()
        {
            var n = (decimal)int.MaxValue + 1;
            Action actual = () => _converter.Convert(n);

            actual.ShouldThrow<InvalidOperationException>();
        }

        [TestMethod]
        public async Task ShouldConvertDecimalNumber()
        {
            var actual = await _converter.Convert(123.23m);

            actual.Should().Be("ONE HUNDRED TWENTY THREE AND TWENTY THREE");
        }

        [TestMethod]
        public async Task ShouldConvertHundredWithZero()
        {
            var actual = await _converter.Convert(101);

            actual.Should().Be("ONE HUNDRED ONE");
        }

        [TestMethod]
        public async Task ShouldConvertTHousandWithZero()
        {
            var actual = await _converter.Convert(1022);

            actual.Should().Be("ONE THOUSAND TWENTY TWO");
        }


        [TestMethod]
        public async Task ShouldTruncateToTwoFracDigits()
        {
            var actual = await _converter.Convert(123.23999m);

            actual.Should().Be("ONE HUNDRED TWENTY THREE AND TWENTY THREE");
        }

        [TestMethod]
        public async Task ShouldCareForNegative()
        {
            var actual = await _converter.Convert(-9m);
            actual.Should().Be("MINUS NINE");
        }

        [TestMethod]
        public async Task ShouldCareForZero()
        {
            var actual = await _converter.Convert(0);
            actual.Should().Be("ZERO");
        }

        [TestMethod]
        public async Task ShouldConvert4568()
        {
            var actual = await _converter.Convert(4568);
            actual.Should().Be("FOUR THOUSAND FIVE HUNDRED SIXTY EIGHT");
        }

        [TestMethod]
        public async Task SHouldConvertTens()
        {
            var expected = new Tuple<int, string>[]
            {
                new Tuple<int, string>(10, "TEN"),
                new Tuple<int, string>(11, "ELEVEN"),
                new Tuple<int, string>(12, "TWELVE"),
                new Tuple<int, string>(13, "THIRTEEN"),
                new Tuple<int, string>(14, "FOURTEEN"),
                new Tuple<int, string>(15, "FIFTEEN"),
                new Tuple<int, string>(16, "SIXTEEN"),
                new Tuple<int, string>(17, "SEVENTEEN"),
                new Tuple<int, string>(18, "EIGHTEEN"),
                new Tuple<int, string>(19, "NINETEEN"),
                new Tuple<int, string>(20, "TWENTY"),
                new Tuple<int, string>(30, "THIRTY"),
                new Tuple<int, string>(40, "FOURTY"),
                new Tuple<int, string>(50, "FIFTY"),
                new Tuple<int, string>(60, "SIXTY"),
                new Tuple<int, string>(70, "SEVENTY"),
                new Tuple<int, string>(80, "EIGHTY"),
                new Tuple<int, string>(90, "NINETY"),
            };

            foreach(var test in expected)
            {
                var actual = await _converter.Convert(test.Item1);

                actual.Should().Be(test.Item2);
            }
        }
    }
}
