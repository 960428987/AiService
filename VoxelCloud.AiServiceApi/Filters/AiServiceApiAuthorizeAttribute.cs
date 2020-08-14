using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using log4net;
using Microsoft.Extensions.Logging;

namespace VoxelCloud.AiServiceApi
{
    public class AiServiceApiAuthorizeAttribute : ActionFilterAttribute
    {
        public readonly ILogger<AiServiceApiAuthorizeAttribute> _logger;
        public AiServiceApiAuthorizeAttribute(ILogger<AiServiceApiAuthorizeAttribute> logger)
        {
              _logger = logger;
        }
        private string privateKey = "c47d187067c6cf953245f128b5fde62a";
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            try
            {
                string str = "";
                string encryptedStr = "";
                var para = actionContext.HttpContext.Request.Query.AsQueryable().ToList();
                // var sortResult2 = from pair in s orderby pair.Key descending select pair; //以字典Key值逆序排序
                var sortResult = from pair in para orderby pair.Key ascending select pair; //以字典Key值顺序排序
                string timeStamp = para.Where(t => t.Key == "stamp").FirstOrDefault().Value.ToString();
                DateTime dt = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(timeStamp)).ToLocalTime().DateTime;
                double timeExpire = Convert.ToDouble(DateTime.Now.Subtract(dt).Duration().TotalSeconds.ToString());
                if (timeExpire > 5)
                {
                    actionContext.Result = new JsonResult("请求已过时");
                    actionContext.HttpContext.Response.StatusCode = 401;
                    _logger.LogInformation("请求已过时");

                }
                else
                {
                    foreach (var item in sortResult)
                    {
                        if (item.Key != "sign")
                        {
                            str += (item.Key.Trim() + "=" + item.Value.ToString() + "&");
                        }
                    }
                    str += privateKey;
                    using (var md5 = MD5.Create())
                    {
                        var result = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
                        var strResult = BitConverter.ToString(result);
                        encryptedStr = strResult.Replace("-", "").ToLower();
                    }
                    if (encryptedStr != para.Where(t => t.Key == "sign").FirstOrDefault().Value.ToString())
                    {
                        actionContext.Result = new JsonResult("请求来源非法");
                        actionContext.HttpContext.Response.StatusCode = 401;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            
        }
    }

}
