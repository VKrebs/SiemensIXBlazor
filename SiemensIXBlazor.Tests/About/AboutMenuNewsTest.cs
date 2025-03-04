﻿using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using SiemensIXBlazor.Components.About;

namespace SiemensIXBlazor.Tests.About
{
    public class AboutMenuNewsTest: TestContext
    {
        private readonly Mock<IJSRuntime> _jsRuntimeMock;
        private readonly Mock<IJSObjectReference> _jsObjectReferenceMock;

        public AboutMenuNewsTest()
        {
            _jsRuntimeMock = new Mock<IJSRuntime>();
            _jsObjectReferenceMock = new Mock<IJSObjectReference>();

            // Mock of module import for JSRuntime
            _jsRuntimeMock.Setup(x => x.InvokeAsync<IJSObjectReference>("import", It.IsAny<object[]>()))
                          .Returns(new ValueTask<IJSObjectReference>(_jsObjectReferenceMock.Object));
            Services.AddSingleton(_jsRuntimeMock.Object);
        }


        [Fact]
        public void ComponentRendersWithoutCrashing()
        {
            // Arrange
            var cut = RenderComponent<AboutMenuNews>();

            // Assert
            cut.MarkupMatches("<ix-menu-about-news id='' i-1-8n-show-more='Show more' offset-bottom='0'></ix-menu-about-news>");
        }

        [Fact]
        public void ChildContentPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<AboutMenuNews>(parameters => parameters.Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "Test content"))));

            // Assert
            Assert.NotNull(cut.Instance.ChildContent);
        }

        [Fact]
        public void AboutItemLabelPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<AboutMenuNews>(parameters => parameters.Add(p => p.AboutItemLabel, "testAboutItemLabel"));

            // Assert
            Assert.Equal("testAboutItemLabel", cut.Instance.AboutItemLabel);
        }

        [Fact]
        public void ExpandedPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<AboutMenuNews>(parameters => parameters.Add(p => p.Expanded, true));

            // Assert
            Assert.True(cut.Instance.Expanded);
        }

        [Fact]
        public void I18nShowMorePropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<AboutMenuNews>(parameters => parameters.Add(p => p.I18NShowMore, "showMoreTest"));

            // Assert
            Assert.Equal("showMoreTest", cut.Instance.I18NShowMore);
        }

        [Fact]
        public void LablePropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<AboutMenuNews>(parameters => parameters.Add(p => p.Label, "testLabel"));

            // Assert
            Assert.Equal("testLabel", cut.Instance.Label);
        }

        [Fact]
        public void OffsetBottomPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<AboutMenuNews>(parameters => parameters.Add(p => p.OffsetBottom, 1));

            // Assert
            Assert.Equal(1, cut.Instance.OffsetBottom);
        }

        [Fact]
        public void ShowPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<AboutMenuNews>(parameters => parameters.Add(p => p.Show, true));

            // Assert
            Assert.True(cut.Instance.Show);
        }

        [Fact]
        public void ClosePopoverEventTriggeredCorrectly()
        {
            // Arrange
            var eventTriggered = false;
            var cut = RenderComponent<AboutMenuNews>(parameters => parameters.Add(p => p.ClosePopoverEvent, EventCallback.Factory.Create(this, () => eventTriggered = true)));

            // Act
            cut.Instance.ClosePopoverEvent.InvokeAsync();

            // Assert
            Assert.True(eventTriggered);
        }

        [Fact]
        public void ShowMoreEventTriggeredCorrectly()
        {
            // Arrange
            var eventTriggered = false;
            var cut = RenderComponent<AboutMenuNews>(parameters => parameters.Add(p => p.ShowMoreEvent, EventCallback.Factory.Create<MouseEventArgs>(this, () => eventTriggered = true)));

            // Act
            cut.Instance.ShowMoreEvent.InvokeAsync(new MouseEventArgs());

            // Assert
            Assert.True(eventTriggered);
        }
    }
}
