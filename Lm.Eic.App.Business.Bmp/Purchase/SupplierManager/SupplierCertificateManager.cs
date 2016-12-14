﻿using System;
using System.Collections.Generic;
using System.Linq;

using Lm.Eic.App.Erp.Bussiness.PurchaseManage;
using Lm.Eic.App.DomainModel.Bpm.Purchase;
using Lm.Eic.Uti.Common.YleeExtension.Conversion;

using Lm.Eic.Uti.Common.YleeOOMapper;

using Lm.Eic.Uti.Common.YleeExtension.FileOperation;

namespace Lm.Eic.App.Business.Bmp.Purchase.SupplierManager
{
 /// <summary>
 /// 供应商证书管理
 /// </summary>
  public  class SupplierCertificateManager
    {
        List<EligibleSuppliersModel> QualifiedSupplierInfo = null;
        //缓存合格供应商清册表
        Dictionary<string, EligibleSuppliersModel> eligibleSuppliersModelKey = new Dictionary<string, EligibleSuppliersModel>();

        /// <summary>
        /// 从ERP中获取年份合格供应商清册表
        /// </summary>
        /// <param name="yearMoth">年份格式yyyyMM</param>
        /// <returns></returns>
        public List<EligibleSuppliersModel> GetQualifiedSupplierList(string endYearMonth)
        {
            QualifiedSupplierInfo = new List<EligibleSuppliersModel>();
            EligibleSuppliersModel model = null;
            string startYearMonth = (int.Parse(endYearMonth)-100).ToString();
            //获取供应商信息
            var supplierInfoList = GetSupplierInformationListBy(startYearMonth, endYearMonth);

            if (supplierInfoList == null || supplierInfoList.Count <= 0) return QualifiedSupplierInfo;

            supplierInfoList.ForEach(supplierInfo =>
            {
                model = new EligibleSuppliersModel();
                if (eligibleSuppliersModelKey.ContainsKey(supplierInfo.SupplierId))
                {
                    model = eligibleSuppliersModelKey[supplierInfo.SupplierId];
                }
                else
                {
                    model = getEligibleSuppliersModel(supplierInfo);
                    eligibleSuppliersModelKey.Add(supplierInfo.SupplierId, model);
                }

                QualifiedSupplierInfo.Add(model);
            });
            return QualifiedSupplierInfo.ToList();
        }

