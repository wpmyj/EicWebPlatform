﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lm.Eic.App.DomainModel.Bpm.Hrm.Archives;
using Lm.Eic.App.DbAccess.Bpm.Repository.HrmRep.Archives;

namespace Lm.Eic.App.Business.Bmp.Hrm.Archives
{
  public class ArIdentityInfoManager
    {
      private IArchivesIdentityRepository irep = null;

      public ArIdentityInfoManager()
      {
          this.irep = new ArchivesIdentityRepository();
      }
      /// <summary>
      /// 根据最后身份证号码的最后六个字母获取身份证信息
      /// </summary>
      /// <param name="lastSixIdWord"></param>
      /// <returns></returns>
      public List<ArchivesIdentityModel> LoadBy(string lastSixIdWord)
      {

          var datas = this.irep.Entities.Where(e => e.IdentityID.EndsWith(lastSixIdWord)).ToList();
          if (datas != null && datas.Count > 0)
          {
              datas.ForEach(d => { d.NativePlace = ArchiveEntityMapper.GetNativePlace(d.IdentityID); });
          }
          return datas;
      }
      /// <summary>
      /// 判断身份证有效期是否过期
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      public bool IdentityLimitDateIsExpired(ArchivesIdentityModel model)
      {
          return DateTime.Now >= ArchiveEntityMapper.GetIdentityExpirationDate(model.LimitedDate);
      }
      public ArchivesIdentityModel GetOneBy(string identityID)
      {

          return this.irep.Entities.FirstOrDefault(e => e.IdentityID == identityID);
      }
    }
}
