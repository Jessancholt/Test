using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Test.UnitTests.Infrastructure;
using Xunit;

namespace Test.UnitTests.Controllers
{
    [Trait(Constants.Category, Constants.UnitTest)]
    public class PostsControllerTests
    {
        [Fact]
        public async Task GetAllReturnsResultTest()
        {
            // Arrange
            var verifier = new PostsControllerVerifierBuilder()
                .SetPostsControllerGetAllResponse()
                .SetupPostsControllerCacheWrapperGetList()
                .Build();

            // Act
            var result = await verifier.Controller.Get(It.IsAny<CancellationToken>());

            // Assert
            var actionResult = (ObjectResult)result.Result!;
            actionResult.Value.Should().BeEquivalentTo(verifier.GetAllResponse);

            verifier.VerifyPostsControllerCacheWrapperGetList();
        }

        [Fact]
        public async Task GetAllReturnsNotFoundTest()
        {
            // Arrange
            var verifier = new PostsControllerVerifierBuilder()
                .SetupPostsControllerCacheWrapperGetList()
                .Build();

            // Act
            var result = await verifier.Controller.Get(It.IsAny<CancellationToken>());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();

            verifier.VerifyPostsControllerCacheWrapperGetList();
        }

        [Fact]
        public async Task GetByIdReturnsResultTest()
        {
            // Arrange
            var verifier = new PostsControllerVerifierBuilder()
                .SetPostsControllerGetByIdResponse()
                .SetupPostsControllerCacheWrapperGet()
                .Build();

            // Act
            var result = await verifier.Controller.Get(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            var actionResult = (ObjectResult)result.Result!;
            actionResult.Value.Should().Be(verifier.GetByIdResponse);

            verifier.VerifyPostsControllerCacheWrapperGet();
        }

        [Fact]
        public async Task GetByIdReturnsNotFoundTest()
        {
            // Arrange
            var verifier = new PostsControllerVerifierBuilder()
                .SetupPostsControllerCacheWrapperGet()
                .Build();

            // Act
            var result = await verifier.Controller.Get(It.IsAny<int>(), It.IsAny<CancellationToken>());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();

            verifier.VerifyPostsControllerCacheWrapperGet();
        }

        [Fact]
        public async Task PostTest()
        {
            // Arrange
            var verifier = new PostsControllerVerifierBuilder()
                .SetPostsControllerPostRequest()
                .SetupPostsControllerPostsServiceCreateAsync()
                .Build();

            // Act
            var result = await verifier.Controller.Post(verifier.PostRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            verifier
                .VerifyPostsControllerPostsServiceCreateAsync()
                .VerifyPostsControllerLoggerError(Times.Never());
        }

        [Fact]
        public async Task PostBadRequestTest()
        {
            // Arrange
            var verifier = new PostsControllerVerifierBuilder()
                .SetupPostsControllerPostsServiceCreateAsyncThrowsException()
                .SetupPostsControllerLoggerError()
                .Build();

            // Act
            var result = await verifier.Controller.Post(verifier.PostRequest, It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeOfType<BadRequestResult>();

            verifier
                .VerifyPostsControllerPostsServiceCreateAsync()
                .VerifyPostsControllerLoggerError(Times.Once());
        }
    }
}
