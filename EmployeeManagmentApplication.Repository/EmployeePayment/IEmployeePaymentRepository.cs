using EmployeeManagmentApplication.Modal.ApplicationClass.DTO.EmployeePaymentDetailsDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagmentApplication.Repository.EmployeeManagmentPaymentRepository
{
    public interface IEmployeePaymentRepository
    {

        Task<EmployeePaymentDetailsDTO> GetEmployeepaymentDetailByIdAsync(int empId);
        Task<List<EmployeePaymentDetailsDTO>> GetAllEmployeepaymentDetailAsync();

    }

}
