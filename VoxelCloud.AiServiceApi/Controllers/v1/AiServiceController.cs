using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VoxelCloud.AiServiceApi.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace VoxelCloud.AiServiceApi.Controllers.v1
{
    /// <summary>
    /// 服务接口控制器  
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [TypeFilter(typeof(AiServiceApiAuthorizeAttribute))]
    [ApiController]
    public class AiServiceController : ControllerBase
    {
        private readonly ILogger<AiServiceController> _logger;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        public AiServiceController(ILogger<AiServiceController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 添加患者
        /// </summary>
        /// <param name="model"></param>
        /// <param name="paraModel"></param>
        /// <returns></returns>
        [Route("add-study")]
        [HttpPost]
        public ResponseModel AddStudy([FromBody] AddStudyModel model,[FromQuery] ParaModel paraModel)
        {
            string s = Request.Query["version"];
            _logger.LogInformation("AddStudyModel参数："+JsonConvert.SerializeObject(model));
            _logger.LogInformation("ParaModel参数：" + JsonConvert.SerializeObject(paraModel));
            ResponseModel responseModel = new ResponseModel()
            {
                isSuccess = true,
                resultCode = "200",
                resultMsg = "成功"
            };
            return responseModel;
        }
    }
}
