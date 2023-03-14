using EmployeeManagmentApplication.Modal.Modals;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagmentApplication.Repository
{
    public interface IEmployeePaymentRepository
    {

        Task<EmployeePaymentDetail> GetEmployeepaymentDetailByIdAsync(int empId);
        Task<List<EmployeePaymentDetail>> GetAllEmployeepaymentDetailAsync();

    }

}
