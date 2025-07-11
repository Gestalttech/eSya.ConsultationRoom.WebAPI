using eSya.ConsultationRoom.DL.Entities;
using eSya.ConsultationRoom.DO;
using eSya.ConsultationRoom.IF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.ConsultationRoom.DL.Repository
{
    public class ConsultationRoomRepository: IConsultationRoomRepository
    {
        private readonly IStringLocalizer<ConsultationRoomRepository> _localizer;
        public ConsultationRoomRepository(IStringLocalizer<ConsultationRoomRepository> localizer)
        {
            _localizer = localizer;
        }

        #region Consultation Room Header
        public async Task<List<DO_Lounge>> GetLoungebyBusinessKey(int businesskey)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var lounges = await db.GtTokm02s.Where(x => x.ActiveStatus == true && x.BusinessKey == businesskey)
                        .Join(db.GtEcapcds,
                        x => new { x.Lounge },
                        y => new { Lounge = y.ApplicationCode },
                        (x, y) => new { x, y })
                                        .Select(t => new DO_Lounge
                                        {
                                            LoungeID = t.x.Lounge,
                                            LoungeName = t.y.CodeDesc
                                        }).
                    GroupBy(y => y.LoungeID, (key, grp) => grp.FirstOrDefault())
                  .ToListAsync();
                    return lounges;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<List<DO_Floor>> GetFloorsbyLounge(int businesskey, int lounge)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var floors = await db.GtTokm02s.Where(x => x.ActiveStatus == true && x.BusinessKey == businesskey
                    && x.Lounge == lounge)
                        .Join(db.GtEcapcds,
                        x => new { x.FloorId },
                        y => new { FloorId = y.ApplicationCode },
                        (x, y) => new { x, y })
                                        .Select(t => new DO_Floor
                                        {
                                            FloorId = t.x.FloorId,
                                            FloorName = t.y.CodeDesc
                                        }).
                    GroupBy(y => y.FloorId, (key, grp) => grp.FirstOrDefault())
                  .ToListAsync();
                    return floors;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<List<DO_Area>> GetAreasbyFloor(int businesskey,int lounge, int floorId)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {
                    var lounges = await db.GtTokm02s.Where(x => x.Lounge == lounge && x.FloorId==floorId
                     && x.BusinessKey == businesskey && x.ActiveStatus == true)
                        .Join(db.GtEcapcds,
                        x => new { x.Area },
                        y => new { Area = y.ApplicationCode },
                        (x, y) => new { x, y })
                                        .Select(t => new DO_Area
                                        {
                                            AreaID = t.x.Area,
                                            AreaName = t.y.CodeDesc
                                        }).
                    GroupBy(y => y.AreaID, (key, grp) => grp.FirstOrDefault())
                  .ToListAsync();
                    return lounges;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<List<DO_ConsultationRoomHeader>> GetConsultationRoomHeadersbyBusinessKey(int businesskey)
        {
            using (var db = new eSyaEnterprise())
            {
                try
                {

                    var ds = db.GtEccsrms.Where(w => w.BusinessKey == businesskey).
                               Join(db.GtEcapcds.Where(w=>w.ActiveStatus),
                               a => new { a.Lounge },
                               p => new { Lounge = p.ApplicationCode },
                               (a, p) => new { a, p }).
                               Join(db.GtEcapcds.Where(w => w.ActiveStatus),
                                b => new { b.a.FloorId },
                                c => new { FloorId = c.ApplicationCode },
                                (b, c) => new { b, c }).
                                Join(db.GtEcapcds.Where(w => w.ActiveStatus),
                                d => new { d.b.a.Area },
                                e => new { Area = e.ApplicationCode },
                               (d, e) => new { d, e }).
                               Select(r => new DO_ConsultationRoomHeader
                               {
                                   BusinessKey = r.d.b.a.BusinessKey,
                                   Lounge = r.d.b.a.Lounge,
                                   FloorId = r.d.b.a.FloorId,
                                   Area = r.d.b.a.Area,
                                   LoungeKey = r.d.b.a.LoungeKey,
                                   NoOfConsRoom = r.d.b.a.NoOfConsRoom,
                                   RoomCount = r.d.b.a.RoomCount,
                                   ActiveStatus = r.d.b.a.ActiveStatus,
                                   LoungeDesc = r.d.b.p.CodeDesc,
                                   FloorDesc = r.d.c.CodeDesc,
                                   AreaDesc = r.e.CodeDesc
                               }).ToList();

                    var result = ds.OrderBy(x => x.LoungeDesc)
                      .ThenBy(x => x.FloorDesc)
                      .ThenBy(x => x.AreaDesc)
                      .ToList();
                    return result;
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task<DO_ReturnParameter> InsertOrUpdateIntoConsultationRoomsHeader(DO_ConsultationRoomHeader obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var _rmheader = db.GtEccsrms.Where(x => x.BusinessKey == obj.BusinessKey && x.Lounge == obj.Lounge
                        && x.FloorId == obj.FloorId && x.Area == obj.Area).FirstOrDefault();
                        if (_rmheader != null)
                        {
                            _rmheader.NoOfConsRoom = obj.NoOfConsRoom;
                            _rmheader.RoomCount = obj.RoomCount;
                            _rmheader.ActiveStatus = obj.ActiveStatus;
                            _rmheader.ModifiedBy = obj.UserID;
                            _rmheader.ModifiedOn = System.DateTime.Now;
                            _rmheader.ModifiedTerminal = obj.TerminalID;
                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                        }
                        else
                        {
                            int max_lounge = db.GtEccsrms.Where(w=>w.BusinessKey==obj.BusinessKey).Select(c => c.LoungeKey).DefaultIfEmpty().Max();
                            int _loungekey = max_lounge + 1;
                            var rm = new GtEccsrm()
                            {
                                BusinessKey=obj.BusinessKey,
                                Lounge =obj.Lounge,
                                FloorId=obj.FloorId,
                                Area=obj.Area,
                                LoungeKey= _loungekey,
                                NoOfConsRoom=obj.NoOfConsRoom,
                                RoomCount=obj.RoomCount,
                                ActiveStatus=obj.ActiveStatus,
                                FormId=obj.FormID,
                                CreatedBy=obj.UserID,
                                CreatedOn=System.DateTime.Now,
                                CreatedTerminal=obj.TerminalID,
                            };
                            db.GtEccsrms.Add(rm);
                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                        }
                      
                       

                    }

                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                }
            }
        }
        public async Task<DO_ReturnParameter> ActiveOrDeActiveConsultationRooms(bool status, int businesskey, int loungekey)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var t_map = db.GtEccsrms.Where(x => x.BusinessKey == businesskey && x.LoungeKey == loungekey).FirstOrDefault();
                        if (t_map == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0001", Message = string.Format(_localizer[name: "W0001"]) };
                        }
                        t_map.ActiveStatus = status;
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        if (status == true)
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0003", Message = string.Format(_localizer[name: "S0003"]) };
                        else
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0004", Message = string.Format(_localizer[name: "S0004"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        #endregion

        #region Consultation Room Details
        public async Task<List<DO_ConsultationRoomDetails>> GetConsultationRoomDetailsbyLoungekey(int businesskey, int loungekey)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEacorms
                        .Where(w => w.BusinessKey == businesskey && w.LoungeKey == loungekey)
                        .Select(r => new DO_ConsultationRoomDetails
                        {
                            BusinessKey = r.BusinessKey,
                            LoungeKey = r.LoungeKey,
                            ConsultRoomNo = r.ConsultRoomNo,
                            Remarks = r.Remarks,
                            RoomStatus = r.RoomStatus,
                            ActiveStatus = r.ActiveStatus
                        }).OrderBy(o => o.ConsultRoomNo).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<DO_ReturnParameter> InsertOrUpdateIntoConsultationRoomsDetail(DO_ConsultationRoomDetails obj)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var _rmdetails = db.GtEacorms.Where(x => x.BusinessKey == obj.BusinessKey && x.LoungeKey == obj.LoungeKey
                        && x.ConsultRoomNo.ToUpper().Replace(" ", "") == obj.ConsultRoomNo.ToUpper().Replace(" ", "")).FirstOrDefault();
                        if (_rmdetails != null)
                        {
                            _rmdetails.Remarks = obj.Remarks;
                            _rmdetails.RoomStatus = obj.RoomStatus;
                            _rmdetails.ActiveStatus = obj.ActiveStatus;
                            _rmdetails.ModifiedBy = obj.UserID;
                            _rmdetails.ModifiedOn = System.DateTime.Now;
                            _rmdetails.ModifiedTerminal = obj.TerminalID;
                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0002", Message = string.Format(_localizer[name: "S0002"]) };
                        }
                        else
                        {

                            var rmdt = new GtEacorm()
                            {
                                BusinessKey = obj.BusinessKey,
                                LoungeKey = obj.LoungeKey,
                                ConsultRoomNo = obj.ConsultRoomNo,
                                Remarks = obj.Remarks,
                                RoomStatus = obj.RoomStatus,
                                ActiveStatus = obj.ActiveStatus,
                                FormId = obj.FormID,
                                CreatedBy = obj.UserID,
                                CreatedOn = System.DateTime.Now,
                                CreatedTerminal = obj.TerminalID,
                            };
                            db.GtEacorms.Add(rmdt);
                            await db.SaveChangesAsync();
                            dbContext.Commit();
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0001", Message = string.Format(_localizer[name: "S0001"]) };
                        }



                    }

                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));
                    }
                }
            }
        }
        public async Task<DO_ReturnParameter> ActiveOrDeActiveConsultationRoomsDetail(bool status, int businesskey, int loungekey, string consroomNo)
        {
            using (var db = new eSyaEnterprise())
            {
                using (var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        var t_details = db.GtEacorms.Where(x => x.BusinessKey == businesskey && x.LoungeKey == loungekey && x.ConsultRoomNo.ToUpper().Replace(" ", "")
                           == consroomNo.ToUpper().Replace(" ", "")).FirstOrDefault();
                        if (t_details == null)
                        {
                            return new DO_ReturnParameter() { Status = false, StatusCode = "W0001", Message = string.Format(_localizer[name: "W0001"]) };
                        }
                        t_details.ActiveStatus = status;
                        await db.SaveChangesAsync();
                        dbContext.Commit();
                        if (status == true)
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0003", Message = string.Format(_localizer[name: "S0003"]) };
                        else
                            return new DO_ReturnParameter() { Status = true, StatusCode = "S0004", Message = string.Format(_localizer[name: "S0004"]) };
                    }
                    catch (DbUpdateException ex)
                    {
                        dbContext.Rollback();
                        throw new Exception(CommonMethod.GetValidationMessageFromException(ex));

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        #endregion

    }
}
