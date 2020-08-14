using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VoxelCloud.AiServiceApi.Model
{
    public class AddStudyModel
    {
       


        /// <summary>
        /// 患者姓名
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// 患者年龄，备注：24Y：24岁，30M：30个月，200D：200天
        /// </summary>
        public string age { get; set; }

        /// <summary>
        /// 患者性别，备注：M:男，F:女,O:其他
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 患者出生日期,备注：格式要求yyyy-MM-dd
        /// </summary>
        public string birthday{ get; set; }

        /// <summary>
        /// 入院/门诊时间,备注：格式要求yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string inOrOutDate { get; set; }

        /// <summary>
        /// 接口标识,区分影像来自哪个机构
        /// </summary>
        [Required]//设置不能为空
        public string id { get; set; }

        /// <summary>
        /// AI服务ID,Ai服务的唯一标示
        /// </summary>
        [Required]
        public string aiID { get; set; }

        /// <summary>
        /// 数据基本信息
        /// </summary>
        [Required]
        public string dataInfo { get; set; }

        /// <summary>
        /// 结果回传地址,AI结果回传的地址https://api.pacs.health-100.cn/amol-back/ai/submit-result
        /// </summary>
        [Required]
        public string returnAddr { get; set; }

        /// <summary>
        /// 患者检查信息
        /// </summary>
        [Required]
        public StudyInfo studyInfo { get; set; }
    }

    public class StudyInfo
    {
        /// <summary>
        /// 检查号
        /// </summary>
        public string studyID { get; set; }

        /// <summary>
        /// 检查信息
        /// </summary>
        public string studyDescription { get; set; }

        /// <summary>
        /// 检查时间,格式要求yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string studyDateTime { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        [Required]
        public string modality { get; set; }

        /// <summary>
        /// 检查唯一标识
        /// </summary>
        [Required]
        public string studyInstanceUID { get; set; }

        /// <summary>
        /// 检查路径,http://xxxx.xxx.xxx 目前不要使用该路径
        /// </summary>
        public string studyPath { get; set; }

        /// <summary>
        /// 序列信息
        /// </summary>
        [Required]
        public List<SeriesInfo> seriesInfo { get; set; }
    }
    public class SeriesInfo
    {
        /// <summary>
        /// 序列唯一标识
        /// </summary>
        [Required]
        public string seriesInstanceUID { get; set; }

        /// <summary>
        /// 序列描述
        /// </summary>
        public string seriesDescription { get; set; }

        /// <summary>
        /// 检查部位
        /// </summary>
        public string checkPart { get; set; }

        /// <summary>
        /// 序列路径,文件完整路径使用
        /// </summary>
        [Required]
        public string seriesPath { get; set; }

        /// <summary>
        /// 图像数量
        /// </summary>
        [Required]
        public int imageCount { get; set; }

        /// <summary>
        /// 文件列表,序列下所有文件名，以逗号分隔，文件完整路径使用
        /// </summary>
        [Required]
        public string imageFileList { get; set; }

        /// <summary>
        /// 序列层厚
        /// </summary>
        public string sliceThickness { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string modality { get; set; }
    }
}
