using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository)
    {
        _leaveRequestRepository = leaveRequestRepository;
    }

    public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        //Get leave request by Id
        var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
        //If leave request not exist then throw exception
        if (leaveRequest == null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);

        //Delete the leave request
        await _leaveRequestRepository.DeleteAsync(leaveRequest);
        return Unit.Value;
    }
}
