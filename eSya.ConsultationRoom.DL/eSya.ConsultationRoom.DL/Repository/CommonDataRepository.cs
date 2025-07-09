﻿using eSya.ConsultationRoom.DL.Entities;
using eSya.ConsultationRoom.DO;
using eSya.ConsultationRoom.IF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConsultationRoom.DL.Repository
{
    public class CommonDataRepository: ICommonDataRepository
    {
        public async Task<List<DO_BusinessLocation>> GetBusinessKey()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var bk = db.GtEcbslns.Join(db.GtAddrcts,
                        b => new { b.CityCode },
                        c => new { c.CityCode },
                        (b, c) => new { b, c })
                        .Where(w => w.b.ActiveStatus && w.c.ActiveStatus)
                        .Select(r => new DO_BusinessLocation
                        {
                            BusinessKey = r.b.BusinessKey,
                            LocationDescription = r.b.BusinessName + ", " + r.c.CityDesc + ", " + r.b.LocationDescription
                        }).ToListAsync();

                    return await bk;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_ApplicationCodes>> GetApplicationCodesByCodeTypeList(List<int> l_codeType)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEcapcds
                        .Where(w => w.ActiveStatus
                        && l_codeType.Contains(w.CodeType))
                        .Select(r => new DO_ApplicationCodes
                        {
                            CodeType = r.CodeType,
                            ApplicationCode = r.ApplicationCode,
                            CodeDesc = r.CodeDesc
                        }).OrderBy(o => o.CodeDesc).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
