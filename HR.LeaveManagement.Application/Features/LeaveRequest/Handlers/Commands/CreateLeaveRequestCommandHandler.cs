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
using HR.LeaveManagement.Application.Responses;
using System.Linq;
using HR.LeaveManagement.Application.Models;
using HR.LeaveManagement.Application.Contracts.Infrastrcuture;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository repository, IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IEmailSender emailSender)

        {
            _leaveRequestRepository = repository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _emailSender = emailSender;

        }
        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.CreateLeaveRequestDto);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Validation Failed";
                response.Errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                response.StatusCode = 400;
                response.StatusMessage = "Bad Request";
                //throw new ValidationException(validationResult);
                return response;
            }

            var leaveRequest = _mapper.Map<entities.LeaveRequest>(request.CreateLeaveRequestDto);
            //EFCore will update the id
            leaveRequest = await _leaveRequestRepository.AddAsync(leaveRequest);


            response.Success = true;
            response.Message = "New Leave request created Successfully";
            response.RecordId = leaveRequest.Id;
            response.StatusCode = 201;
            response.StatusMessage = "OK";

            var email = new Email
            {
                To = "employee@gmail.com",
                Subject = "Leave Request Submitted",
                Body = $"Your leave request from {request.CreateLeaveRequestDto.StartDate:D} to {request.CreateLeaveRequestDto.EndDate:D} has been submitted"
            };
            try
            {
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
