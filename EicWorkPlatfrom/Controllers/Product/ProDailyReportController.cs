﻿using System;
using System.Web.Mvc;
using System.Collections.Generic;
using Lm.Eic.App.DomainModel.Bpm.Pms.DailyReport;
using Lm.Eic.App.Business.Bmp.Pms.DailyReport;
using System.IO;
using Lm.Eic.App.Business.Bmp.Hrm.Archives;
using System.Web;

namespace EicWorkPlatfrom.Controllers.Product
{
    public class ProDailyReportController : EicBaseController
    {
        //
        // GET: /DailyReport/

        public ActionResult Index()
        {
            return View();
        }

        #region report hour set method
        public ActionResult DReportHoursSet()
        {
            return View();
        }

        /// <summary>
        /// 获取产品工艺流程列表
        /// </summary>
        /// <param name="department"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        [HttpGet]
        [NoAuthenCheck]
        public JsonResult GetProductFlowList(string department, string productName, string orderId, int searchMode)
        {
            var result = DailyReportService.ConfigManager.ProductFlowSetter.GetProductFlowListBy(new QueryDailyReportDto()
            {
                Department = department,
                ProductName = productName,
                OrderId = orderId,
                SearchMode = searchMode
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存产品工艺流程数据
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        [HttpPost]
        [NoAuthenCheck]
        public JsonResult StoreProductFlowDatas(List<ProductFlowModel> entities)
        {
            var datas = DailyReportService.ConfigManager.ProductFlowSetter.Store(entities);
            return Json(datas);
        }

        /// <summary>
        /// 获取产品工艺初始化数据
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetProductFlowInitData(string department)
        {
            var departments = ArchiveService.ArchivesManager.DepartmentMananger.Departments;
            var productFlowOverviews = DailyReportService.ConfigManager.ProductFlowSetter.GetProductFlowOverviewListBy(department);
            var data = new { departments = departments, overviews = productFlowOverviews };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 载入产品工艺流程模板
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult LoadProductFlowTemplateFile()
        {
            string filePath = @"E:\各部门日报格式\日报数据表.xls";
            MemoryStream ms = DailyReportService.ConfigManager.ProductFlowSetter.GetProductFlowTemplate(filePath);
            return this.ExportToExcel(ms, "产品工艺流程模板", "产品工艺流程模板");
        }

        public JsonResult ImportProductFlowDatas(HttpPostedFileBase file)
        {
            List<ProductFlowModel> datas = null;
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    ///待加入验证文件名称逻辑:
                    string fileName = Path.Combine(this.CombinedFilePath(FileLibraryKey.FileLibrary, FileLibraryKey.Temp), file.FileName);
                    file.SaveAs(fileName);
                    datas= DailyReportService.ConfigManager.ProductFlowSetter.ImportProductFlowListBy(fileName);
                    System.IO.File.Delete(fileName);
                }
            }
            return Json(datas, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region daily report  input method
        /// <summary>
        /// 日报录入
        /// </summary>
        /// <returns></returns>
        public ActionResult DReportInput()
        {
            return View();
        }
        public ActionResult EditRemarkViewTpl()
        {
            return View();
        }
        /// <summary>
        /// 获取日报输入模板
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public ContentResult GetDailyReportTemplate(string department,DateTime dailyReportDate)
        {
            var datas = DailyReportService.InputManager.DailyReportInputManager.GetDailyReportTemplate(department, dailyReportDate);
            return DateJsonResult(datas);
        }
        /// <summary>
        /// 获取工单详细信息
        /// </summary>
        /// <param name="department"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet]
        [NoAuthenCheck]
        public JsonResult GetOrderDetails(string department,string orderId)
        {
            var orderDetails = DailyReportService.InputManager.DailyReportInputManager.GetOrderDetails(orderId);
            var productFlows = DailyReportService.ConfigManager.ProductFlowSetter.GetProductFlowListBy(new QueryDailyReportDto()
            {
                SearchMode = 5,
                Department = department,
                OrderId = orderId
            });
            var data = new { orderDetails = orderDetails, productFlows = productFlows };//productFlows = productFlows
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取日报录入初始化数据
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult GetDReportInitData(string department)
        {
            var departments = ArchiveService.ArchivesManager.DepartmentMananger.Departments;
            var machines = DailyReportService.ConfigManager.MachineSetter.GetMachineListBy(department);
            var unproductReasons = DailyReportService.ConfigManager.NonProductionReasonSetter.GetNonProductionReasonListBy(department);
            var datas = new { departments = departments, machines = machines, unproductReasons = unproductReasons };
            return Json(datas, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 生成日报清单
        /// </summary>
        /// <returns></returns>
        [NoAuthenCheck]
        public FileResult CreateDailyReportList()
        {

            string department = "成型课";
            DateTime dailyReportDate = DateTime.Now;
            //待添加
            var ms = DailyReportService.InputManager.DailyReportInputManager.BuildDailyReportTempList(department, dailyReportDate);
            return this.ExportToExcel(ms, "日报数据", "日报数据");
        }
        /// <summary>
        /// 保存日报录入数据
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult SaveDailyReportDatas(List<DailyReportTempModel> datas, DateTime inputDate)
        {

            var result = DailyReportService.InputManager.DailyReportInputManager.SavaDailyReportList(datas, inputDate);
            return Json(result);
        }
        /// <summary>
        /// 审核日报数据
        /// </summary>
        /// <param name="department">部门</param>
        /// <returns></returns>
        [NoAuthenCheck]
        public JsonResult AuditDailyReport(string department,DateTime dailyReportDate)
        {
            var result = DailyReportService.InputManager.DailyReportInputManager.AuditDailyReport(department, dailyReportDate);
            return Json(result);
        }
        #endregion
    }
}