        /// 获取供应商信息
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public SupplierInfoModel GetSuppplierInfoBy(string supplierId)
        {
            try
            {
                //先从已存的数据信息中找 没有找到再从ERP中找
                SupplierInfoModel SupplierInfo = SupplierCrudFactory.SuppliersInfoCrud.GetSupplierInfoBy(supplierId);
                if (SupplierInfo == null)
                { SupplierInfo = GetErpSuppplierInfoBy(supplierId); }
                return SupplierInfo;
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }
        }
        /// <summary>
        /// 保存编辑的供应商证书信息
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public OpResult SavaEditSpplierCertificate(List<InPutSupplieCertificateInfoModel> modelList)
        {
            //判断列表是否为空
            if (modelList == null || modelList.Count <= 0) return new OpResult("数据列表不能为空");
            var model = modelList[0];
            //通过SupplierId得到供应商信息
            var supplierInfoModel = GetSuppplierInfoBy(model.SupplierId);
            //判断是否为空
            if (supplierInfoModel == null) return new OpResult(string.Format("没有{0}供应商编号", model.SupplierId), true);
            //赋值 供应商属性和采购性质
            supplierInfoModel.PurchaseType = model.PurchaseType;
            supplierInfoModel.SupplierProperty = model.SupplierProperty;
            string isExistCertificateFileName = string.Empty;
            //更新保存数据
            if (SaveSupplierInfoModel(supplierInfoModel).Result)
            {
                List<SupplierQualifiedCertificateModel> certificateModelList = new List<SupplierQualifiedCertificateModel>();
             
                //保存证书数据
                modelList.ForEach(e =>
                {

                   if(SupplierCrudFactory.SupplierQualifiedCertificateCrud.IsExistCertificateFileName(e.CertificateFileName))
                    {
                        isExistCertificateFileName += e.CertificateFileName + ",";
                    }
                    SupplierQualifiedCertificateModel savemodel = new SupplierQualifiedCertificateModel()
                    {
                        SupplierId = e.SupplierId,
                        EligibleCertificate = e.EligibleCertificate,
                        CertificateFileName =e.CertificateFileName,
                        FilePath = e.FilePath,
                        DateOfCertificate =e.DateOfCertificate.ToDate(),
                        IsEfficacy = "是",
                        OpSign = "add"
                    };
                    certificateModelList.Add(savemodel);
                });
                if (isExistCertificateFileName != String.Empty) return new OpResult("此"+isExistCertificateFileName+"文档已经存在，数据保存失败");
               else  return SupplierCrudFactory.SupplierQualifiedCertificateCrud.SavaSupplierEligibleList(certificateModelList);
            }
            else return new OpResult("数据保存失败");
        }
        /// <summary>
        /// 删除供应商证书
        /// </summary>
        /// <param name="model">实体</param>
        /// <param name="rootPath">根路经</param>
        /// <returns></returns>
        public OpResult DelEditSpplierCertificate(SupplierQualifiedCertificateModel model, string rootPath)
        {
            try
            {
                OpResult result = OpResult.SetResult("数据操作失败!");
                if (model == null || model.FilePath == string.Empty) return new  OpResult("此文档实体路经不能空");

                if (rootPath == null || rootPath == string.Empty) return  new OpResult("此根路经发生错误");

                var fileDocumentPath = rootPath + model.FilePath.Replace("/", @"\");
                if(! fileDocumentPath.ExistFile())
                     result= new OpResult("此" + fileDocumentPath + "文档不存在或路经不对");
                else
                {
                    if (fileDocumentPath.DeleteFileDocumentation())
                        result = SupplierCrudFactory.SupplierQualifiedCertificateCrud.DeleteSupplierCertificate(model);
                }
               
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }

        }
        /// <summary>
        ///获取供应商证书列表
        /// </summary>
        /// <param name="suppliersId">供应商Id</param>
        /// <returns></returns>
        public List<SupplierQualifiedCertificateModel> GetSupplierQualifiedCertificateListBy(string supplierId)
        {
            return SupplierCrudFactory.SupplierQualifiedCertificateCrud.GetQualifiedCertificateListBy(supplierId);
        }

        


     #region   internal

      
        /// <summary>
        /// 获取供应商信息表
        /// </summary>
        /// <param name="yearMoth">年份格式yyyyMM</param>
        /// <returns></returns>
        List<SupplierInfoModel> GetSupplierInformationListBy(string startYearMonth, string endYearMonth)
        {
            List<SupplierInfoModel> SupplierInfoList = new List<SupplierInfoModel>();
            //从ERP中得到此年中所有供应商Id号
            var supplierList = PurchaseDbManager.PurchaseDb.PurchaseSppuerId(startYearMonth, endYearMonth);

            if (supplierList == null || supplierList.Count <= 0) return null;
            supplierList.ForEach(supplierId =>
            {
                SupplierInfoList.Add(GetSuppplierInfoBy(supplierId));
            });
            return SupplierInfoList;
        }
        /// <summary>
        /// <summary>
        /// 批量保存供应商信息
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        OpResult SaveSupplierInfoList(List<SupplierInfoModel> modelList)
        {
            return SupplierCrudFactory.SuppliersInfoCrud.SavaSupplierInfoList(modelList);
        }
       
        /// <summary>
        /// 更新并保存供应商信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        OpResult SaveSupplierInfoModel(SupplierInfoModel model)
        {

            try
            {
                decimal findId_key = 0;
                if (SupplierCrudFactory.SuppliersInfoCrud.IsExistSupperid(model.SupplierId, out findId_key))
                {
                    model.OpSign = "edit";
                    model.Id_key = findId_key;
                }
                else model.OpSign = "add";

                return SupplierCrudFactory.SuppliersInfoCrud.Store(model);
            }
            catch (Exception ex) { throw new Exception(ex.InnerException.Message); }



        }
        /// <summary>
        /// 从ERP中得到供应商信息
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        SupplierInfoModel GetErpSuppplierInfoBy(string supplierId)
        {
            var erpSupplierInfo = PurchaseDbManager.SupplierDb.FindSpupplierInfoBy(supplierId);
           
            if (erpSupplierInfo == null) return null;
            return new SupplierInfoModel
            {
                SupplierId = supplierId,
                SupplierEmail = erpSupplierInfo.Email,
                SupplierAddress = erpSupplierInfo.Address,
                BillAddress = erpSupplierInfo.BillAddress,
                SupplierFaxNo = erpSupplierInfo.FaxNo,
                SupplierName = erpSupplierInfo.SupplierName,
                SupplierShortName = erpSupplierInfo.SupplierShortName,
                SupplierUser = erpSupplierInfo.Contact,
                SupplierTel = erpSupplierInfo.Tel,
                PayCondition = erpSupplierInfo.PayCondition
            };
        }

        /// <summary>
        /// 得到所需的证书字典
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        Dictionary<string, string> CertificateDictionary(string supplierId)
        {
            Dictionary<string, string> certificateDictionary = new Dictionary<string, string>();
            certificateDictionary.Add("供应商环境调查表", string.Empty);
            certificateDictionary.Add("供应商基本资料表", string.Empty);
            certificateDictionary.Add("供应商评鉴表", string.Empty);
            certificateDictionary.Add("不使用童工申明", string.Empty);
            certificateDictionary.Add("PCN协议", string.Empty);
            certificateDictionary.Add("廉洁承诺书", string.Empty);
            certificateDictionary.Add("质量保证协议", string.Empty);
            certificateDictionary.Add("HSF保证书", string.Empty);
            certificateDictionary.Add("REACH保证书", string.Empty);
            certificateDictionary.Add("SVHC调查表", string.Empty);
            certificateDictionary.Add("ISO14001", string.Empty);
            certificateDictionary.Add("ISO9001", string.Empty);

            var SuppliersQualifiedCertificate = GetSupplierQualifiedCertificateListBy(supplierId);
            if (SuppliersQualifiedCertificate == null || SuppliersQualifiedCertificate.Count > 0)
            {
                SuppliersQualifiedCertificate.ForEach(e =>
               {
                   if (certificateDictionary.ContainsKey(e.EligibleCertificate))
                   {
                       certificateDictionary[e.EligibleCertificate] = e.DateOfCertificate.ToShortDateString();
                   }
               });
            }
            return certificateDictionary;
        }


        EligibleSuppliersModel getEligibleSuppliersModel(SupplierInfoModel supplierInfo)
        {
            //从ERP中得到最新二次采购信息
            var SupplierLatestTwoPurchase = PurchaseDbManager.PurchaseDb.FindSupplierLatestTwoPurchaseBy(supplierInfo.SupplierId);
            // 获取供应商证书字典
            var certificateDictionary = CertificateDictionary(supplierInfo.SupplierId);
            return new EligibleSuppliersModel
            {
                LastPurchaseDate = SupplierLatestTwoPurchase[0].PurchaseDate.Trim().ToDate(),
                UpperPurchaseDate = SupplierLatestTwoPurchase[1].PurchaseDate.Trim().ToDate(),
                PurchaseUser = SupplierLatestTwoPurchase[0].PurchasePerson,
                SupplierId = supplierInfo.SupplierId,
                SupplierProperty = supplierInfo.SupplierProperty,
                PurchaseType = supplierInfo.PurchaseType,
                SupplierEmail = supplierInfo.SupplierEmail,
                SupplierAddress = supplierInfo.SupplierAddress,
                BillAddress = supplierInfo.BillAddress,
                SupplierFaxNo = supplierInfo.SupplierFaxNo,
                SupplierName = supplierInfo.SupplierName,
                Remark = supplierInfo.Remark,
                SupplierShortName = supplierInfo.SupplierShortName,
                SupplierUser = supplierInfo.SupplierUser,
                SupplierTel = supplierInfo.SupplierTel,
                EnvironmentalInvestigation = certificateDictionary["供应商环境调查表"],
                HonestCommitment = certificateDictionary["廉洁承诺书"],
                HSF_Guarantee = certificateDictionary["HSF保证书"],
                ISO14001 = certificateDictionary["ISO14001"],
                ISO9001 = certificateDictionary["ISO9001"],
                NotUseChildLabor = certificateDictionary["不使用童工申明"],
                PCN_Protocol = certificateDictionary["PCN协议"],
                QualityAssuranceProtocol = certificateDictionary["质量保证协议"],
                REACH_Guarantee = certificateDictionary["REACH保证书"],
                SupplierBaseDocument = certificateDictionary["供应商基本资料表"],
                SupplierComment = certificateDictionary["供应商评鉴表"],
                SVHC_Guarantee = certificateDictionary["SVHC调查表"],
            };
        }

       
        #endregion
    }

}
