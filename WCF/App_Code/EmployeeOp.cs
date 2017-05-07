using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Employee
/// </summary>
public class EmployeeOp
{
   static SA43Team2StoreDBEntities m = new SA43Team2StoreDBEntities();
 
    public EmployeeOp()
    { }

    /*get department list*/
    public static List<string> GetDept()
    {
        List<string> dept = new List<string>();
        dept = (from x in m.Departments
                select x.DepartmentID).ToList();
        return dept;
    }

    /*get current representative*/
    public static Employee currentRep(string deptId)
    {

        string e = (from x in m.Departments
                    where x.DepartmentID.Equals(deptId)
                    select x.RepID).First();

        Employee e1 = (from x in m.Employees
                       where x.EmpID.Equals(e)
                       select x).First();
        return e1;
    }

    /*authenticate employee by username and password*/
    public static Employee Authenticate(string username,string password)
    {
        try
        {
            Employee e = (from x in m.Employees
                          where x.EmpID.Equals(username)
                          select x).First();

            if (e.Equals(null))
            {
                return null;
            }
            else
            {
                if (e.Password.Equals(password))
                {
                    return e;
                }
                else
                {
                    return null;
                }
            }
        }
        catch
        {
            return null; 
        }
    }

    /*get representative id*/
    public static List<string> getRepresentativeId()
    {

        List<String> l = new List<String>();
        l = (from x in m.Departments
             select x.RepID).ToList();

        return l;
    }

    /*get deparment head*/
    public static Employee getDepHead(string deptId)
    {
        Employee e = (from x in m.Employees
                      where x.DepartmentID.Equals(deptId) && x.EmpTitle.Equals("Head")
                      select x).First();

        return e;
    }

    /*get employee by department id*/
    public static List<Employee> getEmployee(string deptId)
    {
        List<Employee> l = new List<Employee>();
        l = (from x in m.Employees
             where x.DepartmentID.Equals(deptId) && x.EmpTitle.Equals("Employee")
             select x).ToList();

        return l;
    }

    /*get delegated employee*/
    public static void delegateEmp(string DeptId,string EmpName, string startDate, string endDate)
    {
        
        string EmpActName = EmpName.Replace("_", " ");
        Employee e = (from x in m.Employees
                      where x.EmpName.Equals(EmpActName) && x.DepartmentID.Equals(DeptId)
                      select x).First();
        e.Delegate = 1;
      
        Employee e1 = (from x in m.Employees
                       where x.DepartmentID.Equals(DeptId) && x.EmpTitle.Equals("Head")
                       select x).First();
        e1.Delegate = 1;
        e1.DelegateStartDate = Convert.ToDateTime(startDate);
        e1.DelegateEndDate = Convert.ToDateTime(endDate);

        m.SaveChanges();
    }

    /*dedegate employee */
    public static void DedelegateEmp(string EmpName,string DeptId)
    {
        string EmpActName = EmpName.Replace("_", " ");
        Employee e = (from x in m.Employees
                      where x.EmpName.Equals(EmpActName) && x.DepartmentID.Equals(DeptId)
                      select x).First();
        e.Delegate = 0;


        Employee e1 = (from x in m.Employees
                       where x.DepartmentID.Equals(DeptId) && x.EmpTitle.Equals("Head")
                       select x).First();
        e1.Delegate = 0;
        m.SaveChanges();
    }

    /*update representative by department id and employee name*/
    public static void UpdateRep(string deptId,string EmpName)
    {
        string EmpActName = EmpName.Replace("_", " ");
        string e = (from x in m.Employees
                    where x.DepartmentID.Equals(deptId) && x.EmpName.Equals(EmpActName)
                    select x.EmpID).First();

        Department d = (from x in m.Departments
                        where x.DepartmentID.Equals(deptId)
                        select x).First();
        d.RepID = e;
        m.SaveChanges();
    }

    /*get delegated employee by deptId*/
    public static Employee getDelEmployee(string deptId)
    {
        Employee e = (from x in m.Employees
                    where x.DepartmentID.Equals(deptId) && x.EmpTitle.Equals("Employee") && x.Delegate == 1
                    select x).First();
        Employee e1 = (from x in m.Employees
                       where x.DepartmentID.Equals(deptId) && x.EmpTitle.Equals("Head") && x.Delegate == 1
                       select x).First();

        Employee e2 = new Employee();
        e2.EmpID = e.EmpID;
        e2.DepartmentID = deptId;
        e2.EmpTitle = e.EmpTitle;
        e2.EmpName = e.EmpName;
        e2.Email = e.Email;
        e2.Delegate = e.Delegate;
        e2.DelegateStartDate = e1.DelegateStartDate;
        e2.DelegateEndDate = e1.DelegateEndDate;

        return e2;
    }

    /*get collection point*/
    public static List<string> getCollectionPoint(string deptId)
    {
        List<string> l = new List<string>();


       Department d = (from x in m.Departments
                        where x.DepartmentID.Equals(deptId)
                        select x).First();
        string s = (from x in m.CollectionPointDetails
                    where x.CollectionPointID.Equals(d.CollectionPointID)
                    select x.CollectionPoint).First();
        l.Add(s);
        return l;
    }

    /*get collection time*/
    public static List<string> getCollectionTime(string deptId)
    {

        List<string> l = new List<string>();

       Department d = (from x in m.Departments
                        where x.DepartmentID.Equals(deptId)
                        select x).First();
        string s = (from x in m.CollectionPointDetails
                    where x.CollectionPointID.Equals(d.CollectionPointID)
                    select x.CollectionTime).First();
        l.Add(s);
        return l;
    }











}