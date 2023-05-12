using Moq;
using Serilog;
using Test.BusinessLogic.Services.Interfaces;
using Test.DataAccess.Entities;
using Test.WebAPI.Controllers;
using Test.WebAPI.Models.ApiRequests;
using Test.WebAPI.Models.ApiResponses;

namespace Test.UnitTests.Infrastructure
{
    public class PostsControllerTestsVerifier
    {
        private readonly Mock<IPostsService> _service;
        private readonly Mock<ICacheService<string, PostResponse>> _cache;
        private readonly Mock<ILogger> _logger;

        public IList<PostResponse> GetAllResponse;
        public PostResponse GetByIdResponse;
        public PostRequest PostRequest;

        public readonly PostsController Controller;

        public PostsControllerTestsVerifier(
            Mock<IPostsService> service,
            Mock<ICacheService<string, PostResponse>> cache,
            Mock<ILogger> logger,
            IList<PostResponse> getAllResponse,
            PostResponse getByIdResponse,
            PostRequest postRequest,
            PostsController controller)
        {
            _service = service;
            _cache = cache;
            _logger = logger;
            GetAllResponse = getAllResponse;
            GetByIdResponse = getByIdResponse;
            PostRequest = postRequest;
            Controller = controller;
        }

        public PostsControllerTestsVerifier VerifyPostsControllerPostsServiceCreateAsync()
        {
            _service.Verify(x => x.CreateAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public PostsControllerTestsVerifier VerifyPostsControllerCacheWrapperGetList()
        {
            _cache.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<Func<Task<IList<PostResponse>>>>()), Times.Once);

            return this;
        }

        public PostsControllerTestsVerifier VerifyPostsControllerCacheWrapperGet()
        {
            _cache.Verify(x => x.Get(It.IsAny<string>(), It.IsAny<Func<Task<PostResponse>>>()), Times.Once);

            return this;
        }

        public PostsControllerTestsVerifier VerifyPostsControllerLoggerError(Times times)
        {
            _logger.Verify(x => x.Error(It.IsAny<string>()), times);

            return this;
        }
    }
}
