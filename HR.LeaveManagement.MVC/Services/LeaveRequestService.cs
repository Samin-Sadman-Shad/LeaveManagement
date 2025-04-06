using AutoMapper;
using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using HR.LeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;

namespace HR.LeaveManagement.MVC.Services
{
    public class LeaveRequestService : BaseHttpService, ILeaveRequestService
    {
        private readonly IClient __httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;

        public LeaveRequestService(IClient client, ILocalStorageService storage, IMapper mapper) 
            : base(client, storage)
        {
            __httpClient = client;
            _localStorageService = storage;
            _mapper = mapper;
        }

        public async Task<Response<int>> CreateLeaveRequest(CreateLeaveRequestViewModel viewModel)
        {
            try
            {
                var response = new Response<int>();
                CreateLeaveRequestDto leaveRequestDto = _mapper.Map<CreateLeaveRequestDto>(viewModel);
                AddBearerToken();
                var apiResponse = await _client.LeaveRequestsPOSTAsync(leaveRequestDto);
                if (apiResponse is null)
                {
                    response.Success = false;
                    response.StatusCode = HttpStatusCode._417;
                }
                else if (apiResponse.Success)
                {
                    response.Success = true;
                    response.StatusCode = apiResponse.StatusCode;
                    response.Message = apiResponse.Message;
                    response.Data = apiResponse.RecordId;
                }
                else
                {
                    response.Success = false;
                    response.StatusCode = apiResponse.StatusCode;
                    response.Message = apiResponse.Message;
                    if (apiResponse.Errors is not null)
                    {
                        foreach (var error in apiResponse.Errors)
                        {
                            response.ValidationErrors.Add(error);
                        }
                    }
                }

                return response;
            }
            catch(ApiException ex)
            {
                return ConvertApiException<int>(ex);
            }

        }

        public Task<Response<bool>> DeleteLeaveRequest(int leaveRequestId)
        {
            throw new NotImplementedException();
        }
    }
}
