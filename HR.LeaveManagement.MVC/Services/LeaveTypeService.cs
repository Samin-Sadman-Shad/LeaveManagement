using AutoMapper;
using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services.Base;
using systemNet = System.Net;

namespace HR.LeaveManagement.MVC.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {
        private readonly IClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly IMapper _mapper;
        public LeaveTypeService(IClient httpClient, ILocalStorageService storage, IMapper mapper):base(httpClient, storage)
        {
            _httpClient = httpClient;
            _localStorage = storage;
            _mapper = mapper;
        }

        public async Task<Response<int>> Create(CreateLeaveTypeViewModel model)
        {
            try
            {
                var response = new Response<int>();
                var createLeaveTypeDto = _mapper.Map<CreateLeaveTypeDto>(model);
                //_client.ReadResponse = true;
                var apiResult = await _client.LeaveTypesPOSTAsync(createLeaveTypeDto);
                if (apiResult is null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode._417;
                }
                else if (apiResult.Success)
                {
                    response.Success = true;
                    response.StatusCode = apiResult.StatusCode;
                    response.Message = apiResult.Message;
                    response.Data = apiResult.RecordId;
                }
                else
                {
                    response.Success = false;
                    response.StatusCode = apiResult.StatusCode;
                    response.Message = apiResult.Message;
                    if(apiResult.Errors != null)
                    {
                        foreach (var error in apiResult.Errors)
                        {
                            response.ValidationErrors.Add(error);
                        }
                    }

                }

                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiException<int>(ex);
            }

        }

        public async Task<Response<int>> Delete(int id)
        {
            try
            {
                var response = new Response<int>();
                var result = await _client.LeaveTypesDELETEAsync(id);
                if(result is null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode._417;
                    return response;
                }
                if (result.Success)
                {
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                }
                response.StatusCode = result.StatusCode;
                return response;
            }
            catch(ApiException ex)
            {
                return ConvertApiException<int>(ex);
            }


        }

        public async Task<List<LeaveTypeViewModel>> GetAll()
        {
            try
            {
                var result = await _client.LeaveTypesGETAsync();
                var leaveTypes = result.Records;
                return _mapper.Map<List<LeaveTypeViewModel>>(leaveTypes);
            }
            catch (ApiException ex)
            {
                return new List<LeaveTypeViewModel>();
            }
            catch(Exception ex)
            {
                return new List<LeaveTypeViewModel>();
            }

        }

        public async Task<LeaveTypeViewModel> GetById(int id)
        {
            var result = await _client.LeaveTypesGET2Async(id);
            var leaveType = result.Record;
            return _mapper.Map<LeaveTypeViewModel>(leaveType);
        }

        public async Task<Response<int>> Update(int id, LeaveTypeViewModel model)
        {
            try
            {
                var response = new Response<int>();
                var updateLeaveTypeDto = _mapper.Map<UpdateLeaveTypeDto>(model);
                var apiResult = await _client.LeaveTypesPUTAsync(id, updateLeaveTypeDto);
                if (apiResult is null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode._417;
                }
                else if (apiResult.Success)
                {
                    response.Success = true;
                    response.StatusCode = apiResult.StatusCode;
                    response.Message = apiResult.Message;
                }
                else
                {
                    response.Success = false;
                    response.StatusCode = apiResult.StatusCode;
                    if(apiResult.Errors != null)
                    {
                        foreach (var error in apiResult.Errors)
                        {
                            response.ValidationErrors.Add(error);
                        }
                    }

                }

                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiException<int>(ex);
            }
        }
    }
}
