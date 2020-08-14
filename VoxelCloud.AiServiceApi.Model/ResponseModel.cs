using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VoxelCloud.AiServiceApi.Model
{
   public class ResponseModel
    {
        /// <summary>
        /// 接口调用是否成功
        /// </summary>
        [Required]
        public bool isSuccess { get; set; }

        /// <summary>
        /// 返回码
        /// </summary>
        [Required]
        public string resultCode { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        [Required]
        public string resultMsg { get; set;}
    }
}
