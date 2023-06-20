using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using TestProject.Domain.Models.Reports;

namespace TestProject.Converters
{
    internal class EmployeeSalaryModelToDecimalConverter : ConverterBase
    {
        protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 0;
            if (value is not IEnumerable) return result;

            var collection = value as IEnumerable<object>;
            var first = collection.FirstOrDefault();
            if (first == null) return result;

            var isDepartment = first is EmployeeSalaryModel;

            if (isDepartment)
            {
                result = CalculateSalaryForDepartment(value as ICollection<dynamic>);
            }
            else
            {
                result = CalculateSalaryForCompany(value as ICollection<dynamic>);
            }

            return result;
        }

        protected override object[] ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private double CalculateSalaryForCompany(ICollection<dynamic> departments)
        {
            double result = 0;
            foreach (var item in departments)
            {
                try
                {

                    result += CalculateSalaryForDepartment(item.Items as ICollection<dynamic>);
                }
                catch (Exception ex)
                {
                    // ignore
                }
            }
            return result;
        }

        private double CalculateSalaryForDepartment(ICollection<dynamic> employeeSalaries)
        {
            double result = 0;
            foreach (var item in employeeSalaries)
            {
                try
                {
                    result += (double)item.EmployeeSalary;
                }
                catch (Exception ex)
                {
                    // ignore
                }
            }
            return result;
        }
    }
}
