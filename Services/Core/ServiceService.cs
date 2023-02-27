using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Model;
using Data.DataAccess;
using AutoMapper;
using Data.DataAccess.Constant;

namespace Services.Core
{
    public interface IServiceService
    {
        ResultModel Add(ServiceCreateModel model);
        ResultModel Update(ServiceUpdateModel model);
        ResultModel Get(Guid? id);
        ResultModel GetAll();
        ResultModel Delete(Guid id);

        Guid TestDI();
    }
    public class ServiceService : IServiceService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly Guid id;

        public Guid TestDI()
        {
            return id;
        }
        public ServiceService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            id = Guid.NewGuid();
        }
        public ResultModel Add(ServiceCreateModel model)
        {   
            var result = new ResultModel();
            try
            {
                var data = _mapper.Map<ServiceCreateModel, Data.Entities.Services>(model);
                _dbContext.Services.Add(data);
                _dbContext.SaveChanges();
                result.Data = _mapper.Map<Data.Entities.Services, ServiceModel>(data);
                result.Succeed = true;

            }
            catch (Exception e)
            {
                result.ErrorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
            }
            return result;
        }

        public ResultModel Delete(Guid id)
        {
            ResultModel result = new ResultModel();
            try
            {
                var data = _dbContext.Services.Where(s => s.Id == id && !s.isDeleted).FirstOrDefault();
                if (data != null)
                {
                    data.isDeleted = true;
                    _dbContext.SaveChanges();
                    var view = _mapper.Map<Data.Entities.Services, ServiceModel>(data);
                    result.Data = view;
                    result.Succeed = true;
                }
                else
                {
                    result.ErrorMessage = "Service" + ErrorMessage.ID_NOT_EXISTED;
                    result.Succeed = false;
                }


            }
            catch (Exception e)
            {
                result.ErrorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
            }
            return result;
        }

        public ResultModel Get(Guid? id)
        {
            ResultModel result = new ResultModel();
            try
            {
                var data = _dbContext.Services.Where(s => s.Id == id && !s.isDeleted).FirstOrDefault();
                if(data != null)
                {
                    var view = _mapper.Map<Data.Entities.Services, ServiceModel>(data);
                    result.Data = view;
                    result.Succeed = true;
                }
                else 
                {
                    result.ErrorMessage = "Service" + ErrorMessage.ID_NOT_EXISTED;
                    result.Succeed = false;
                }
                

            }
            catch (Exception e)
            {
                result.ErrorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
            }
            return result;
        }

        public ResultModel GetAll()
        {
            ResultModel result = new ResultModel();
            try
            {
                var data = _dbContext.Services.Where(s => s.isDeleted != true);
                var view = _mapper.ProjectTo<ServiceModel>(data);
                result.Data = view;
                result.Succeed = true;
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
            }
            return result;
        }

        public ResultModel Update(ServiceUpdateModel model)
        {
            ResultModel result = new ResultModel();
            try
            {
                var data = _dbContext.Services.Where(s => s.Id == model.Id && !s.isDeleted).FirstOrDefault();
                if (data != null)
                {
                    if(model.Code != null)
                    {
                        data.Code = model.Code;
                    }
                    if (model.Name != null)
                    {
                        data.Name = model.Name;
                    }
                    if (model.Price != null)
                    {
                        data.Price = model.Price;
                    }
                    data.DateUpdated = DateTime.Now;
                    _dbContext.SaveChanges();
                    result.Succeed = true;
                    result.Data = _mapper.Map<Data.Entities.Services, ServiceModel>(data);
                }
                else
                {
                    result.ErrorMessage = "Service" + ErrorMessage.ID_NOT_EXISTED;
                    result.Succeed = false;
                }
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.InnerException != null ? e.InnerException.Message : e.Message;
            }
            return result;
        }
    }

        
    }
