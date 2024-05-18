using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest
{
    //Cancel Leave Request
    public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IEmailSender _emailSender;

        public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, 
        ILeaveAllocationRepository leaveAllocationRepository,
        IEmailSender emailSender)
        {
            this._leaveRequestRepository = leaveRequestRepository;
            this._leaveAllocationRepository = leaveAllocationRepository;
            this._emailSender = emailSender;
        }

        public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            //Get leave request by Id
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
            //Check if record is found and exist
            if (leaveRequest is null)
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            leaveRequest.Cancelled = true;
            //Call repository object to update cancel leave flag to true  
            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            // if already approved, re-evaluate the employee's allocations for the leave type
            if(leaveRequest.Approved == true)
            {
                int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                // Get current leave allocation for the user
                var allocation = await _leaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                allocation.NumberOfDays += daysRequested;
                // Update leave allocation leave for the user after leave taken
                await _leaveAllocationRepository.UpdateAsync(allocation);
            }
            
            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty, /* Get email from employee record */
                    Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been cancelled successfully.",
                    Subject = "Leave Request Cancelled"
                };
                // send confirmation email based on leave request info
                await _emailSender.SendEmail(email);
            }
            catch (Exception)
            {
                // log error
            }
            
            return Unit.Value;
        }
    }
}
