using AutoMapper;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Http;
using Moq;
using Serilog;
using Test.BusinessLogic.Services.Interfaces;
using Test.DataAccess.Entities;
using Test.WebAPI.Controllers;
using Test.WebAPI.Infrastructure.MappingProfiles;
using Test.WebAPI.Models.ApiRequests;
using Test.WebAPI.Models.ApiResponses;

namespace Test.UnitTests.Infrastructure
{
    public class PostsControllerVerifierBuilder
    {
        private readonly Mock<IPostsService> _service;
        private readonly Mock<ICacheService<string, PostResponse>> _cache;
        private readonly Mock<ILogger> _logger;
        
        private PostRequest _postRequest;
        private IList<PostResponse> _getAllResponse;
        private PostResponse _getByIdResponse;

        private readonly PostsController _controller;

        public PostsControllerVerifierBuilder()
        {
            _service = new Mock<IPostsService>();
            _cache = new Mock<ICacheService<string, PostResponse>>();
            _logger = new Mock<ILogger>();

            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<PostModelMapProfile>()));

            _controller = new PostsController(
                _service.Object,
                _cache.Object,
                mapper,
                _logger.Object);

            _controller.ControllerContext.HttpContext = new DefaultHttpContext
            {
                RequestAborted = CancellationToken.None
            };
        }

        public PostsControllerVerifierBuilder SetPostsControllerGetAllResponse()
        {
            _getAllResponse = Builder<PostResponse>
                .CreateListOfSize(3)
                .Build();

            return this;
        }

        public PostsControllerVerifierBuilder SetPostsControllerGetByIdResponse()
        {
            _getByIdResponse = Builder<PostResponse>
                .CreateNew()
                .Build();

            return this;
        }

        public PostsControllerVerifierBuilder SetPostsControllerPostRequest()
        {
            _postRequest = Builder<PostRequest>
                .CreateNew()
                .Build();

            return this;
        }

        public PostsControllerTestsVerifier Build()
        {
            return new PostsControllerTestsVerifier(
                _service,
                _cache,
                _logger,
                _getAllResponse,
                _getByIdResponse,
                _postRequest,
                _controller);
        }

        public PostsControllerVerifierBuilder SetupPostsControllerPostsServiceCreateAsync()
        {
            _service.Setup(x => x.CreateAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            return this;
        }

        public PostsControllerVerifierBuilder SetupPostsControllerPostsServiceCreateAsyncThrowsException()
        {
            _service.Setup(x => x.CreateAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(It.IsAny<Exception>())
                .Verifiable();

            return this;
        }

        public PostsControllerVerifierBuilder SetupPostsControllerCacheWrapperGetList()
        {
            _cache.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<Func<Task<IList<PostResponse>>>>()))
                .ReturnsAsync(_getAllResponse)
                .Verifiable();

            return this;
        }

        public PostsControllerVerifierBuilder SetupPostsControllerCacheWrapperGet()
        {
            _cache.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<Func<Task<PostResponse>>>()))
                .ReturnsAsync(_getByIdResponse)
                .Verifiable();

            return this;
        }

        public PostsControllerVerifierBuilder SetupPostsControllerLoggerError()
        {
            _logger.Setup(x => x.Error(It.IsAny<string>()))
                .Verifiable();

            return this;
        }
    }
}
