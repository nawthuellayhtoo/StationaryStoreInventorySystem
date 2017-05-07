using System;
using DataAccess;
using Model;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class LoginBL
    {
        LoginDA da = new LoginDA();
        EmployeeBO ebo;
        DepartmentBO dbo;

        public EmployeeBO convertEmployeeBO(Employee e)
        {
            ebo = new EmployeeBO();
            ebo.EmployeeId = e.EmpID;
            ebo.EmployeeDept = e.DepartmentID;
            ebo.Department = getDeptByEmpDeptId(ebo.EmployeeDept);
            ebo.EmployeeTitle = e.EmpTitle;
            ebo.EmployeeName = e.EmpName;
            ebo.EmployeeEmail = e.Email;
            ebo.Delegate = e.Delegate;
            ebo.DelegateStartDate = (DateTime)e.DelegateStartDate;
            ebo.DelegateEndDate = (DateTime)e.DelegateEndDate;
            ebo.Password = e.Password;
            return ebo;
        }

        public List<EmployeeBO> getEmployeeListByDepartmentId(string deptId)
        {
            List<Employee> list = da.getEmpListByDeptId(deptId);
            List<EmployeeBO> listbo = new List<EmployeeBO>();

            foreach (Employee e in list)
            {
                EmployeeBO ebo = convertEmployeeBO(e);
                listbo.Add(ebo);
            }

            return listbo;
        }

        public DepartmentBO convertDeptBO(Department d)
        {
            dbo = new DepartmentBO();
            dbo.DeptId = d.DepartmentID;
            dbo.DeptName = d.DepartmentName;
            dbo.DeptRep = d.RepID;
            dbo.DeptCollectionPoint = d.CollectionPointID;
            return dbo;
        }

        public bool checkDelegate(EmployeeBO ebo)
        {
            if (ebo != null)
            {
                if (ebo.EmployeeTitle.Equals("Employee"))
                {
                    if (ebo.Delegate.Equals(1))
                    {
                        EmployeeBO hbo = getDeptHeadByDepId(ebo.EmployeeDept);

                        if (DateTime.Today >= hbo.DelegateStartDate)
                        {
                            if (hbo.DelegateEndDate >= DateTime.Today)
                            {
                                return true;
                            }
                        }

                        else if (DateTime.Today > hbo.DelegateEndDate)
                        {
                            ebo.Delegate = 0;
                            hbo.Delegate = 0;
                            da.updateDelegateStatus(ebo.EmployeeId);
                            da.updateDelegateStatus(hbo.EmployeeId);
                            return false;
                        }
                    }
                }

                if (DateTime.Today >= ebo.DelegateStartDate)
                {
                    if (ebo.DelegateEndDate >= DateTime.Today)
                    {
                        if (ebo.Delegate.Equals(1))
                        {
                            if (ebo.EmployeeTitle.Equals("Head"))
                            {
                                return true;
                            }

                            else if (ebo.EmployeeTitle.Equals("Manager"))
                            {
                                return true;
                            }

                            else if (ebo.EmployeeTitle.Equals("Supervisor"))
                            {
                                return true;
                            }
                        }

                        else
                        {
                            return false;
                        }
                    }
                }
            }

            if (DateTime.Today > ebo.DelegateEndDate)
            {
                ebo.Delegate = 0;
                da.updateDelegateStatus(ebo.EmployeeId);
                return false;
            }

            return false;
        }
        public EmployeeBO getDeptHeadByDepId(string depId)
        {
            Employee head = da.getHeadByDepId(depId);
            EmployeeBO hbo = convertEmployeeBO(head);

            return hbo;

        }
        public EmployeeBO authenticate(string userId, string password)
        {
            Employee employee = da.getEmployeeById(userId);

            if (employee != null)
            {
                if (employee.Password == password)
                {
                    EmployeeBO ebo = convertEmployeeBO(employee);
                }
            }

            return ebo;
        }

        public EmployeeBO getStoreManagerBO()
        {
            Employee sm = da.getStoreManager();
            EmployeeBO smbo = convertEmployeeBO(sm);
            return smbo;
        }

        public EmployeeBO getStoreSupervisorBO()
        {
            Employee ss = da.getStoreSupervisor();
            EmployeeBO ssbo = convertEmployeeBO(ss);
            return ssbo;
        }

        public DepartmentBO getDeptByEmpDeptId(string deptId)
        {
            Department dept = da.getDepartmentById(deptId);
            DepartmentBO dbo = convertDeptBO(dept);
            return dbo;
        }
    }
}