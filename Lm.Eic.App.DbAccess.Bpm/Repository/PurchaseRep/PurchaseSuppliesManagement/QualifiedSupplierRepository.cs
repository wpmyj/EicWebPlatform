﻿using Lm.Eic.App.DbAccess.Bpm.Mapping;
using Lm.Eic.App.DomainModel.Bpm.Purchase;
using Lm.Eic.Uti.Common.YleeDbHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Lm.Eic.App.DbAccess.Bpm.Repository.PurchaseRep.PurchaseSuppliesManagement
{
   /// <summary>
   /// 供应商合格文件
    /// </summary>
  public interface ISupplierQualifiedCertificateRepository : IRepository<SupplierQualifiedCertificateModel> { }
  public class SupplierQualifiedCertifcateRepository : BpmRepositoryBase<SupplierQualifiedCertificateModel>, ISupplierQualifiedCertificateRepository
    { }



  /// <summary>
  /// 供应商信息
  /// </summary>
  public interface ISupplierInfoRepository : IRepository<SupplierInfoModel> { }
  public class SupplierInfoRepository : BpmRepositoryBase<SupplierInfoModel>, ISupplierInfoRepository 
  { }


    /// <summary>
    ///  供应商考核表
    /// </summary>
    public interface ISupplierSeasonAuditRepository : IRepository<SupplierSeasonAuditModel> { }
    public class SupplierSeasonAuditRepository:BpmRepositoryBase<SupplierSeasonAuditModel>,ISupplierSeasonAuditRepository
    { }


    /// <summary>
    ///   供应商考核实地辅导计划/执行表
    /// </summary>
    public interface ISupplierSeasonAuditTutorRepository : IRepository<SupplierSeasonTutorModel> { }
    public class SupplierSeasonAuditTutorRepository:BpmRepositoryBase <SupplierSeasonTutorModel>, ISupplierSeasonAuditTutorRepository
    { }


    /// <summary>
    /// 供应商自评复评明细表 
    /// </summary>
    public interface ISupplierGradeInfoRepository:IRepository<SupplierGradeInfoModel> { }
    public class SupplierGradeInfoRepository: BpmRepositoryBase<SupplierGradeInfoModel>, ISupplierGradeInfoRepository
    { }
}
