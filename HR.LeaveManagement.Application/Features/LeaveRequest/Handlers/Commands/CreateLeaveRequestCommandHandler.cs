﻿using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveRequest.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistance;
using entities = HR.LeaveManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.DTO.LeaveRequest.Validators;
using HR.LeaveManagement.Application.Exceptions;
using System.Linq;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Contracts.Infrastrcuture;
using System.Net;
using HR.LeaveManagement.Application.Responses.Common;
using HR.LeaveManagement.Application.DTO.LeaveRequest;
using HR.LeaveManagement.Application.Models.Emails;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Handlers.Commands
{
    /// <summary>
    /// User can only create the leave request of a leave Type if the user has the allocation for that type
    /// </summary>
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, CreateCommandResponse<CreateLeaveRequestDto>>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        //check if the user has the sufficient allocation
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        //access the HTTPContext of the api inside the handler
        private readonly IHttpContextAccessor _httpAccessor;
        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository repository, IMapper mapper, 
            ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository,
            IEmailSender emailSender, IHttpContextAccessor httpAccessor)

        {
            _leaveRequestRepository = repository;
            _leaveTypeRepository = leaveTypeRepository;
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
            _emailSender = emailSender;
            _httpAccessor = httpAccessor;
        }
        public async Task<CreateCommandResponse<CreateLeaveRequestDto>> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateCommandResponse<CreateLeaveRequestDto>();

            var validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.CreateLeaveRequestDto);

            //token is coming from client to api which was generated by the api with claims come from client
            //access the token information from client Claims principal
            //from all the claims, get the claim with type userid
            var userId = _httpAccessor.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == "uid")?.Value;

            //Don't put the leave request in the system if request exceeds current allocation for that leave type
            //when admin approve the request, admin make some deduction from the allocated days
            //check leave allocation for this particular employee
            var allocation = await _leaveAllocationRepository.GetLeaveAllocationByUser(userId, request.CreateLeaveRequestDto.LeaveTypeId);
            if (allocation is null)
            {
                validationResult.Errors.Add(
                     new FluentValidation.Results.ValidationFailure("", "Leave Allocation is not accessible"));
                response.Success = false;
                response.StatusCode = HttpStatusCode.FailedDependency;
                response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                return response;
            }

            var numberOfDays = (int)(request.CreateLeaveRequestDto.EndDate - request.CreateLeaveRequestDto.StartDate).TotalDays;
            if(numberOfDays > allocation.NumberOfDays)
            {
                validationResult.Errors.Add(
                    new FluentValidation.Results.ValidationFailure("", "You don't have enough allocated days"));
                response.Success = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                return response;
            }

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Validation Failed";
                response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = "Bad Request";
                //throw new ValidationException(validationResult);
                return response;
            }

            //validate command, create leave request from command and pass it to db
            //no need to access userId creating the leave request until the request id created
            var leaveRequest = _mapper.Map<entities.LeaveRequest>(request.CreateLeaveRequestDto);

            //put employee id data received from the client token
            leaveRequest.EmployeeId = userId;

            //EFCore will update the id
            leaveRequest = await _leaveRequestRepository.AddAsync(leaveRequest);

            response.Success = true;
            response.Message = "New Leave request created Successfully";
            response.RecordId = leaveRequest.Id;
            response.Record = request.CreateLeaveRequestDto;
            response.StatusCode = HttpStatusCode.Created;
            //response.StatusMessage = Enum.GetName(HttpStatusCode.Created);

            
            try
            {
                var emailAddress = _httpAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value
                ?? "employee@gmail.com";

                var email = new Email
                {
                    /*To = "employee@gmail.com",*/
                    To = emailAddress,
                    Subject = "Leave Request Submitted",
                    Body = $"Your leave request from {request.CreateLeaveRequestDto.StartDate:D} to {request.CreateLeaveRequestDto.EndDate:D} has been submitted"
                };
                await _emailSender.SendEmailAsync(email);
            }
            catch (Exception ex) 
            {
                response.Success = false;
            }

            //return leaveRequest.Id;
            return response;

        }
    }
}
