﻿using Lm.Eic.App.DomainModel.Bpm.Quanity;
using Lm.Eic.App.Erp.Bussiness.QmsManage;
using Lm.Eic.App.Erp.Bussiness.QuantityManage;
using Lm.Eic.App.Erp.Domain.QuantityModel;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;
using Lm.Eic.Uti.Common.YleeOOMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lm.Eic.App.Business.Bmp.Quality.InspectionManage
{
    /// <summary>
    /// 进料检验数据采集器
    /// </summary>
    public class IqcInspectionDataGather
    {
        /// <summary>
        /// 得到总表信息
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="materialId"></param>
        /// <returns></returns>
        public List<IqcInspectionItemDataSummaryLabelModel> GetIqcInspectionItemDataSummaryLabelList(string orderId, string materialId)
        {
            List<IqcInspectionItemDataSummaryLabelModel> returnList = new List<IqcInspectionItemDataSummaryLabelModel>();
            var orderIdInfoList = GetPuroductSupplierInfo(orderId); if (orderIdInfoList == null || orderIdInfoList.Count <= 0) return returnList;
            // 得到需要检验的项目
            var iqcNeedInspectionsItemdatas = IqcInspectionManagerCrudFactory.InspectionItemConfigCrud.FindIqcInspectionItemConfigDatasBy(materialId);
            if (iqcNeedInspectionsItemdatas != null && iqcNeedInspectionsItemdatas.Count > 0)
                iqcNeedInspectionsItemdatas.ForEach(m =>
                    {
                       var orderMaterialInfo = orderIdInfoList.FirstOrDefault(e => e.ProductID == materialId);
                        ///得到检验方法数据
                       var inspectionModeConfigModelData = GetInspectionModeConfigDataBy(m, orderMaterialInfo.ProduceNumber);
                        ///得到已经检验的数据
                       var  iqcHaveInspectionData = InspectionService.InspectionDataGather.GetIqcInspectionDetailModelBy(orderId, m.MaterialId, m.InspectionItem);
                       var model = new IqcInspectionItemDataSummaryLabelModel()
                        {
                            OrderId = orderId,
                            MaterialId = m.MaterialId,
                            InspectionItem = m.InspectionItem,
                            SizeLSL = m.SizeLSL,
                            SizeUSL = m.SizeUSL,
                            SizeMemo=m.SizeMemo,
                            AcceptCount= 0,
                            InspectionAQL = string.Empty ,
                            InspectionMode = string.Empty,
                            InspectionLevel = string.Empty,
                            RefuseCount = 0,
                            NeedFinishDataNumber =0,
                            InspectionCount= 0,
                            InspectionItemDatas = string.Empty,
                            InsptecitonItemIsFinished = false,
                            HaveFinishDataNumber =0,
                            InspectionItemResult=string.Empty 
                          
                        };
                        if (inspectionModeConfigModelData!=null )
                        {
                            model.AcceptCount = inspectionModeConfigModelData.AcceptCount;
                            model.InspectionAQL = inspectionModeConfigModelData.InspectionAQL;
                            model.InspectionMode = inspectionModeConfigModelData.InspectionMode;
                            model.InspectionLevel = inspectionModeConfigModelData.InspectionLevel;
                            model.RefuseCount = inspectionModeConfigModelData.RefuseCount;
                            //需要录入的数据个数 暂时为抽样的数量
                            model.NeedFinishDataNumber = inspectionModeConfigModelData.InspectionCount;
                            model.InspectionCount = inspectionModeConfigModelData.InspectionCount;
                        }
                        if (iqcHaveInspectionData != null)
                        {
                            model.InspectionItemDatas = iqcHaveInspectionData.InspectionItemDatas;
                            model.InspectionItemResult = iqcHaveInspectionData.InsprectionItemResult;
                            model.InsptecitonItemIsFinished = true;
                            if (iqcHaveInspectionData.InspectionItemDatas != string.Empty)
                            { model.HaveFinishDataNumber= iqcHaveInspectionData.InspectionItemDatas.Length; }
                        }
                     
                        returnList.Add(model);
                    });
           
            return returnList;
        }



        /// <summary>
        /// 得到抽样物料信息
        /// </summary>
        /// <param name="orderId">ERP单号</param>
        /// <returns></returns>
        public List<MaterialModel> GetPuroductSupplierInfo(string orderId)
        {
            return QualityDBManager.OrderIdInpectionDb.FindMaterialBy(orderId);
        }
        
        /// <summary>
        /// 得到IQC物料料号得到相应的规格参数 
        /// </summary>
        /// <param name="orderId">ERP单号</param>
        /// <param name="sampleMaterialId">物料料号</param>
        /// <returns></returns>
        public IqcInspectionItemConfigModel GetIqcInspectionItemConfigDataBy(string sampleMaterialId,string inspectionItem)
        {
            return IqcInspectionManagerCrudFactory.InspectionItemConfigCrud.FindIqcInspectionItemConfigDatasBy(sampleMaterialId).FirstOrDefault(e => e.InspectionItem == inspectionItem);
        }
       
        public IqcInspectionDetailModel GetIqcInspectionDetailModelBy(string orderId, string materailId,string inspecitonItem)
        {
            return IqcInspectionManagerCrudFactory.IqcInspectionDetailCrud.GetIqcInspectionDetailModelBy(orderId, materailId, inspecitonItem);
        }
        /// <summary>
        /// 存储Iqc检验数据
        /// </summary>
        /// <returns></returns>
        public OpResult StoreIqcInspectionDetailModel(IqcInspectionDetailModel model)
        {
            return IqcInspectionManagerCrudFactory.IqcInspectionDetailCrud.Store(model,true);
        }
        /// <summary>
        /// 存储Iqc检验项次
        /// </summary>
        /// <returns></returns>
        public OpResult StoreIqcInspectionMasterModel(IqcInspectionMasterModel model)
        {
            return IqcInspectionManagerCrudFactory.IqcInspectionMasterCrud.Store(model, true);
        }
        /// <summary>
        /// 由检验项目得到检验方式模块
        /// </summary>
        /// <param name="iqcInspectionItemConfig"></param>
        /// <param name="inMaterialCount"></param>
        /// <returns></returns>
        public InspectionModeConfigModel GetInspectionModeConfigDataBy(IqcInspectionItemConfigModel iqcInspectionItemConfig, double inMaterialCount)
        {
            var maxs = new List<Int64>(); var mins = new List<Int64>();
            double maxNumber; double minNumber;
            if (iqcInspectionItemConfig == null) return new InspectionModeConfigModel(); ;
            var models = IqcInspectionManagerCrudFactory.InspectionModeConfigCrud.GetInspectionStartEndNumberBy(
                iqcInspectionItemConfig.InspectionMode,
                iqcInspectionItemConfig.InspectionLevel,
                iqcInspectionItemConfig.InspectionAQL);
            models.ForEach(e =>
            { maxs.Add(e.EndNumber); mins.Add(e.StartNumber); });
            if (maxs.Count > 0)
                maxNumber = GetMaxNumber(maxs, inMaterialCount);
            else
                maxNumber = 0;
            if (mins.Count > 0)
                minNumber = GetMinNumber(mins, inMaterialCount);
            else
                minNumber = 0;
            return models.Where(e => e.StartNumber == minNumber && e.EndNumber == maxNumber).ToList().FirstOrDefault();
            // InspectionCount, AcceptCount, RefuseCount,
        }
        private Int64 GetMaxNumber(List<Int64> maxNumbers, double number)
        {
            List<Int64> IntMaxNumbers = new List<Int64>();
            foreach (var max in maxNumbers)
            {
                if (max != -1)
                {

                    if (max >= number)
                    {
                        IntMaxNumbers.Add(max);
                    }
                }
            }
            if (IntMaxNumbers.Count > 0)
            { return IntMaxNumbers.Min(); }
            else return -1;
        }
        private Int64 GetMinNumber(List<Int64> minNumbers, double mumber)
        {
            List<Int64> IntMinNumbers = new List<Int64>();
            foreach (var min in minNumbers)
            {
                if (min != -1)
                {

                    if (min <= mumber)
                    {
                        IntMinNumbers.Add(min);
                    }
                }
                else return -1;
            }
            return IntMinNumbers.Max();
        }
    }

}
