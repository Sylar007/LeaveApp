using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public ChangeLeaveRequestApprovalCommandHandler(
         ILeaveRequestRepository leaveRequestRepository, 
         ILeaveTypeRepository leaveTypeRepository, 
         ILeaveAllocationRepository leaveAllocationRepository,
         IMapper mapper, 
         IEmailSender emailSender)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _leaveTypeRepository = leaveTypeRepository;
        this._leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
        this._emailSender = emailSender;
    }

    public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
    {
        //Get leave request by Id
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
        //Check if record is found and exist
        if (leaveRequest is null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        
        leaveRequest.Approved = request.Approved;
        //Call repository object to update approve flag to true 
        await _leaveRequestRepository.UpdateAsync(leaveRequest);

        // if request is approved, get and update the employee's allocations
        if (request.Approved)
        {
            int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
            // Get current leave allocation for the user
            var allocation = await _leaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
            allocation.NumberOfDays -= daysRequested;
            // Update leave allocation leave for the user after leave taken
            await _leaveAllocationRepository.UpdateAsync(allocation);
        }

        try
        {
            var email = new EmailMessage
            {
                To = string.Empty, /* Get email from employee record */
                Body = $"The approval status for your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been updated.",
                Subject = "Leave Request Approval Status Updated"
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
