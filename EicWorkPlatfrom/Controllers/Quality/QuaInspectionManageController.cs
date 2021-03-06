﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lm.Eic.App.Business.Bmp.Quality.InspectionManage;
using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using System.IO;
using Lm.Eic.App.Erp.Bussiness.QmsManage;
using Lm.Eic.Uti.Common.YleeOOMapper;

namespace EicWorkPlatfrom.Controllers
{
    /********************************************************************
    	created:	2017/03/27
    	file ext:	cs
    	author:		YLee
    	purpose:
    *********************************************************************/
    public class QuaInspectionManageController : EicBaseController
    {
        //
        // GET: /QuaInspectionManage/


        public ActionResult Index()
        {
            return View();
        }

        #region 检验项目配置

        #region IQC
        /// <summary>
        /// IQC检验项目配置
        /// </summary>
        /// <returns></returns>
        public ActionResult IqcInspectionItemConfiguration()
        {
            return View();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        [HttpGet]
        public JsonResult GetIqcspectionItemConfigDatas(string materialId)
        {
            //得到此物料的品名 ，规格 ，供应商，图号
            var ProductMaterailModel = QmsDbManager.MaterialInfoDb.GetProductInfoBy(materialId).FirstOrDefault();
            //添加物料检验项
            var InspectionItemConfigModelList = InspectionService.ConfigManager.IqcItemConfigManager.GetIqcspectionItemConfigDatasBy(materialId);

            var datas = new { ProductMaterailModel, InspectionItemConfigModelList };
            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 在数据库中是否存在此料号
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        [HttpGet]
        public JsonResult CheckIqcspectionItemConfigMaterialId(string materialId)
        {
            var result = InspectionService.ConfigManager.IqcItemConfigManager.IsExistInspectionConfigMaterailId(materialId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除进料检验配置数据
        /// </summary>
        /// <param name="configItem"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        [HttpPost]
        public JsonResult DeleteIqlInspectionConfigItem(InspectionIqcItemConfigModel configItem)
        {
            var opResult = InspectionService.ConfigManager.IqcItemConfigManager.StoreIqcInspectionItemConfig(configItem);
            return Json(opResult);
        }
        /// <summary>
        /// 批量保存IQC进料检验项目配置数据
        /// </summary>
        /// <param name="iqcInspectionConfigItems"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult SaveIqcInspectionItemConfigDatas(List<InspectionIqcItemConfigModel> iqcInspectionConfigItems)
        {
            var opResult = InspectionService.ConfigManager.IqcItemConfigManager.StoreIqcInspectionItemConfig(iqcInspectionConfigItems);
            return Json(opResult);
        }
        /// <summary>
        /// 导入EXCEL数据到IQC物料检验配置
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult ImportIqcInspectionItemConfigDatas(HttpPostedFileBase file)
        {
            List<InspectionIqcItemConfigModel> datas = null;
            string filePath = this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.Temp);
            this.SaveFileToServer(file, filePath, () =>
            {
                string fileName = Path.Combine(filePath, file.FileName);
                datas = InspectionService.ConfigManager.IqcItemConfigManager.ImportProductFlowListBy(fileName);
                if (datas != null && datas.Count > 0)
                //批量保存数据
                { var opResult = InspectionService.ConfigManager.IqcItemConfigManager.StoreIqcInspectionItemConfig(datas); }

                System.IO.File.Delete(fileName);
            });
            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 载入IQC物料检验配置模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult LoadIqcInspectionItemConfigFile()
        {
            ///路经下载
            string filePath = @"E:\各部门日报格式\IQC物料检验配置数据表.xls";
            var dlfm = InspectionService.ConfigManager.IqcItemConfigManager.GetIqcInspectionItemConfigTemplate(filePath, "IQC物料检验配置模板");
            return this.DownLoadFile(dlfm);
            //return null;
        }
        #endregion

        #region FQC
        /// <summary>
        /// IQC检验项目配置
        /// </summary>
        /// <returns></returns>
        public ActionResult FqcInspectionItemConfiguration()
        {
            return View();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        [HttpGet]
        public JsonResult GetFqcInspectionItemConfigDatas(string materialId)
        {
            //添加物料检验项
            var InspectionItemConfigModelList = InspectionService.ConfigManager.FqcItemConfigManager.GetFqcspectionItemConfigDatasBy(materialId);
            //得到此物料的品名 ，规格 ，供应商，图号
            var ProductMaterailModel = QmsDbManager.MaterialInfoDb.GetProductInfoBy(materialId).FirstOrDefault();

            var datas = new { ProductMaterailModel, InspectionItemConfigModelList };
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 在数据库中是否存在此料号
        /// </summary>
        /// <param name="materialId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        [HttpGet]
        public JsonResult CheckFqcInspectionItemConfigMaterialId(string materialId)
        {

            var result = InspectionService.ConfigManager.FqcItemConfigManager.IsExistFqcConfigMaterailId(materialId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除进料检验配置数据 deleteIqlInspectionConfigItem
        /// </summary>
        /// <param name="configItem"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        [HttpPost]
        public JsonResult DeleteFqcInspectionConfigItem(InspectionFqcItemConfigModel configItem)
        {

            var opResult = InspectionService.ConfigManager.FqcItemConfigManager.StoreFqcInspectionItemConfig(configItem);
            return Json(opResult);
        }

        /// <summary>
        /// 批量保存IQC进料检验项目配置数据
        /// </summary>
        /// <param name="fqcInspectionConfigItems"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult SaveFqcInspectionItemConfigDatas(List<InspectionFqcItemConfigModel> fqcInspectionConfigItems)
        {

            var opResult = InspectionService.ConfigManager.FqcItemConfigManager.StoreFqcInspectionItemConfig(fqcInspectionConfigItems);
            return Json(opResult);
        }

        #endregion


        #endregion

        #region  检验方式
        public ActionResult IqcInspectionModeConfiguration()
        {
            return View();
        }
        /// <summary>
        /// 存储  检验方式配置
        /// </summary>
        /// <param name="inspectionMode"></param>
        /// <returns></returns>
        ///获取检验水平数据
        [NoAuthenCheck]
        public JsonResult GetInspectionLevelValues(string inspectionMode)
        {
            var data = InspectionService.ConfigManager.ModeConfigManager.GetInspectionModeConfigStrList(inspectionMode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        ///获取AQL数据
        [NoAuthenCheck]
        public JsonResult GetInspectionAQLValues(string inspectionMode, string inspectionLevel)
        {
            var data = InspectionService.ConfigManager.ModeConfigManager.GetInspectionModeConfigStrList(inspectionMode, inspectionLevel);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 存储配置方式
        /// </summary>
        /// <param name="inspectionModeConfigEntity"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult StoreInspectionModeConfigData(InspectionModeConfigModel inspectionModeConfigEntity)
        {
            var opResult = InspectionService.ConfigManager.ModeConfigManager.StoreInspectionModeConfig(inspectionModeConfigEntity);
            return Json(opResult);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="inspectionMode"></param>
        /// <param name="inspectionLevel"></param>
        /// <param name="inspectionAQL"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetIqcInspectionModeDatas(string inspectionMode, string inspectionLevel, string inspectionAQL)
        {
            var datas = InspectionService.ConfigManager.ModeConfigManager.GetInInspectionModeConfigModelList(inspectionMode, inspectionLevel, inspectionAQL);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 检验方式转换配置
        public ActionResult InspectionModeSwitchConfiguration()
        {
            return View();
        }
        [NoAuthenCheck]
        public JsonResult GetModeSwitchDatas(string inspectionModeType)
        {
            var datas = InspectionService.ConfigManager.ModeSwithConfigManager.GetInspectionModeSwithConfig(inspectionModeType);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult SaveModeSwitchDatas(string inspectionModeType, List<InspectionModeSwitchConfigModel> switchModeList)
        {
            var opResult = InspectionService.ConfigManager.ModeSwithConfigManager.StroeInspectionModeSwithConfig(inspectionModeType, switchModeList);
            return Json(opResult);
        }
        #endregion

        #region  检验项目数据收集
        #region IQC

        [NoAuthenCheck]
        public ActionResult InspectionDataGatheringOfIQC()
        {
            return View();
        }
        /// <summary>
        /// 由单号得到物料所有信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetIqcMaterialInfoDatas(string orderId)
        {
            var datas = InspectionService.DataGatherManager.IqcDataGather.GetPuroductSupplierInfo(orderId);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 由料号得到检验配置数据
        /// <param name="materialId">料号</param>
        /// <param name="orderId">单号</param>
        /// <returns></returns>
        /// </summary>
        [NoAuthenCheck]
        [HttpGet]
        public JsonResult GetIqcInspectionItemDataSummaryLabelList(string orderId, string materialId)
        {
            var datas = InspectionService.DataGatherManager.IqcDataGather.BuildingIqcInspectionItemDataSummaryLabelListBy(orderId, materialId);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 上传FQC检验采集数据附件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult UploadIqcGatherDataAttachFile(HttpPostedFileBase file)
        {
            string addchangeFileName = DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00");
            string filePath = this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.IqcInspectionGatherDataFile, DateTime.Now.ToString("yyyyMM"));
            this.SaveFileToServer(file, filePath, addchangeFileName);
            return Json("OK");
        }
        /// <summary>
        /// 存储采集的数据
        /// </summary>
        /// <param name="gatherData"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        [HttpPost]
        public JsonResult StoreIqcInspectionGatherDatas(InspectionItemDataSummaryVM gatherData)
        {
            if (gatherData == null) return Json(new OpResult("数据为空", false));
            if (gatherData.FileName != null && gatherData.FileName.Length > 1)
            {
                gatherData.DocumentPath = Path.Combine(FileLibraryKey.FileLibrary, FileLibraryKey.IqcInspectionGatherDataFile, DateTime.Now.ToString("yyyyMM"), gatherData.FileName);
                gatherData.SiteRootPath = this.SiteRootPath;
            }
            var opResult = InspectionService.DataGatherManager.IqcDataGather.StoreInspectionIqcGatherDatas(gatherData);
            return Json(opResult);
        }


        #endregion

        #region FQC
        [NoAuthenCheck]
        public ActionResult InspectionDataGatheringOfFQC()
        {
            return View();
        }
        /// <summary>
        /// 获取FQC工单物料信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetFqcOrderInfoDatas(string orderId)
        {
            var orderInfo = InspectionService.DataGatherManager.FqcDataGather.GetFqcInspectionFqcOrderIdInfoBy(orderId);
            var sampledDatas = InspectionService.DataGatherManager.FqcDataGather.MasterDatasGather.GetFqcMasterOrderIdDatasBy(orderId);
            var datas = new { orderInfo = orderInfo, sampledDatas = sampledDatas };
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 创建抽检表单项
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="sampleCount"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult CreateFqcSampleFormItem(string orderId, int sampleCount)
        {
            var datas = InspectionService.DataGatherManager.FqcDataGather.BuildingFqcInspectionSummaryDatasBy(orderId, sampleCount);

            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 找到已检验中所有检验项目
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderIdNumber"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetFqcSampleFormItems(string orderId, int orderIdNumber)
        {
            var datas = InspectionService.DataGatherManager.FqcDataGather.FindFqcInspectionSummaryDataBy(orderId, orderIdNumber);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 上传FQC检验采集数据附件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult UploadFqcGatherDataAttachFile(HttpPostedFileBase file)
        {
            string addchangeFileName = DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00");
            string filePath = this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.FqcInspectionGatherDataFile, DateTime.Now.ToString("yyyyMM"));
            this.SaveFileToServer(file, filePath, addchangeFileName);
            return Json("OK");
        }

        /// <summary>
        /// 保存FQC检验采集数据
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult StoreFqcInspectionGatherDatas(InspectionItemDataSummaryVM gatherData)
        {
            if (gatherData == null) return Json(new OpResult("数据为空，保存失败", false));
            if (gatherData.FileName != null && gatherData.FileName.Length > 1)//上传文件
            {
                gatherData.DocumentPath = Path.Combine(FileLibraryKey.FileLibrary, FileLibraryKey.FqcInspectionGatherDataFile, DateTime.Now.ToString("yyyyMM"), gatherData.FileName);
                gatherData.SiteRootPath = this.SiteRootPath;
            }
            var datas = InspectionService.DataGatherManager.FqcDataGather.StoreFqcDataGather(gatherData);
            return Json(datas);
        }

        #endregion
        #endregion

        #region 检验单管理
        #region iqc检验单管理
        [NoAuthenCheck]
        public ActionResult InspectionFormManageOfIqc()
        {
            return View();
        }
        /// <summary>
        /// 根据单据状态获得检验单数据 selectedFormStatus,dateFrom,dateTo
        /// </summary>  
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetInspectionFormManageOfIqcDatas(string formQueryString, int queryOpModel, DateTime dateFrom, DateTime dateTo)
        {
            var datas = InspectionService.InspectionFormManager.IqcFromManager.GetInspectionFormManagerDatas(formQueryString, queryOpModel, dateFrom, dateTo);
            return DateJsonResult(datas);
        }
        [NoAuthenCheck]
        public JsonResult GetInspectionFormDetailOfIqcDatas(string orderId, string materialId)
        {
            var datas = InspectionService.DataGatherManager.IqcDataGather.FindIqcInspectionItemDataSummaryLabelListBy(orderId, materialId);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult PostInspectionFormManageCheckedOfIqcData(InspectionIqcMasterModel model)
        {
            var opResult = InspectionService.InspectionFormManager.IqcFromManager.AuditIqcInspectionMasterModel(model);
            return Json(opResult);
        }
        #endregion

        #region fqc检验单管理
        /// <summary>
        /// Fqc检验单管理
        /// </summary>
        /// <returns></returns>
        public ActionResult InspectionFormManageOfFqc()
        {
            return View();
        }
        /// <summary>
        /// 根据单据状态获得检验单数据
        /// </summary>  selectedFormStatus,dateFrom,dateTo
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetInspectionFormManageOfFqcDatas(string formStatus, DateTime dateFrom, DateTime dateTo)
        {
            var datas = InspectionService.InspectionFormManager.FqcFromManager.GetInspectionFormManagerListBy(formStatus, dateFrom, dateTo);
            return DateJsonResult(datas);
        }
        [NoAuthenCheck]
        public JsonResult GetInspectionFormDetailOfFqcDatas(string orderId, int orderIdNumber)
        {

            var datas = InspectionService.InspectionFormManager.FqcFromManager.GetInspectionDatailListBy(orderId, orderIdNumber);

            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        [NoAuthenCheck]
        public JsonResult PostInspectionFormManageCheckedOfFqcData(InspectionFqcMasterModel model)
        {

            var opResult = InspectionService.InspectionFormManager.FqcFromManager.AuditFqcInspectionMasterModel(model);
            return Json(opResult);
        }
        #endregion
        #endregion

    }
}