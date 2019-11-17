﻿using System;
using FluentAssertions;
using Markdown.MdProcessing;
using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    public class Md_should
    {
        private Md md;
        
        [SetUp]
        public void SetUp()
        {
            md = new Md();
        }

        [Test]
        public void Render_Should_ThrowInvalidOperationException_When_NoMdProcessorWasSet()
        {
            Following.Code(() => md.Render("text")).Should().Throw<InvalidOperationException>();
        }
        
        [TestCase("abc", "abc", TestName = "when no md symbols found")]
        [TestCase("abc dce", "abc dce", TestName = "when no md symbols found and two words")]
        [TestCase("abc dce", "abc dce", TestName = "when no md symbols found and two words")]
        [TestCase("abc      dce", "abc dce", TestName = "when additional spaces and two words")]
        [TestCase("_abc_ dce", "<em>abc</em> dce", TestName = "when word with emphasis and standard word")]
        [TestCase("_ abc _ dce", "_ abc _ dce", TestName = "when word with emphasis with spaces and standard word")]
        [TestCase("__abc__ dce", "<strong>abc</strong> dce", TestName = "when word with bold symbols and standard word")]
        [TestCase("__ abc __ dce", "__ abc __ dce", TestName = "when word with bold symbols with spaces and standard word")]
        [TestCase("__abc__ _dce_", "<strong>abc</strong> <em>dce</em>", TestName = "when words with bold symbols and emphasis")]
        [TestCase("__Lorem _ipsum_ dolor sit amet__", "<strong>Lorem <em>ipsum</em> dolor sit amet</strong>", TestName = "when text with nested emphasis")]
        public void Render_Should_RenderMdToHTML(string text, string expectedResult)
        {
            md.SetMdProcessor(new MdToHtmlProcessor());
            md.Render(text).Should().Be(expectedResult);
        }
    }
}