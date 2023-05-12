using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Test.BusinessLogic.Services.Interfaces;
using Test.DataAccess.Entities;
using Test.WebAPI.Models.ApiRequests;
using Test.WebAPI.Models.ApiResponses;

namespace Test.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsService;
        private readonly ICacheService<string, PostResponse> _cache;
        private readonly IMapper _mapper;
        private readonly Serilog.ILogger _logger;

        public PostsController(
            IPostsService postsService,
            ICacheService<string, PostResponse> cache,
            IMapper mapper,
            Serilog.ILogger logger)
        {
            _postsService = postsService;
            _mapper = mapper;
            _logger = logger;
            _cache = cache;
        }

        /// <summary>
        /// Gets the post by specified identifier.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PostResponse>>> Get(CancellationToken cancellationToken)
        {
            var response = await _cache.Get("get", async () =>
            {
                var results = await _postsService.GetAsync(cancellationToken);
                return _mapper.Map<List<PostResponse>>(results);
            });
            
            if (response is null)
                return NotFound();

            return new ObjectResult(response);
        }

        /// <summary>
        /// Gets the post by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PostResponse>> Get(int id, CancellationToken cancellationToken)
        {
            var response = await _cache.Get("get/" + id, async () =>
            {
                var post = await _postsService.GetAsync(id, cancellationToken);
                return _mapper.Map<PostResponse>(post);
            });

            if (response is null)
                return NotFound();

            return new ObjectResult(response);
        }

        /// <summary>
        /// Posts the specified create post request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Post(PostRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var post = _mapper.Map<Post>(request);
                await _postsService.CreateAsync(post, cancellationToken);
                return Ok(post);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return BadRequest();
            }
        }
    }
}
