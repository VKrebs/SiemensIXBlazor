using SiemensIXBlazor.Components;
using SiemensIXBlazor.Enums.Blind;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Moq;
using Microsoft.Extensions.DependencyInjection;

namespace SiemensIXBlazor.Tests
{
    public class BlindTest: TestContext
    {
        private readonly Mock<IJSRuntime> _jsRuntimeMock;
        private readonly Mock<IJSObjectReference> _jsObjectReferenceMock;

        public BlindTest()
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
            var cut = RenderComponent<Blind>();

            // Assert
            cut.MarkupMatches("<ix-blind id='' variant='insight'></ix-blind>");
        }

        [Fact]
        public void IdPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Blind>(parameters => parameters.Add(p => p.Id, "testId"));

            // Assert
            Assert.Equal("testId", cut.Instance.Id);
        }

        [Fact]
        public void CollapsedPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Blind>(parameters => parameters.Add(p => p.Collapsed, true));

            // Assert
            Assert.True(cut.Instance.Collapsed);
        }

        [Fact]
        public void IconPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Blind>(parameters => parameters.Add(p => p.Icon, "testIcon"));

            // Assert
            Assert.Equal("testIcon", cut.Instance.Icon);
        }

        [Fact]
        public void VariantPropertyIsSetCorrectly()
        {
            // Arrange
            var cut = RenderComponent<Blind>(parameters => parameters.Add(p => p.Variant, BlindVariant.insight));

            // Assert
            Assert.Equal(BlindVariant.insight, cut.Instance.Variant);
        }

        [Fact]
        public void CollapsedChangedEventTriggeredCorrectly()
        {
            // Arrange
            var eventTriggered = false;
            _jsRuntimeMock.Setup(x => x.InvokeAsync<bool>(It.IsAny<string>(), It.IsAny<object[]>()))
                          .Returns(new ValueTask<bool>(true));
            var cut = RenderComponent<Blind>(parameters => parameters.Add(p => p.CollapsedChangedEvent, EventCallback.Factory.Create<bool>(this, () => eventTriggered = true)));

            // Act
            cut.Instance.CollapsedChangedEvent.InvokeAsync(true);

            // Assert
            Assert.True(eventTriggered);
        }
    }
}